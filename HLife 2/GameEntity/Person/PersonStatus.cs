using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace HLife_2
{
    public class PersonStatus
    {
        public List<PersonStatusItem> StatusItems { get; set; }

        public PersonStatus()
        {
            this.StatusItems = XmlUtilities.CreateInstances<PersonStatusItem>(Game.Instance.ResourceController.BuildPath(@"Resources\PersonStatus.xml"));
        }

        /// <summary>
        /// Gets a requested status item, if it exists.
        /// </summary>
        /// <param name="name">Name of the PersonStatusItem.</param>
        /// <returns>Requested item. Null if not found.</returns>
        public PersonStatusItem GetItem(string name)
        {
            foreach(PersonStatusItem item in this.StatusItems)
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
            PersonStatusItem item = this.GetItem(name);

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
            PersonStatusItem item = this.GetItem(name);

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
        public bool SetValue(string name, object value)
        {
            PersonStatusItem item = this.GetItem(name);

            if (item != null)
            {
                item.Value = value;

                return true;
            }

            return false;
        }
    }

    public class PersonStatusItem
    {
        public object Value { get; set; }

        public string Name { get; set; }

        public Type ControlType { get; set; }

        public void ConvertFromXml()
        {
            // Set the control type. We assume it's within System.Windows.Forms.
            this.ControlType = Type.GetType("System.Windows.Forms." + ((XmlNode[])this.Value)[1].Value + "," + typeof(Label).Assembly.FullName);

            // Get the type of the Value from the "type" attribute.
            Type temp = Type.GetType(((XmlNode[])this.Value)[0].Value);

            // Convert the string value to the proper type.
            this.Value = Convert.ChangeType(((XmlNode[])this.Value)[2].Value, temp);
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
    }
}
