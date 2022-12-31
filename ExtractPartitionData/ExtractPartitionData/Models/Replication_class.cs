using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ExtractPartitionData.Models;
public class Replication_class
{
    public int? Id { get; set; }
    public string? SqlTableName { get; set; }
    public string? SourceSqlTableName { get; set; }
    public string? TableName { get; set; }
    public string? CompanyName { get; set; }
    public long? SyncedVersion { get; set; }
    public string? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }

    public static string SQLQuery_GetAllReplicationRecords = @"SELECT Id, SqlTableName, SourceSqlTableName, TableName, CompanyName, SyncedVersion, ErrorCode, ErrorMessage FROM [$ic$Replication]";
    public static string SQLQuery_GetReplicationRecordById = SQLQuery_GetAllReplicationRecords + " WHERE Id = %RID%";
}
