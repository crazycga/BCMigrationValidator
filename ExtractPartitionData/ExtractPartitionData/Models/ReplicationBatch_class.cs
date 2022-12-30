using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractPartitionData.Models;
public class ReplicationBatch_class
{
    public int? Id { get; set; }
    public int? BatchId { get; set; }
    public string? Type { get; set; }

    public static string SQLQuery_GetAllBatches = @"SELECT Id, BatchId, [Type] FROM [$ic$ReplicationBatch]";
    public static string SQLQuery_GetBatchByReplicationId = SQLQuery_GetAllBatches + " WHERE Id = %RID%";
    public static string ScalarSQLQuery_GetMinBatchNumber = @"SELECT MIN(BatchId) FROM [$ic$ReplicationBatch] WHERE Id = %RID%";
    public static string ScalarSQLQuery_GetMaxBatchNumber = @"SELECT MAX(BatchId) FROM [$ic$ReplicationBatch] WHERE Id = %RID%";
}
