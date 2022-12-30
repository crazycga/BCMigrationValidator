using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtractPartitionData.Models.DynamicsGPMigrationData;
using NuGet.Frameworks;

namespace ExtractPartitionDataTests.Models_Tests.DynamicsGPMigrationData_Tests;

[TestClass]
public class CM00100_record_Tests
{
    public CM00100_record PrepareSampleRecord()
    {
        // note: this will prepare a BASIC sample record with NO expected failures

        CM00100_record tempReturn = new CM00100_record();
        tempReturn.BANKID = "SAMPLE";
        tempReturn.BANKNAME = "Sample Bank of Someplace";
        tempReturn.ADDRESS1 = "123 Somewhere Street";
        tempReturn.ADDRESS2 = "Station A";
        tempReturn.ADDRESS3 = "Highway 860";
        tempReturn.CITY = "FictionTown";
        tempReturn.STATE = "SomeState";
        tempReturn.ZIPCODE = "X0X 0X0";                     // Canadian postal code example
        tempReturn.COUNTRY = "Fiction Country";
        tempReturn.PHNUMBR1 = "8888675309";
        tempReturn.PHNUMBR2 = "99914411243";
        tempReturn.PHONE3 = "23894795";
        tempReturn.FAXNUMBR = "3075550099";
        tempReturn.TRNSTNBR = "8228988";
        tempReturn.BNKBRNCH = "999";
        tempReturn.NOTEINDX = "2812710.00000";
        tempReturn.DDTRANUM = "90950";
        tempReturn.DEX_ROW_ID = "32767";

        return tempReturn;
    }

    [TestMethod]
    public void Tab_Check_Object()
    {
        CM00100_record testRecord = this.PrepareSampleRecord();
        testRecord.BANKID = "AB\u0009CD";

        bool result = testRecord.PerformTests();

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void CarriageReturn_Check_Object()
    {
        CM00100_record testRecord = this.PrepareSampleRecord();
        testRecord.ADDRESS3 = "AB\u000dCD";

        bool result = testRecord.PerformTests();

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void NewLine_Check_Object()
    {
        CM00100_record testRecord = this.PrepareSampleRecord();
        testRecord.DDTRANUM = "AB\u000aCD";

        bool result = testRecord.PerformTests();

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Confirm_Total_Expected_Pass()
    {
        CM00100_record passRecord = this.PrepareSampleRecord();

        bool result = passRecord.PerformTests();

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Conversion_PHNUMBR1_failure_test()
    {
        CM00100_record testRecord = this.PrepareSampleRecord();
        testRecord.PHNUMBR1 = "888867xf5309";

        bool result = testRecord.PerformTests();
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Conversion_PHNUMBR2_failure_test()
    {
        CM00100_record testRecord = this.PrepareSampleRecord();
        testRecord.PHNUMBR2 = "888867xf5309";

        bool result = testRecord.PerformTests();
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Conversion_PHONE3_failure_test()
    {
        CM00100_record testRecord = this.PrepareSampleRecord();
        testRecord.PHONE3 = "888867xf5309";

        bool result = testRecord.PerformTests();
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Conversion_FAXNUMBR_failure_test()
    {
        CM00100_record testRecord = this.PrepareSampleRecord();
        testRecord.FAXNUMBR = "888867xf5309";

        bool result = testRecord.PerformTests();
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Conversion_TRNSTNBR_failure_test()
    {
        CM00100_record testRecord = this.PrepareSampleRecord();
        testRecord.TRNSTNBR = "7l539";

        bool result = testRecord.PerformTests();
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Conversion_BNKBRNCH_failure_test()
    {
        CM00100_record testRecord = this.PrepareSampleRecord();
        testRecord.BNKBRNCH = "645.642";

        bool result = testRecord.PerformTests();
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Conversion_NOTEINDX_failure_test()
    {
        CM00100_record testRecord = this.PrepareSampleRecord();
        testRecord.NOTEINDX = "23994.9x33";

        bool result = testRecord.PerformTests();
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Conversion_DDTRANUM_failure_test()
    {
        CM00100_record testRecord = this.PrepareSampleRecord();
        testRecord.DDTRANUM = "1world2";

        bool result = testRecord.PerformTests();
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Conversion_DEX_ROW_ID_failure_test()
    {
        CM00100_record testRecord = this.PrepareSampleRecord();
        testRecord.DEX_ROW_ID = "1,434";

        bool result = testRecord.PerformTests();
        Assert.IsFalse(result);
    }
}
