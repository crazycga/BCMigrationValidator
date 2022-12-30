using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractPartitionData.Models.DynamicsGPMigrationData;

/// <summary>
/// These are the basic tests that should be checked for data types.  They're in a class in case a universal test needs to be added.
/// </summary>
static class BasicTests
{
    public static bool TestInteger(string? testData, bool acceptNull = false)
    {
        if ((!acceptNull) && (testData == null))
        {
            return false;
        }
        else
        {
            return Int64.TryParse(testData, out var result);
        }
    }

    public static bool TestFloat(string? testData, bool acceptNull = false)
    {
        if ((!acceptNull) && (testData == null))
        {
            return false;
        }
        else
        {
            return float.TryParse(testData, out var result);
        }
    }

    public static bool TestBoolean(string? testData, bool acceptNull = false)
    {
        if ((!acceptNull) && (testData == null))
        {
            return false;
        }
        else
        {
            return Boolean.TryParse(testData, out var result);
        }
    }
}
