using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    /// <summary>
    /// Defailt settings for a Prop.
    /// </summary>
    public class PropTemplate
    {
        /// <summary>
        /// Reference name. MUST BE UNIQUE.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Printable name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Path to the image file.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Description of this Prop.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Performable Actions.
        /// </summary>
        public List<string> Actions { get; set; }

        /// <summary>
        /// Clerical categories.
        /// </summary>
        public List<string> Categories { get; set; }

        public Mod Source { get; set; }
        
        public int MaxOccupancy { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PropTemplate()
        {
            this.Name = "{PROP}";
            this.DisplayName = "{PROP}";
            this.Actions = new List<string>();
            this.Categories = new List<string>();
        }
    }
}
