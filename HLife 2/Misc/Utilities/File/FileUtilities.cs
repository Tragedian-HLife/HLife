using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLife_2
{
    /// <summary>
    /// Utilities for handling files.
    /// </summary>
    public static class FileUtilities
    {
        /// <summary>
        /// Read each line of a file.
        /// </summary>
        /// <param name="file">Path to the file.</param>
        /// <returns>List of lines from the file.</returns>
        public static List<string> ReadFile(string file)
        {
            string line;
            List<string> lines = new List<string>();
            
            System.IO.StreamReader fh = new System.IO.StreamReader(file);
            while ((line = fh.ReadLine()) != null)
            {
                lines.Add(line);
            }

            fh.Close();

            return lines;
        }
    }
}
