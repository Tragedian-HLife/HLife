using AI.Fuzzy.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Serialization;

namespace HLife
{
    public enum StatShapes
    {
        Symmetric,
        Asymmetric,
    }

    public enum StatStatuses
    {
        LowFatal,
        LowDanger,
        LowWarning,
        Nominal,
        HighWarning,
        HighDanger,
        HighFatal,
    }

    public enum StatBasicStatuses
    {
        Fatal,
        Danger,
        Warning,
        Nominal,
    }

    public class Stat
        : ICloneable
    {
        public object Value { get; set; }

        public string Name { get; set; }

        public Type ControlType { get; set; }

        public string Type { get; set; }

        [XmlIgnore]
        public bool IsNumeric { get; protected set; }

        public string Maximum { get; set; }

        public StatShapes StatShape { get; set; }

        public double LowestValue { get; set; }

        [XmlIgnore]
        public double LowerDangerValue { get; protected set; }

        [XmlIgnore]
        public double LowerNominalValue { get; protected set; }

        [XmlIgnore]
        public double HigherNominalValue { get; protected set; }

        [XmlIgnore]
        public double HigherDangerValue { get; protected set; }

        public double HighestValue { get; set; }

        public void ConvertFromXml()
        {
            // Set the control type. We assume it's within System.Windows.Forms.
            this.ControlType = System.Type.GetType("System.Windows.Controls." + ((XmlNode[])this.Value)[1].Value + "," + typeof(Label).Assembly.FullName);

            if (((XmlNode[])this.Value).Count() >= 4)
            {
                this.Maximum = ((XmlNode[])this.Value)[2].Value;
            }

            // Get the type of the Value from the "type" attribute.
            Type temp = System.Type.GetType(((XmlNode[])this.Value)[0].Value);

            // Convert the string value to the proper type.
            this.Value = Convert.ChangeType(((XmlNode[])this.Value)[((XmlNode[])this.Value).Count() - 1].Value, temp);

            try
            {
                double trash = (double)this.Value;

                this.IsNumeric = true;
            }
            catch
            {
                this.IsNumeric = false;
            }
        }

        /// <summary>
        /// Returns a new instance of this item's Windows Forms Control.
        /// Will still need to be cast to the correct Control type.
        /// </summary>
        /// <returns>Control instance.</returns>
        public Control GetControlInstance()
        {
            return (Control)Activator.CreateInstance(this.ControlType);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        
        public StatStatuses GetStatus()
        {
            double value = (double)this.Value;

            if (value <= this.LowestValue)
            {
                return StatStatuses.LowFatal;
            }
            else if(value <= this.LowerDangerValue)
            {
                return StatStatuses.LowDanger;
            }
            else if (value < this.LowerNominalValue)
            {
                return StatStatuses.LowWarning;
            }
            else if (value >= this.HighestValue)
            {
                return StatStatuses.HighFatal;
            }
            else if (value >= this.HigherDangerValue)
            {
                return StatStatuses.HighDanger;
            }
            else if (value > this.HigherNominalValue)
            {
                return StatStatuses.HighWarning;
            }
            else
            {
                return StatStatuses.Nominal;
            }
        }

        public StatBasicStatuses GetBasicStatus()
        {
            StatStatuses status = this.GetStatus();

            if(status == StatStatuses.LowFatal
                || status == StatStatuses.HighFatal)
            {
                return StatBasicStatuses.Fatal;
            }
            else if (status == StatStatuses.LowDanger
                || status == StatStatuses.HighDanger)
            {
                return StatBasicStatuses.Danger;
            }
            else if (status == StatStatuses.LowWarning
                || status == StatStatuses.HighWarning)
            {
                return StatBasicStatuses.Warning;
            }
            else
            {
                return StatBasicStatuses.Nominal;
            }
        }

        public double GetAbsoluteDifference(StatBasicStatuses averagingMethod)
        {
            double average = 0;

            if (this.StatShape == StatShapes.Symmetric)
            {
                if (averagingMethod == StatBasicStatuses.Nominal)
                {
                    average = (this.HigherNominalValue + this.LowerNominalValue) / 2.0;
                }
                else if (averagingMethod == StatBasicStatuses.Danger)
                {
                    average = (this.HigherDangerValue - this.LowerDangerValue) / 2.0;
                }
                else if (averagingMethod == StatBasicStatuses.Fatal)
                {
                    average = (this.HighestValue - this.LowestValue) / 2.0;
                }
            }
            else
            {
                average = this.HighestValue;
            }

            return (double)this.Value - average;
        }

        public double GetPercentDifference(StatBasicStatuses averagingMethod)
        {
            double absoluteDiff = this.GetAbsoluteDifference(averagingMethod);

            return absoluteDiff / (this.HighestValue - this.LowestValue);
        }

        public FuzzyVariable CreateFuzzyVariable()
        {
            this.LowestValue = 0;
            this.HighestValue = 100;

            return FuzzyLogicUtilities.CreateSymmetricTrapezoidalVariable("status", this.LowestValue, this.HighestValue,
                "lowFatal", "lowDanger", "lowWarning", "nominal", "highWarning", "highDanger", "highFatal");
        }

        public void FuzzyExample()
        {
            // Get the specified output variable.
            double result = FuzzyLogicUtilities.GetResult
            (
                // Add the ruleset.
                FuzzyLogicUtilities.AddRules
                (
                    // Create the fuzzy logic system.
                    FuzzyLogicUtilities.CreateMamdaniSystem
                    (
                        // Create the input sets.
                        new List<FuzzyVariable>()
                        {
                            this.CreateFuzzyVariable()
                        },

                        // Create the output sets.
                        new List<FuzzyVariable>()
                        {
                            FuzzyLogicUtilities.CreateSymmetricTrapezoidalVariable("basicStatus", 0.0, 1.0, "fatal", "danger", "warning", "nominal")
                        }
                    ),

                    // Define the rules.
                    "if (status is lowFatal) or (status is highFatal) then (basicStatus is fatal)",
                    "if (status is lowDanger) or (status is highDanger) then (basicStatus is danger)",
                    "if (status is lowWarning) or (status is highWarning) then (basicStatus is warning)",
                    "if (status is nominal) then (basicStatus is nominal)"
                ),

                // Specify the output variable's name.
                "basicStatus",

                // Set the input values.
                "status".Pair(30)
            );

            if(result < (2.0 / 7.0))
            {
                Debug.WriteLine("Fatal");
            }
            else if (result >= (2.0 / 7.0) && result < (4.0 / 7.0))
            {
                Debug.WriteLine("Danger");
            }
            if (result >= (4.0 / 7.0) && result < (6.0 / 7.0))
            {
                Debug.WriteLine("Warning");
            }
            if (result >= (6.0 / 7.0))
            {
                Debug.WriteLine("Nominal");
            }
        }
    }
}
