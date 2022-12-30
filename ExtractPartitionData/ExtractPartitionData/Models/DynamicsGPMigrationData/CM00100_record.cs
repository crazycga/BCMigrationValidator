using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExtractPartitionData.Models.DynamicsGPMigrationData;

[XmlRootAttribute("row")]
public class CM00100_record : _DynamicsGPMigrationRecord_class
{
    [XmlAttribute("BANKID")]
    public string? BANKID { get; set; }

    [XmlAttribute("BANKNAME")]
    public string? BANKNAME { get; set; }

    [XmlAttribute("ADDRESS1")]
    public string? ADDRESS1 { get; set; }

    [XmlAttribute("ADDRESS2")]
    public string? ADDRESS2 { get; set; }

    [XmlAttribute("ADDRESS3")]
    public string? ADDRESS3 { get; set; }

    [XmlAttribute("CITY")]
    public string? CITY { get; set; }

    [XmlAttribute("STATE")]
    public string? STATE { get; set; }

    [XmlAttribute("ZIPCODE")]
    public string? ZIPCODE { get; set; }

    [XmlAttribute("COUNTRY")]
    public string? COUNTRY { get; set; }

    [XmlAttribute("PHNUMBR1")]
    public string? PHNUMBR1 { get; set; }

    [XmlAttribute("PHNUMBR2")]
    public string? PHNUMBR2 { get; set; }

    [XmlAttribute("PHONE3")]
    public string? PHONE3 { get; set; }

    [XmlAttribute("FAXNUMBR")]
    public string? FAXNUMBR { get; set; }

    [XmlAttribute("TRNSTNBR")]
    public string? TRNSTNBR { get; set; }

    [XmlAttribute("BNKBRNCH")]
    public string? BNKBRNCH { get; set; }

    [XmlAttribute("NOTEINDX")]
    public string? NOTEINDX { get; set; }

    [XmlAttribute("DDTRANUM")]
    public string? DDTRANUM { get; set; }

    public CM00100_record() : base()
    {
    }

    public CM00100_record(mainForm referringForm) : base(referringForm)
    {
    }

    public static CM00100_record Parse(string incomingXML)
    {
        CM00100_record tempReturn = new CM00100_record();

        var mySerializer = new XmlSerializer(typeof(CM00100_record));
        using (var myStream = new StringReader(incomingXML))
        {
            tempReturn = (CM00100_record)mySerializer.Deserialize(myStream);
        }

        return tempReturn;
    }

    public override bool PerformTests()
    {
        if (string.IsNullOrEmpty(this.BANKID))
        {
            throw new ArgumentException("Object is not initialized");
        }

        bool testsPassed = true;

        // Tests:
        // 1. base tests
        // 2. can PHNUMBR1 be made into a number?
        // 3. can PHNUMBR2 be made into a number?
        // 4. can PHONE3 be made into a number?
        // 5. can FAXNUMBR be made into a number?
        // 6. can TRNSTNBR be made into a number?
        // 7. can BNKBRNCH be made into a number?
        // 9. can DDTRANUM be made into a number?

        testsPassed = base.PerformTests();
        if (!testsPassed) 
        {
            AppendOutput("Record: " + this.BANKID + " failed universal validation...");
            return testsPassed; 
        }

        long temp = 0;
        float tempFloat = 0.00F;

        // test 2: can PHNUMBR1 be made into a number?
        bool test2 = Int64.TryParse(this.PHNUMBR1, out temp);
        if (!test2) 
        {
           AppendOutput("Record: " + this.BANKID + "; field: PHNUMBR1 failed validation..."); 
            testsPassed = false;
        }

        // test 3: can PHNUMBR2 be made into a number?
        bool test3 = Int64.TryParse(this.PHNUMBR2, out temp);
        if (!test3)
        {
            AppendOutput("Record: " + this.BANKID + "; field: PHNUMBR2 failed validation..."); 
            testsPassed = false;
        }

        // test 4: can PHONE3 be made into a number?
        bool test4 = Int64.TryParse(this.PHONE3, out temp);
        if (!test4)
        {
            AppendOutput("Record: " + this.BANKID + "; field: PHONE3 failed validation..."); 
            testsPassed = false;
        }

        // test 5: can FAXNUMBR be made into a number?
        bool test5 = Int64.TryParse(this.FAXNUMBR, out temp);
        if (!test5)
        {
            AppendOutput("Record: " + this.BANKID + "; field: FAXNUMBR failed validation...");
            testsPassed = false;
        }

        // test 6: can TRNSTNBR be made into a number?
        bool test6 = Int64.TryParse(this.TRNSTNBR, out temp);
        if (!test6)
        {
            AppendOutput("Record: " + this.BANKID + "; field: TRNSTNBR failed validation...");
            testsPassed = false;
        }

        // test 7: can BNKBRNCH be made into a number?
        bool test7 = Int64.TryParse(this.BNKBRNCH, out temp);
        if (!test7)
        {
            AppendOutput("Record: " + this.BANKID + "; field: BNKBRNCH failed validation...");
            testsPassed = false;
        }

        // test 9: can DDTRANUM be made into a number?
        bool test9 = Int64.TryParse(this.DDTRANUM, out temp);
        if (!test9)
        {
            AppendOutput("Record: " + this.BANKID + "; field: DDTRANUM failed validation...");
            testsPassed = false;
        }

        return testsPassed;
    }
}


