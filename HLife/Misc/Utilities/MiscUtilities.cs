using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    /// <summary>
    /// Collection of random utilities.
    /// </summary>
    public static class MiscUtilities
    {
        /// <summary>
        /// Global random to avoid repeat values.
        /// </summary>
        public static Random Rand = new Random();

        /// <summary>
        /// Gets a random member of an enum.
        /// </summary>
        /// <typeparam name="T">Enum to pull from.</typeparam>
        /// <returns>Random member of T.</returns>
        public static T GetRandomEnum<T>()
        {
            Array values = Enum.GetValues(typeof(T));

            return (T)values.GetValue(MiscUtilities.Rand.Next(values.Length));
        }

        public static T GetRandomEntry<T>(List<T> list)
        {
            if(list.Count == 0)
            {
                return default(T);
            }

            return list[MiscUtilities.Rand.Next(list.Count)];
        }
    }
}
