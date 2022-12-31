using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace ExtractPartitionData.Models.DynamicsGPMigrationData;
public class _DynamicsGPMigrationRecord_class
{
    [XmlIgnore]
    protected static mainForm? _referringWindow = null;

    [XmlAttribute("NOTEINDX")]
    public string? NOTEINDX { get; set; }

    [XmlAttribute("DEX_ROW_ID")]
    public string? DEX_ROW_ID { get; set; }

    /// <summary>
    /// Base constructor; no AppendOutput available
    /// </summary>
    public _DynamicsGPMigrationRecord_class()
    {
        //this.NOTEINDX = null;
    }

    /// <summary>
    /// Base constructor with AppendOutput(string) routine expected on mainForm; nullable
    /// </summary>
    /// <param name="referringWindow">Referring window; usually (this)</param>
    public _DynamicsGPMigrationRecord_class(mainForm? referringWindow) : base()
    {
        if (referringWindow != null)
        {
            _referringWindow = referringWindow;
        }
    }

    /// <summary>
    /// Conditionally calls ReferringWindow.AppendOutput(string) if configured
    /// </summary>
    /// <param name="output">String value to append to output</param>
    /// <returns>True: appended output to routine; false: no output configured</returns>
    public bool AppendOutput(string output)
    {
        if (_referringWindow == null) { return false; }
        else { _referringWindow.AppendOutput(output); return true; }
    }

    public virtual bool PerformTests()
    {
        float tempFloat;
        long temp;
        bool _initialReturn = true;

        // implement base tests applicable to all types

        foreach (PropertyInfo prop in this.GetType().GetProperties())
        {
            var propertyType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
            if (propertyType == typeof(string))
            {
                if (prop.GetValue(this) != null)
                {
                    if (prop.GetValue(this).ToString().Contains("\t"))
                    {
                        AppendOutput("Problem with row; field '" + prop.Name.Trim() + "' contains a [tab] character...");
                        _initialReturn = false;
                    }
                    else if (prop.GetValue(this).ToString().Contains("\r"))
                    {
                        AppendOutput("Problem with row; field '" + prop.Name.Trim() + "' contains a [carriage return] character...");
                        _initialReturn = false;
                    }
                    else if (prop.GetValue(this).ToString().Contains("\n"))
                    {
                        AppendOutput("Problem with row; field '" + prop.Name.Trim() + "' contains a [new line] character...");
                        _initialReturn = false;
                    }
                }
            }
        }

        // test: can NOTEINDX be made into a float if it is not null ?
        if (this.NOTEINDX != null)
        {
            bool test8 = float.TryParse(this.NOTEINDX, out tempFloat);
            Debug.WriteLine("test8: " + test8.ToString() + "; tempFloat = " + tempFloat.ToString());
            if (!test8)
            {
                AppendOutput("Problem with row; NOTEINDX failed validation...");
                _initialReturn = false;
            }
        }

        // test: can DEX_ROW_ID be made into a number?
        bool test10 = Int64.TryParse(this.DEX_ROW_ID, out temp);
        if (!test10)
        {
            AppendOutput("Problem with row; DEX_ROW_ID failed validation...");
            _initialReturn = false;
        }

        return _initialReturn;
    }
}
