using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife_2
{
    /// <summary>
    /// Holds general game settings.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Starting population of the city.
        /// Tells the PersonController how many NPCs to generate.
        /// </summary>
        public int StartingPopulation { get; set; }

        /// <summary>
        /// Minimum number of people in a family.
        /// The PersonController won't generate a family with fewer members than this.
        /// </summary>
        public int MinFamilySize { get; set; }

        /// <summary>
        /// Maximum number of people in a family.
        /// The PersonController won't generate a family with more members than this.
        /// </summary>
        public int MaxFamilySize { get; set; }
    }
}
