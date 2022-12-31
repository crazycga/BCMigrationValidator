using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractPartitionData;
public static class SQLStatements
{
    public static string ScalarSQLQuery_GetPartitionData = @"SELECT Payload FROM [$ic$ReplicationBatchPartition] WHERE Id = %RID% AND BatchId = %BID% AND ParitionId = %PID%";
    public static string ScalarSQLQuery_GetBatchType = @"SELECT Type FROM [$ic$ReplicationBatch] WHERE Id = %RID% AND BatchId = %BID%";
    public static string ScalarSQLQuery_GetMinBatchNumber = @"SELECT MIN(BatchId) FROM [$ic$ReplicationBatch] WHERE Id = %RID%";
    public static string ScalarSQLQuery_GetMaxBatchNumber = @"SELECT MAX(BatchId) FROM [$ic$ReplicationBatch] WHERE Id = %RID%";

}
