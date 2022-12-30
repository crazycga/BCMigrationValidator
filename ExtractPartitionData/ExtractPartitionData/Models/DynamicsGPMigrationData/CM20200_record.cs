using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExtractPartitionData.Models.DynamicsGPMigrationData;

[XmlRootAttribute("row")]
public class CM20200_record : _DynamicsGPMigrationRecord_class
{
    [XmlAttribute("CHEKBKID")]
    public string? CHEKBKID { get; set; }

    [XmlAttribute("DSCRIPTN")]
    public string? DSCRIPTN { get; set; }

    [XmlAttribute("BANKID")]
    public string? BANKID { get; set; }

    [XmlAttribute("CURNCYID")]
    public string? CURNCYID { get; set; }

    [XmlAttribute("ACTINDX")]
    public string? ACTINDX { get; set; }

    [XmlAttribute("BNKACTNM")]
    public string? BNKACTNM { get; set; }

    [XmlAttribute("NXTCHNUM")]
    public string? NXTCHNUM { get; set; }

    [XmlAttribute("Next_Deposit_Number")]
    public string? Next_Deposit_Number { get; set; }

    [XmlAttribute("INACTIVE")]
    public string? INACTIVE { get; set; }

    [XmlAttribute("DYDEPCLR")]
    public string? DYDEPCLR { get; set; }

    [XmlAttribute("XCDMCHPW")]
    public string? XCDMCHPW { get; set; }

    [XmlAttribute("MXCHDLR")]
    public string? MXCHDLR { get; set; }

    [XmlAttribute("DUPCHNUM")]
    public string? DUPCHNUM { get; set; }

    [XmlAttribute("OVCHNUM1")]
    public string? OVCHNUM1 { get; set; }

    [XmlAttribute("LOCATNID")]
    public string? LOCATNID { get; set; }

    [XmlAttribute("CMUSRDF1")]
    public string? CMUSRDF1 { get; set; }

    [XmlAttribute("CMUSRDF2")]
    public string? CMUSRDF2 { get; set; }

    [XmlAttribute("Last_Reconciled_Date")]
    public string? Last_Reconciled_Date { get; set; }

    [XmlAttribute("Last_Reconciled_Balance")]
    public string? Last_Reconciled_Balance { get; set; }

    [XmlAttribute("CURRBLNC")]
    public string? CURRBLNC { get; set; }

    [XmlAttribute("CREATDDT")]
    public string? CREATDDT { get; set; }

    [XmlAttribute("MODIFDT")]
    public string? MODIFDT { get; set; }

    [XmlAttribute("Recond")]
    public string? Recond { get; set; }

    [XmlAttribute("Reconcile_In_Progress")]
    public string? Reconcile_In_Progress { get; set; }

    [XmlAttribute("Deposit_In_Progress")]
    public string? Deposit_In_Progress { get; set; }

    [XmlAttribute("CHBKPSWD")]
    public string? CHBKPSWD { get; set; }

    [XmlAttribute("CURNCYPD")]
    public string? CURNCYPD { get; set; }

    [XmlAttribute("CRNCYRCD")]
    public string? CRNCYRCD { get; set; }

    [XmlAttribute("ADPVADLR")]
    public string? ADPVADLR { get; set; }

    [XmlAttribute("ADPVAPWD")]
    public string? ADPVAPWD { get; set; }

    [XmlAttribute("DYCHTCLR")]
    public string? DYCHTCLR { get; set; }

    [XmlAttribute("CMPANYID")]
    public string? CMPANYID { get; set; }

    [XmlAttribute("CHKBKTYP")]
    public string? CHKBKTYP { get; set; }

    [XmlAttribute("DDACTNUM")]
    public string? DDACTNUM { get; set; }

    [XmlAttribute("DDINDNAM")]
    public string? DDINDNAM { get; set; }

    [XmlAttribute("DDTRANS")]
    public string? DDTRANS { get; set; }

    [XmlAttribute("PaymentRateTypeID")]
    public string? PaymentRateTypeID { get; set; }

    [XmlAttribute("DepositRateTypeID")]
    public string? DepositRateTypeID { get; set; }

    [XmlAttribute("CashInTransAcctIdx")]
    public string? CashInTransAcctIdx { get; set; }

    public CM20200_record() : base()
    {
    }

    public CM20200_record(mainForm referringForm) : base(referringForm)
    {
    }

    public static CM20200_record Parse(string incomingXML)
    {
        CM20200_record tempReturn = new CM20200_record();

        var mySerializer = new XmlSerializer(typeof(CM20200_record));
        using (var myStream = new StringReader(incomingXML))
        {
            tempReturn = (CM20200_record)mySerializer.Deserialize(myStream);
        }

        return tempReturn;
    }

    public override bool PerformTests()
    {
        if (string.IsNullOrEmpty(this.CHEKBKID))
        {
            throw new ArgumentException("Object is not initialized");
        }

        bool testsPassed = true;

        // Tests:
        // 1. base tests
        // 2. can account index be parsed into integer?
        // 3. can bank account number be parsed into integer?
        // 4. can next cheque number be parsed into integer?
        // 5. can next deposit number be parsed into integer?
        // 6. can inactive be parsed into boolean?
        // 7. can DYDEPCLR(?) be parsed into a boolean?
        // 8. can MXCHDLR be parsed into a float?
        // 9. can duplicate cheque number be parsed into a boolean?
        // 10. can OVCHNUM1 be parsed into a boolean?
        // 11. can Last_Reconciled_Date be parsed into a datetime?
        // 12. can Last_Reconciled_Balance be parsed into a float?
        // 13. can current balance be parsed into a float?
        // 14. can created date be parsed into a datetime?
        // 15. can modified date be parsed into a datetime?
        // 16. can recond be parsed into a boolean?
        // 17. can Reconcile_In_Progress be parsed into a float?
        // 18. can Deposit_In_Progress be parsed into a float?
        // 19. can ADPVADLR be parsed into a float?
        // 20. can DYCHTCLR be parsed into an integer?
        // 21. can company ID be parsed into an integer?
        // 22. can chequebook type by parsed into an integer?
        // 23. can direct deposit account number be parsed into an integer?
        // 24. can direct deposit transit number be parsed into an integer?
        // 25. can cash in transit account index be parsed into an integer?

        testsPassed = base.PerformTests();
        if (!testsPassed)
        {
            AppendOutput("Record: " + this.CHEKBKID + " failed universal validation...");
            return testsPassed;
        }

        if (BasicTests.TestInteger(this.ACTINDX) == false)
        {
            AppendOutput("Record: " + this.CHEKBKID + "; ACTINDX failed validation...");
            testsPassed = false;
        }

        if (BasicTests.TestInteger(this.BNKACTNM) == false)
        {
            AppendOutput("Record: " + this.CHEKBKID + "; BNKACTNM failed validation...");
            testsPassed = false;
        }

        if (BasicTests.TestInteger(this.NXTCHNUM) == false)
        {
            AppendOutput("Record: " + this.CHEKBKID + "; NXTCHNUM failed validation...");
            testsPassed = false;
        }

        if (BasicTests.TestInteger(this.Next_Deposit_Number) == false)
        {
            AppendOutput("Record: " + this.CHEKBKID + "; Next_Deposit_Number failed validation...");
            testsPassed = false;
        }

        if (BasicTests.TestBoolean(this.INACTIVE) == false)
        {
            AppendOutput("Record: " + this.CHEKBKID + "; INACTIVE failed validation...");
            testsPassed = false;
        }


    }

}
