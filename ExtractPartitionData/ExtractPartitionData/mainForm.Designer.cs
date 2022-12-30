namespace ExtractPartitionData;

partial class mainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.tbServerName = new System.Windows.Forms.TextBox();
            this.tbServerUser = new System.Windows.Forms.TextBox();
            this.tbServerPwd = new System.Windows.Forms.TextBox();
            this.tbDatabaseName = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnWellFormed = new System.Windows.Forms.Button();
            this.btnGetReplications = new System.Windows.Forms.Button();
            this.tbOutputWindow = new System.Windows.Forms.TextBox();
            this.cbAnalyzeSelection = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnExamine = new System.Windows.Forms.Button();
            this.cboxSaveOutput = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnTestObject = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbServerName
            // 
            this.tbServerName.Location = new System.Drawing.Point(81, 12);
            this.tbServerName.Name = "tbServerName";
            this.tbServerName.Size = new System.Drawing.Size(217, 23);
            this.tbServerName.TabIndex = 0;
            this.tbServerName.Text = "<required>";
            // 
            // tbServerUser
            // 
            this.tbServerUser.Location = new System.Drawing.Point(81, 70);
            this.tbServerUser.Name = "tbServerUser";
            this.tbServerUser.Size = new System.Drawing.Size(217, 23);
            this.tbServerUser.TabIndex = 1;
            this.tbServerUser.Text = "<required>";
            // 
            // tbServerPwd
            // 
            this.tbServerPwd.Location = new System.Drawing.Point(81, 99);
            this.tbServerPwd.Name = "tbServerPwd";
            this.tbServerPwd.PasswordChar = '*';
            this.tbServerPwd.Size = new System.Drawing.Size(217, 23);
            this.tbServerPwd.TabIndex = 2;
            // 
            // tbDatabaseName
            // 
            this.tbDatabaseName.Location = new System.Drawing.Point(81, 41);
            this.tbDatabaseName.Name = "tbDatabaseName";
            this.tbDatabaseName.Size = new System.Drawing.Size(217, 23);
            this.tbDatabaseName.TabIndex = 3;
            this.tbDatabaseName.Text = "<required>";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(82, 193);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Database";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "User Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Password";
            // 
            // btnWellFormed
            // 
            this.btnWellFormed.Location = new System.Drawing.Point(81, 363);
            this.btnWellFormed.Name = "btnWellFormed";
            this.btnWellFormed.Size = new System.Drawing.Size(75, 23);
            this.btnWellFormed.TabIndex = 10;
            this.btnWellFormed.Text = "Validate";
            this.btnWellFormed.UseVisualStyleBackColor = true;
            this.btnWellFormed.Click += new System.EventHandler(this.btnWellFormed_Click);
            // 
            // btnGetReplications
            // 
            this.btnGetReplications.Location = new System.Drawing.Point(163, 193);
            this.btnGetReplications.Name = "btnGetReplications";
            this.btnGetReplications.Size = new System.Drawing.Size(75, 23);
            this.btnGetReplications.TabIndex = 11;
            this.btnGetReplications.Text = "Get ALL";
            this.btnGetReplications.UseVisualStyleBackColor = true;
            this.btnGetReplications.Click += new System.EventHandler(this.btnGetReplications_Click);
            // 
            // tbOutputWindow
            // 
            this.tbOutputWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutputWindow.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbOutputWindow.Location = new System.Drawing.Point(342, 12);
            this.tbOutputWindow.Multiline = true;
            this.tbOutputWindow.Name = "tbOutputWindow";
            this.tbOutputWindow.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbOutputWindow.Size = new System.Drawing.Size(1024, 704);
            this.tbOutputWindow.TabIndex = 12;
            // 
            // cbAnalyzeSelection
            // 
            this.cbAnalyzeSelection.FormattingEnabled = true;
            this.cbAnalyzeSelection.Location = new System.Drawing.Point(82, 128);
            this.cbAnalyzeSelection.Name = "cbAnalyzeSelection";
            this.cbAnalyzeSelection.Size = new System.Drawing.Size(217, 23);
            this.cbAnalyzeSelection.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "Analyze";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 729);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1378, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // btnExamine
            // 
            this.btnExamine.Location = new System.Drawing.Point(82, 222);
            this.btnExamine.Name = "btnExamine";
            this.btnExamine.Size = new System.Drawing.Size(75, 23);
            this.btnExamine.TabIndex = 16;
            this.btnExamine.Text = "Examine";
            this.btnExamine.UseVisualStyleBackColor = true;
            this.btnExamine.Click += new System.EventHandler(this.btnExamine_Click);
            // 
            // cboxSaveOutput
            // 
            this.cboxSaveOutput.AutoSize = true;
            this.cboxSaveOutput.Checked = true;
            this.cboxSaveOutput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxSaveOutput.Location = new System.Drawing.Point(82, 157);
            this.cboxSaveOutput.Name = "cboxSaveOutput";
            this.cboxSaveOutput.Size = new System.Drawing.Size(15, 14);
            this.cboxSaveOutput.TabIndex = 17;
            this.cboxSaveOutput.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 15);
            this.label6.TabIndex = 18;
            this.label6.Text = "Save Output";
            // 
            // btnTestObject
            // 
            this.btnTestObject.Location = new System.Drawing.Point(210, 351);
            this.btnTestObject.Name = "btnTestObject";
            this.btnTestObject.Size = new System.Drawing.Size(75, 23);
            this.btnTestObject.TabIndex = 19;
            this.btnTestObject.Text = "Test Obj";
            this.btnTestObject.UseVisualStyleBackColor = true;
            this.btnTestObject.Click += new System.EventHandler(this.btnTestObject_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 751);
            this.Controls.Add(this.btnTestObject);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboxSaveOutput);
            this.Controls.Add(this.btnExamine);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbAnalyzeSelection);
            this.Controls.Add(this.tbOutputWindow);
            this.Controls.Add(this.btnGetReplications);
            this.Controls.Add(this.btnWellFormed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tbDatabaseName);
            this.Controls.Add(this.tbServerPwd);
            this.Controls.Add(this.tbServerUser);
            this.Controls.Add(this.tbServerName);
            this.Name = "mainForm";
            this.Text = "Partition Extractor";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private TextBox tbServerName;
    private TextBox tbServerUser;
    private TextBox tbServerPwd;
    private TextBox tbDatabaseName;
    private Button btnConnect;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Button btnWellFormed;
    private Button btnGetReplications;
    private TextBox tbOutputWindow;
    private ComboBox cbAnalyzeSelection;
    private Label label5;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel toolStripStatusLabel1;
    private Button btnExamine;
    private CheckBox cboxSaveOutput;
    private Label label6;
    private Button btnTestObject;
}
