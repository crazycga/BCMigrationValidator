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

    /// <summary>
    /// Will determine if the testData is a boolean if it can survive a boolean.TryParse **OR** if it is a 1 or 0
    /// </summary>
    /// <param name="testData">String to be tested</param>
    /// <param name="acceptNull">Determines whether nulls are allowed or not; default NOT allowed</param>
    /// <returns>True if the string can be converted to a boolean; false if not</returns>
    public static bool TestBoolean(string? testData, bool acceptNull = false)
    {
        if ((!acceptNull) && (testData == null))
        {
            return false;
        }
        else
        {
            if (int.TryParse(testData, out var result))
            {
                if ((result == 1) || (result == 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return Boolean.TryParse(testData, out var result_secondary);
            }
        }
    }

    public static bool TestDateTime(string? testData, bool acceptNull = false)
    {
        if ((!acceptNull && testData == null))
        {
            return false;
        }
        else
        {
            return DateTime.TryParse(testData, out var result);
        }
    }
}
