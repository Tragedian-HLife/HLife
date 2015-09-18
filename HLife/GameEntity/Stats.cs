using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Serialization;

namespace HLife
{
    public class Stats
    {
        public static List<Stat> DefaultPersonStatusItems { get; set; }

        public static List<Stat> DefaultPropStatusItems { get; set; }

        public List<Stat> StatEntries { get; set; }

        public Stats(Type ownerType)
        {
            if (ownerType == typeof(Person))
            {
                if (Stats.DefaultPersonStatusItems == null)
                {
                    Stats.DefaultPersonStatusItems = new List<Stat>();

                    foreach (Mod mod in ModController.GetModsByType("PersonStats"))
                    {
                        Stats.DefaultPersonStatusItems.AddRange(XmlUtilities.CreateInstances<Stat>(mod.Directory + @"\Stats\Stats.xml"));
                    }
                }

                this.StatEntries = Stats.DefaultPersonStatusItems.Clone();
            }
            else if (ownerType == typeof(Prop))
            {
                if (Stats.DefaultPropStatusItems == null)
                {
                    Stats.DefaultPropStatusItems = new List<Stat>();

                    foreach (Mod mod in ModController.GetModsByType("PropStats"))
                    {
                        Stats.DefaultPropStatusItems.AddRange(XmlUtilities.CreateInstances<Stat>(mod.Directory + @"\Stats\Stats.xml"));
                    }
                }

                this.StatEntries = Stats.DefaultPropStatusItems.Clone();
            }
        }

        /// <summary>
        /// Gets a requested status item, if it exists.
        /// </summary>
        /// <param name="name">Name of the PersonStatusItem.</param>
        /// <returns>Requested item. Null if not found.</returns>
        public Stat GetItem(string name)
        {
            foreach(Stat item in this.StatEntries)
            {
                if(item.Name == name)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if a requested status item exists.
        /// </summary>
        /// <param name="name">Name of the PersonStatusItem.</param>
        /// <returns>True if exists.</returns>
        public bool HasItem(string name)
        {
            if (this.GetItem(name) != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the value of a requested status item.
        /// Will need to be cast to the correct type.
        /// </summary>
        /// <param name="name">Name of the PersonStatusItem.</param>
        /// <returns>PersonStatusItem.Value</returns>
        public object GetValue(string name)
        {
            Stat item = this.GetItem(name);

            if(item != null)
            {
                return item.Value;
            }

            return null;
        }

        /// <summary>
        /// Returns the value of a requested status item.
        /// </summary>
        /// <typeparam name="T">Desired type of the value.</typeparam>
        /// <param name="name">Name of the PersonStatusItem.</param>
        /// <returns>(T)PersonStatusItem.Value</returns>
        public T GetValue<T>(string name)
        {
            Stat item = this.GetItem(name);

            if (item != null)
            {
                return (T)item.Value;
            }

            return default(T);
        }

        /// <summary>
        /// Sets the value of a requested status item.
        /// </summary>
        /// <param name="name">Name of the PersonStatusItem.</param>
        /// <param name="value">New value.</param>
        /// <returns>True if status item exists.</returns>
        public bool SetValue(string name, object value, bool relative = false)
        {
            Stat item = this.GetItem(name);

            if (item != null)
            {
                if(relative)
                {
                    try
                    {
                        item.Value = (double)item.Value + (double)value;
                    }
                    // If we're being a bitch and can't cast an int to a double...
                    catch(InvalidCastException)
                    {
                        item.Value = (double)item.Value + (int)value;
                    }
                    // If something genuinely goes wrong...
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    item.Value = value;
                }

                return true;
            }

            return false;
        }
    }

    public class Stat
        : ICloneable
    {
        public object Value { get; set; }

        public string Name { get; set; }

        public Type ControlType { get; set; }

        public string Type { get; set; }

        public string Maximum { get; set; }

        public void ConvertFromXml()
        {
            // Set the control type. We assume it's within System.Windows.Forms.
            this.ControlType = System.Type.GetType("System.Windows.Controls." + ((XmlNode[])this.Value)[1].Value + "," + typeof(Label).Assembly.FullName);

            if (((XmlNode[])this.Value).Count() >= 4)
            {
                this.Maximum = ((XmlNode[])this.Value)[3].Value;
            }

            // Get the type of the Value from the "type" attribute.
            Type temp = System.Type.GetType(((XmlNode[])this.Value)[0].Value);

            // Convert the string value to the proper type.
            this.Value = Convert.ChangeType(((XmlNode[])this.Value)[((XmlNode[])this.Value).Count() - 1].Value, temp);
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
    }
}
