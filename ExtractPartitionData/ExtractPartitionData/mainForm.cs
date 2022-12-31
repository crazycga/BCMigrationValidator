namespace ExtractPartitionData;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Drawing.Text;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using ExtractPartitionData.Models;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using ExtractPartitionData.Models.DynamicsGPMigrationData;

public partial class mainForm : Form
{
    /// RID: ReplicationId
    /// BID: BatchId
    /// PID: PartitionId
    /// 
    /// For each:
    ///     - Replication ID
    ///         - Batch ID [file]
    ///             - collect Partition IDs <summary>
    /// 
    /// Notes: 
    /// - the known batch types are: MERGE, DELETE
    /// - the batch numbering for MERGE starts at 1; DELETE seems to start at 0
    /// 

    public static mainForm _referringWindow;

    List<Replication_class> Replications = new List<Replication_class>();
    List<ReplicationBatch_class> Batches = new List<ReplicationBatch_class>();
    List<ReplicationBatchPartition_class> Partitions = new List<ReplicationBatchPartition_class>();

    AppConfig_class currentConfig = new AppConfig_class();
    BindingList<FileSelector_class> comboboxFileList = new BindingList<FileSelector_class>();
    SqlConnection mainSQLConnection = new SqlConnection();

    LogLevel _LOGLEVEL = LogLevel.Warning;

    public mainForm()
    {
        InitializeComponent();
        _referringWindow = this;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        var mySettings = Program.Config.GetSection("MySettings").Get<AppConfig_class>();

        if (mySettings != null)
        {
            if (!string.IsNullOrEmpty(mySettings.LogLevel))
            {
                switch(mySettings.LogLevel)
                {
                    case "None": this._LOGLEVEL = LogLevel.None; break;
                    case "Critical": this._LOGLEVEL = LogLevel.Critical; break;
                    case "Error": this._LOGLEVEL = LogLevel.Error; break;
                    case "Warning": this._LOGLEVEL = LogLevel.Warning; break;
                    case "Information": this._LOGLEVEL = LogLevel.Information; break;
                    case "Debug": this._LOGLEVEL = LogLevel.Debug; break;
                    case "Trace": this._LOGLEVEL = LogLevel.Trace; break;
                    default: this._LOGLEVEL = LogLevel.Error; break;
                }
            }

            if (!string.IsNullOrEmpty(mySettings.DefaultServer))
            {
                this.tbServerName.Text = mySettings.DefaultServer;
            }

            if (!string.IsNullOrEmpty(mySettings.DefaultDatabase))
            {
                this.tbDatabaseName.Text = mySettings.DefaultDatabase;
            }

            if (!string.IsNullOrEmpty(mySettings.DefaultUser))
            {
                this.tbServerUser.Text = mySettings.DefaultUser;
            }

            if (!string.IsNullOrEmpty(mySettings.DefaultPassword))
            {
                this.tbServerPwd.Text = mySettings.DefaultPassword;
            }
        }

        FileSelector_class blankList = new FileSelector_class();
        blankList.Id = 1;
        blankList.ReplicationNumber = -1;
        blankList.BatchNumber = -1;
        blankList.Name = "<please connect first>";

        this.comboboxFileList.Add(blankList);

        var cbBindingSource = new BindingSource();
        cbBindingSource.DataSource = this.comboboxFileList;

        cbAnalyzeSelection.DataSource = cbBindingSource;
        cbAnalyzeSelection.DisplayMember = "Name";
        cbAnalyzeSelection.ValueMember = "Id";
        cbAnalyzeSelection.SelectedIndex = 0;
        cbAnalyzeSelection.Refresh();

        toolStripStatusLabel1.Text = "Unconnected";
    }

    public void AppendOutput(string incomingText)
    {
        if (incomingText == null) { return; }

        string tempOut;
        tempOut = DateTime.Now.ToString("s") + " >>> " + incomingText + Environment.NewLine;
        this.tbOutputWindow.AppendText(tempOut);
        this.tbOutputWindow.ScrollToCaret();
        Application.DoEvents();
    }

