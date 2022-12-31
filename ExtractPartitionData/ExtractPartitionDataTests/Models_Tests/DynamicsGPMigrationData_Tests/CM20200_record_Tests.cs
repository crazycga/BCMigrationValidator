using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtractPartitionData.Models.DynamicsGPMigrationData;
using NuGet.Frameworks;

namespace ExtractPartitionDataTests.Models_Tests.DynamicsGPMigrationData_Tests;

[TestClass]
public class CM20200_record_Tests
{
    public CM20200_record PrepareSampleRecord()
    {
        // note: this will prepare a BASIC sample record with NO expected failures

        CM20200_record tempReturn = new CM20200_record();
        tempReturn.CHEKBKID = "ROYALBANK";
        tempReturn.DSCRIPTN = "Sample bank account";
        tempReturn.BANKID = "RBC";
        tempReturn.CURNCYID = "CAD";
        tempReturn.ACTINDX = "1";
        tempReturn.BNKACTNM = "884483";
        tempReturn.NXTCHNUM = "999";
        tempReturn.Next_Deposit_Number = "43523";
        tempReturn.INACTIVE = "0";
        tempReturn.DYDEPCLR = "1";
        tempReturn.XCDMCHPW = "Sample";
        tempReturn.MXCHDLR = "32.45";
        tempReturn.DUPCHNUM = "0";
        tempReturn.OVCHNUM1 = "1";
        tempReturn.LOCATNID = "SITE";
        tempReturn.CMUSRDF1 = "User Defined";
        tempReturn.CMUSRDF2 = "Second UDF";
        tempReturn.Last_Reconciled_Date = "2022-06-15";
        tempReturn.Last_Reconciled_Balance = "999.33";
        tempReturn.CURRBLNC = "1234.56";
        tempReturn.CREATDDT = "1999-12-31";
        tempReturn.MODIFDT = "2005-01-19";
        tempReturn.Recond = "0";
        tempReturn.Reconcile_In_Progress = "3874.25";
        tempReturn.Deposit_In_Progress = "87724.23";
        tempReturn.CHBKPSWD = "Password";
        tempReturn.CURNCYPD = "TextHere";
        tempReturn.CRNCYRCD = "Currency";
        tempReturn.ADPVADLR = "23985.23";
        tempReturn.ADPVAPWD = "AnotherPassword";
        tempReturn.DYCHTCLR = "334422";
        tempReturn.CMPANYID = "5";
        tempReturn.CHKBKTYP = "2";
        tempReturn.DDACTNUM = "22222";
        tempReturn.DDINDNAM = "Account Name";
        tempReturn.DDTRANS = "98032";
        tempReturn.PaymentRateTypeID = "AVERAGE";
        tempReturn.DepositRateTypeID = "SPOT";
        tempReturn.CashInTransAcctIdx = "22";
        tempReturn.NOTEINDX = "2812710.00000";
        tempReturn.DEX_ROW_ID = "9999";

        return tempReturn;
    }

    [TestMethod]
    public void Confirm_Total_Expected_Pass()
    {
        CM20200_record passRecord = this.PrepareSampleRecord();

        bool result = passRecord.PerformTests();

        Assert.IsTrue(result);
    }
}
