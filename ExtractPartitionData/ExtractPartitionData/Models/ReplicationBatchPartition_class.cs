using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractPartitionData.Models;
public class ReplicationBatchPartition_class
{
    public int? Id { get; set; }
    public int? BatchId { get; set; }
    public int? PartitionId { get; set; }
    public string? Payload { get; set; }

    public static string SQLQuery_GetAllBatchPartitions = @"SELECT Id, BatchId, PartitionId, Payload FROM [$ic$ReplicationBatchPartition]";
    public static string SQLQuery_GetBatchPartitionById = SQLQuery_GetAllBatchPartitions + @" WHERE Id = %RID%";
    public static string SQLQuery_GetBatchPartitionByBatchId = SQLQuery_GetAllBatchPartitions + @" WHERE BatchId = %BID%";
    public static string SQLQuery_GetBatchPartitionByIdByBatchIdByPartition = SQLQuery_GetAllBatchPartitions + @" WHERE Id = %RID% AND BatchId = %BID% AND PartitionId = %PID%";
    public static string ScalarSQLQuery_GetMinPartitionByBatch = @"SELECT MIN(PartitionId) FROM [$ic$ReplicationBatchPartition] WHERE Id = %RID% AND BatchId = %BID%";
    public static string ScalarSQLQuery_GetMaxPartitionByBatch = @"SELECT MAX(PartitionId) FROM [$ic$ReplicationBatchPartition] WHERE Id = %RID% AND BatchId = %BID%";

}