    private void btnWellFormed_Click(object sender, EventArgs e)
    {
        int counter = 0;

        OpenFileDialog newOpen = new OpenFileDialog();

        if (newOpen.ShowDialog() == DialogResult.OK)
        {
            var settings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore, XmlResolver = null, ConformanceLevel = ConformanceLevel.Fragment };


            using (var reader = XmlReader.Create(new StreamReader(newOpen.FileName), settings))
            {
                var newDocument = new XmlDocument();
                XmlElement newRoot = newDocument.CreateElement("Root");
                newDocument.AppendChild(newRoot);

                XmlNode n;
                while ((n = newDocument.ReadNode(reader)) != null)
                {
                    newRoot.AppendChild(n);
                    counter++;
                }

                XDocument myXDoc;

                using (var nodeReader = new XmlNodeReader(newDocument))
                {
                    nodeReader.MoveToContent();
                    myXDoc = XDocument.Load(nodeReader);

                    var something = myXDoc.Descendants().Select(x => (string)x.Attribute("CUSTNMBR")).ToList();
                    foreach (string checkit in something)
                    {
                        if (!string.IsNullOrEmpty(checkit) && (checkit.Contains('\t')))
                        {
                            MessageBox.Show("Found: " + checkit);
                        }
                    }
                }


            }
        }
        MessageBox.Show("Done: " + counter.ToString() + " records.");
    }

    private void MakeConnection()
    {
        if (string.IsNullOrEmpty(tbServerName.Text))
        {
            AppendOutput("No server name specified... Aborting.");
        }

        if (string.IsNullOrEmpty(tbDatabaseName.Text))
        {
            AppendOutput("No database name specified...  Aborting.");
            return;
        }

        if (string.IsNullOrEmpty(tbServerUser.Text))
        {
            AppendOutput("No user name specified...  Aborting.");
            return;
        }

        if (string.IsNullOrEmpty(tbServerPwd.Text))
        {
            AppendOutput("No password specified...  Aborting.");
            return;
        }

        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        builder.DataSource = tbServerName.Text;
        builder.InitialCatalog = tbDatabaseName.Text;
        builder.UserID = tbServerUser.Text;
        builder.Password = tbServerPwd.Text;
        builder.TrustServerCertificate = true;

        mainSQLConnection = new SqlConnection(builder.ConnectionString);
        try
        {
            mainSQLConnection.Open();
            AppendOutput("Connected to database...  Refreshing list.");

            toolStripStatusLabel1.Text = "Connected to [" + tbServerName.Text + "][" + tbDatabaseName.Text + "]";

            DownloadAllReplications(mainSQLConnection);
            DownloadAllReplicationBatches(mainSQLConnection);

            this.comboboxFileList.Clear();
            

            int counter = 1;

            foreach(ReplicationBatch_class x in this.Batches)
            {
                FileSelector_class newSelector = new FileSelector_class();
                newSelector.Id = counter;
                newSelector.ReplicationNumber = (int)x.Id;
                newSelector.BatchNumber = (int)x.BatchId;

                Replication_class parentReplication = Replications.Find(s => s.Id == x.Id);
                if (parentReplication != null)
                {
                    newSelector.Name = parentReplication.SourceSqlTableName + " - Batch " + newSelector.BatchNumber.ToString();
                }
                else
                {
                    newSelector.Name = "Error occurred";
                }

                comboboxFileList.Add(newSelector);

                counter++;
            }

            this.cbAnalyzeSelection.Refresh();
            AppendOutput("List refresh done...  " + this.comboboxFileList.Count.ToString() + " items in list...");
        }
        catch(Exception ex)
        {
            AppendOutput("An error occured; exception: " + ex.Message);
        }
    }

    private void DownloadAllReplications(SqlConnection myConn)
    {
        string sqlQuery_Replications = Replication_class.SQLQuery_GetAllReplicationRecords;

        using (SqlCommand getReplications = new SqlCommand(sqlQuery_Replications, myConn))
        {
            getReplications.CommandTimeout = 0;                     // Set to zero to disable timeouts - large datasets pretty much require this

            if (myConn.State != System.Data.ConnectionState.Open)
            {
                myConn.Open();
            }

            using (SqlDataReader reader = getReplications.ExecuteReader())
            {
                while (reader.Read())
                {
                    Replication_class incomingRecord = new Replication_class();
                    if (reader.IsDBNull(reader.GetOrdinal("Id")))
                    {
                        incomingRecord.Id = null;
                    }
                    else
                    {
                        incomingRecord.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    }

                    if (reader.IsDBNull(reader.GetOrdinal("SqlTableName")))
                    {
                        incomingRecord.SqlTableName = null;
                    }
                    else
                    {
                        incomingRecord.SqlTableName = reader.GetString(reader.GetOrdinal("SqlTableName"));
                    }

                    if (reader.IsDBNull(reader.GetOrdinal("SourceSqlTableName")))
                    {
                        incomingRecord.SourceSqlTableName = null;
                    }
                    else
                    {
                        incomingRecord.SourceSqlTableName = reader.GetString(reader.GetOrdinal("SourceSqlTableName"));
                    }

                    if (reader.IsDBNull(reader.GetOrdinal("TableName")))
                    {
                        incomingRecord.TableName = null;
                    }
                    else
                    {
                        incomingRecord.TableName = reader.GetString(reader.GetOrdinal("TableName"));
                    }

                    if (reader.IsDBNull(reader.GetOrdinal("CompanyName")))
                    {
                        incomingRecord.CompanyName = null;
                    }
                    else
                    {
                        incomingRecord.CompanyName = reader.GetString(reader.GetOrdinal("CompanyName"));
                    }

                    if (reader.IsDBNull(reader.GetOrdinal("SyncedVersion")))
                    {
                        incomingRecord.SyncedVersion = null;
                    }
                    else
                    {
                        incomingRecord.SyncedVersion = reader.GetInt64(reader.GetOrdinal("SyncedVersion"));
                    }

                    if (reader.IsDBNull(reader.GetOrdinal("ErrorCode")))
                    {
                        incomingRecord.ErrorCode = null;
                    }
                    else
                    {
                        incomingRecord.ErrorCode = reader.GetString(reader.GetOrdinal("ErrorCode"));
                    }

                    if (reader.IsDBNull(reader.GetOrdinal("ErrorMessage")))
                    {
                        incomingRecord.ErrorMessage = null;
                    }
                    else
                    {
                        incomingRecord.ErrorMessage = reader.GetString(reader.GetOrdinal("ErrorMessage"));
                    }

                    Replications.Add(incomingRecord);
                }
            }
        }
    }

    private void DownloadAllReplicationBatches(SqlConnection myConn)
    {
        string sqlQuery_ReplicationBatches = ReplicationBatch_class.SQLQuery_GetAllBatches;

        using (SqlCommand getReplicationBatches = new SqlCommand(sqlQuery_ReplicationBatches, myConn))
        {
            getReplicationBatches.CommandTimeout = 0;                     // Set to zero to disable timeouts - large datasets pretty much require this

            if (myConn.State != System.Data.ConnectionState.Open)
            {
                myConn.Open();
            }

            using (SqlDataReader reader = getReplicationBatches.ExecuteReader())
            {
                while (reader.Read())
                {
                    ReplicationBatch_class incomingRecord = new ReplicationBatch_class();
                    if (reader.IsDBNull(reader.GetOrdinal("Id")))
                    {
                        incomingRecord.Id = null;
                    }
                    else
                    {
                        incomingRecord.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    }

                    if (reader.IsDBNull(reader.GetOrdinal("BatchId")))
                    {
                        incomingRecord.BatchId = null;
                    }
                    else
                    {
                        incomingRecord.BatchId = reader.GetInt32(reader.GetOrdinal("BatchId"));
                    }

                    if (reader.IsDBNull(reader.GetOrdinal("Type")))
                    {
                        incomingRecord.Type = null;
                    }
                    else
                    {
                        incomingRecord.Type = reader.GetString(reader.GetOrdinal("Type"));
                    }

                    Batches.Add(incomingRecord);
                }
            }
        }
    }

    private void DownloadAllPartitions(SqlConnection myConn)
    {
        string sqlQuery_ReplicationBatchPartitions = ReplicationBatchPartition_class.SQLQuery_GetAllBatchPartitions;

        using (SqlCommand getReplicationBatches = new SqlCommand(sqlQuery_ReplicationBatchPartitions, myConn))
        {
            getReplicationBatches.CommandTimeout = 0;                     // Set to zero to disable timeouts - large datasets pretty much require this

            if (myConn.State != System.Data.ConnectionState.Open)
            {
                myConn.Open();
            }

            using (SqlDataReader reader = getReplicationBatches.ExecuteReader())
            {
                while (reader.Read())
                {
                    ReplicationBatchPartition_class incomingRecord = new ReplicationBatchPartition_class();
                    if (reader.IsDBNull(reader.GetOrdinal("Id")))
                    {
                        incomingRecord.Id = null;
                    }
                    else
                    {
                        incomingRecord.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    }

                    if (reader.IsDBNull(reader.GetOrdinal("BatchId")))
                    {
                        incomingRecord.BatchId = null;
                    }
                    else
                    {
                        incomingRecord.BatchId = reader.GetInt32(reader.GetOrdinal("BatchId"));
                    }

                    if (reader.IsDBNull(reader.GetOrdinal("PartitionId")))
                    {
                        incomingRecord.PartitionId = null;
                    }
                    else
                    {
                        incomingRecord.PartitionId = reader.GetInt32(reader.GetOrdinal("PartitionId"));
                    }

                    if (reader.IsDBNull(reader.GetOrdinal("Payload")))
                    {
                        incomingRecord.Payload = null;
                    }
                    else
                    {
                        incomingRecord.Payload = reader.GetString(reader.GetOrdinal("Payload"));
                    }

                    Partitions.Add(incomingRecord);
                    if (this._LOGLEVEL <= LogLevel.Debug)
                    {
                        this.AppendOutput("Replication Id: " + incomingRecord.Id.ToString() + ", Batch Id: " + incomingRecord.BatchId.ToString() + ", PartitionId: " + incomingRecord.PartitionId.ToString() + " loaded...");                    
                    }
                }
            }
        }
    }

    private string DownloadReplicationBatchPartitions(int ReplicationId, int BatchId)
    {
        int fromPart = 0;
        int toPart = 0;
        List<ReplicationBatchPartition_class> currentPartitions = new List<ReplicationBatchPartition_class>();

        string tempReturn = string.Empty;
        string scalar_GetMin = ReplicationBatchPartition_class.ScalarSQLQuery_GetMinPartitionByBatch.Replace("%RID%", ReplicationId.ToString()).Replace("%BID%", BatchId.ToString());
        string scalar_GetMax = ReplicationBatchPartition_class.ScalarSQLQuery_GetMaxPartitionByBatch.Replace("%RID%", ReplicationId.ToString()).Replace("%BID%", BatchId.ToString());

        using (SqlCommand minCmd = new SqlCommand(scalar_GetMin, mainSQLConnection))
        {
            fromPart = (int)minCmd.ExecuteScalar();
        }

        using (SqlCommand maxCmd = new SqlCommand(scalar_GetMax, mainSQLConnection))
        {
            toPart = (int)maxCmd.ExecuteScalar();
        }

        for(int i = fromPart; i <= toPart; i++)
        {
            ReplicationBatchPartition_class tempRead = new ReplicationBatchPartition_class();

            string sql_GetPartition = ReplicationBatchPartition_class.SQLQuery_GetBatchPartitionByIdByBatchIdByPartition
                .Replace("%RID%", ReplicationId.ToString())
                .Replace("%BID%", BatchId.ToString())
                .Replace("%PID%", i.ToString());

            AppendOutput("Loading R: " + ReplicationId.ToString() + " B: " + BatchId.ToString() + " P: " + i.ToString() + "...");

            using (SqlCommand getCmd = new SqlCommand(sql_GetPartition, mainSQLConnection))
            {
                getCmd.CommandTimeout = 0;

                using (SqlDataReader reader = getCmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        if (reader.IsDBNull(reader.GetOrdinal("Id")))
                        {
                            tempRead.Id = null;
                        }
                        else
                        {
                            tempRead.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        }

                        if (reader.IsDBNull(reader.GetOrdinal("BatchId")))
                        {
                            tempRead.BatchId = null;
                        }
                        else
                        {
                            tempRead.BatchId = reader.GetInt32(reader.GetOrdinal("BatchId"));
                        }
                        if (reader.IsDBNull(reader.GetOrdinal("PartitionId")))
                        {
                            tempRead.PartitionId = null;
                        }
                        else
                        {
                            tempRead.PartitionId = reader.GetInt32(reader.GetOrdinal("PartitionId"));
                        }
                        if (reader.IsDBNull(reader.GetOrdinal("Payload")))
                        {
                            tempRead.Payload = null;
                        }
                        else
                        {
                            tempRead.Payload = reader.GetString(reader.GetOrdinal("Payload"));
                        }
                    }
                }

                currentPartitions.Add(tempRead);
            }
        }

        AppendOutput("Loaded " + currentPartitions.Count.ToString() + " partitions; parsing...");

        foreach (ReplicationBatchPartition_class x in currentPartitions)
        {
            tempReturn += x.Payload;
        }

        AppendOutput("String returned with " + tempReturn.Length.ToString() + " characters...");
        return tempReturn;
    }

    private bool ValidateXMLBuild(string incomingParse)
    {
        bool tempReturn = false;

        if (string.IsNullOrEmpty(incomingParse))
        {
            return false;
        }

        var XMLSettings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore, XmlResolver = null, ConformanceLevel = ConformanceLevel.Fragment };

        try
        {
            using (var reader = XmlReader.Create(new StringReader(incomingParse), XMLSettings)) 
            {
                var newDocument = new XmlDocument();
                XmlElement newRoot = newDocument.CreateElement("Root");
                newDocument.AppendChild(newRoot);

                XmlNode n;
                while ((n = newDocument.ReadNode(reader)) != null)
                {
                    newRoot.AppendChild(n);
                }
            }

            tempReturn = true;
        }
        catch(Exception ex)
        {
            AppendOutput("Error found: " + ex.Message);
            tempReturn = false;
        }

        return tempReturn;
    }
    
    private void OutputToFile(string incomingParse)
    {
        SaveFileDialog newSaveFile = new SaveFileDialog();
        if (newSaveFile.ShowDialog() == DialogResult.OK)
        {
            File.WriteAllText(newSaveFile.FileName, incomingParse);
        }
    }

    private void TestCM00100()
    {
        string tempRecord = string.Empty;

        tempRecord = DownloadReplicationBatchPartitions(2, 1);

        CM00100_record newCheck = new CM00100_record();
        newCheck = CM00100_record.Parse(tempRecord);
        newCheck.PerformTests();
        AppendOutput("Done...");
    }

    private void btnGetReplications_Click(object sender, EventArgs e)
    {
        if (mainSQLConnection.State != System.Data.ConnectionState.Open)
        {
            AppendOutput("Connection not open yet; please connect first...  Aborting.");
            return;
        }

        if (MessageBox.Show("Are you sure you want to get ALL data?","Seriously?", MessageBoxButtons.OKCancel) != DialogResult.OK)
        {
            return;
        }

        DownloadAllReplications(mainSQLConnection);
        this.AppendOutput(this.Replications.Count.ToString() + " replications downloaded...");
        DownloadAllReplicationBatches(mainSQLConnection);
        this.AppendOutput(this.Batches.Count.ToString() + " batches downloaded...");
        DownloadAllPartitions(mainSQLConnection);
        this.AppendOutput(this.Partitions.Count.ToString() + " partitions downloaded...");
        this.AppendOutput("Done.");
    }

    private void btnConnect_Click(object sender, EventArgs e)
    {
        MakeConnection();
    }

    private void btnExamine_Click(object sender, EventArgs e)
    {
        try
        {
            if (mainSQLConnection.State != System.Data.ConnectionState.Open)
            {
                AppendOutput("Connection not open yet; please connect first...  Aborting.");
                return;
            }

            FileSelector_class thisFile = comboboxFileList.Where(s => s.Id == cbAnalyzeSelection.SelectedIndex).FirstOrDefault();

            if (thisFile == null)
            {
                AppendOutput("There are no records for this type...");
                return;
            }

            string temp = DownloadReplicationBatchPartitions(thisFile.ReplicationNumber, thisFile.BatchNumber);

            ValidateXMLBuild(temp);

            if (cboxSaveOutput.Checked)
            {
                OutputToFile(temp);
            }
        }
        catch(Exception ex)
        {
            AppendOutput("Error: " + ex.Message);
        }

        AppendOutput("Done...");
    }

    private void btnTestObject_Click(object sender, EventArgs e)
    {
        TestCM00100();
    }
}
