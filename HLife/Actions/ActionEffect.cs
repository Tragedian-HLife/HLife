using HLife.GameEntities.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HLife.Actions
{
    /// <summary>
    /// Represents an atomic effect of an action.
    /// </summary>
    public class ActionEffect
    {
        /// <summary>
        /// Stat name that this effect affects.
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// The numeric value that this effect will apply to the stat.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// True for a relative Value.
        /// eg. A Value of -5 would subtract 5 from the stat's current value.
        /// </summary>
        public bool ValueIsRelative { get; set; }

        /// <summary>
        /// True to multiply the Value to the stat's current value.
        /// False to add the Value to the stat's current value.
        /// </summary>
        public bool RelativeOperationIsMultiplication { get; set; }

        /// <summary>
        /// Custom condition logic to check if this effect applies to given arguments.
        /// </summary>
        [XmlIgnore]
        public Func<ActionEventArgs, bool> Condition { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ActionEffect()
        {
            this.Value = 0;
            this.ValueIsRelative = true;
            this.RelativeOperationIsMultiplication = false;

            // Set the condition to always true, by default.
            this.Condition = delegate (ActionEventArgs args) { return true; };
        }

        /// <summary>
        /// Basic constructor for a relative value.
        /// </summary>
        /// <param name="name">Stat name.</param>
        /// <param name="value">Relative value.</param>
        public ActionEffect(string name, double value)
            : this()
        {
            this.ItemName = name;
            this.Value = value;
        }

        /// <summary>
        /// Basic constructor to allow for non-relative or multiplicative values.
        /// </summary>
        /// <param name="name">Stat name.</param>
        /// <param name="value">Stat value.</param>
        /// <param name="relative">Whether Value is to be relative.</param>
        /// <param name="isMultiplication">Whether the relative value is to be multiplied.</param>
        public ActionEffect(string name, double value, bool relative, bool isMultiplication)
            : this(name, value)
        {
            this.ValueIsRelative = relative;
            this.RelativeOperationIsMultiplication = isMultiplication;
        }

        /// <summary>
        /// Complex constructor to manually set every property.
        /// </summary>
        /// <param name="name">Stat name.</param>
        /// <param name="value">Stat value.</param>
        /// <param name="relative">Whether Value is to be relative.</param>
        /// <param name="isMultiplication">Whether the relative value is to be multiplied.</param>
        /// <param name="condition">Custom logic to check if the effect applies.</param>
        public ActionEffect(string name, double value, bool relative, bool isMultiplication, Func<ActionEventArgs, bool> condition)
            : this(name, value, relative, isMultiplication)
        {
            this.Condition = condition;
        }

        /// <summary>
        /// Apply this effect to the given Person.
        /// </summary>
        /// <param name="subject">Person to whom this effect should be applied.</param>
        /// <returns>True if the effect was applied successfully. False if the stat couldn't be found.</returns>
        public bool Apply(Person subject)
        {
            // If we have a Person...
            if (subject != null)
            {
                // If the effect isn't multiplicative...
                if (!this.RelativeOperationIsMultiplication)
                {
                    // Let the Stat logic handle everything.
                    return subject.Stats.SetValue(this.ItemName, this.Value, this.ValueIsRelative);
                }
                else
                {
                    // Set the stat to a manually multiplied value.
                    return subject.Stats.SetValue(this.ItemName, subject.Stats.GetValue<double>(this.ItemName) * this.Value, false);
                }
            }

            // Report failure.
            return false;
        }
    }
}
