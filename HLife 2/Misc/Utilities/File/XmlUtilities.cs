using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace HLife_2
{
    /// <summary>
    /// XML utilities.
    /// </summary>
    public static class XmlUtilities
    {
        /// <summary>
        /// Reads an XML file and returns a new object with the read settings.
        /// If T has a ConvertFromXml() member, it will be called automatically.
        /// </summary>
        /// <typeparam name="T">Type of object to create. The Name of the Type will be the Node we read from.</typeparam>
        /// <param name="file">Path to the XML file.</param>
        /// <returns>Object of type T with XML's values.</returns>
        public static T CreateInstance<T>(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlTextReader reader = new XmlTextReader(Path.Combine(Application.StartupPath, file));

            reader.ReadToDescendant(typeof(T).Name);

            T read = (T)serializer.Deserialize(reader.ReadSubtree());

            reader.Close();

            if (typeof(T).GetMethods().Count(e => e.Name == "ConvertFromXml") > 0)
            {
                typeof(T).GetMethod("ConvertFromXml").Invoke(read, null);
            }

            return read;
        }

        /// <summary>
        /// Reads an XML file and returns a list of new objects with the read settings.
        /// If T has a ConvertFromXml() member, it will be called automatically.
        /// </summary>
        /// <typeparam name="T">Type of objects to create. The Name of the Type will be the Node we read from.</typeparam>
        /// <param name="file">Path to the XML file.</param>
        /// <returns>List of objects of type T with XML's values.</returns>
        public static List<T> CreateInstances<T>(string file)
        {
            List<T> read = new List<T>();
            
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            XmlTextReader reader = new XmlTextReader(Path.Combine(Application.StartupPath, file));

            reader.ReadToDescendant(typeof(T).Name);
            read.Add((T)serializer.Deserialize(reader.ReadSubtree()));

            while (reader.ReadToNextSibling(typeof(T).Name))
            {
                read.Add((T)serializer.Deserialize(reader.ReadSubtree()));
            }

            reader.Close();

            if (typeof(T).GetMethods().Count(e => e.Name == "ConvertFromXml") > 0)
            {
                read.ForEach(e => typeof(T).GetMethod("ConvertFromXml").Invoke(e, null));
            }

            return read;
        }
    }
}
