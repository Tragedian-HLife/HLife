using HLife.GameEntities.People;
using HLife.GameEntities.Props;
using HLife.Mods;
using Newtonsoft.Json;
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

namespace HLife.GameEntities
{
    public class Stats
    {
        [JsonIgnore]
        [XmlIgnore]
        public static List<Stat> DefaultPersonStatusItems { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public static List<Stat> DefaultPropStatusItems { get; set; }

        public List<Stat> StatEntries { get; set; }
        
        public Stats()
        {
            this.StatEntries = new List<Stat>();

            if (Stats.DefaultPersonStatusItems == null)
            {
                Stats.DefaultPersonStatusItems = new List<Stat>();

                foreach (Mod mod in ModController.GetModsByType("PersonStats"))
                {
                    Stats.DefaultPersonStatusItems.AddRange(XmlUtilities.CreateInstances<Stat>(mod.Directory + @"\Stats\Stats.xml"));
                }
            }

            if (Stats.DefaultPropStatusItems == null)
            {
                Stats.DefaultPropStatusItems = new List<Stat>();

                foreach (Mod mod in ModController.GetModsByType("PropStats"))
                {
                    Stats.DefaultPropStatusItems.AddRange(XmlUtilities.CreateInstances<Stat>(mod.Directory + @"\Stats\Stats.xml"));
                }
            }
        }

        public Stats(Type ownerType)
            : this()
        {
            if (ownerType == typeof(Person))
            {
                this.StatEntries = Stats.DefaultPersonStatusItems.Clone();
            }
            else if (ownerType == typeof(Prop))
            {
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

        public List<Stat> GetStatsByStatus(StatStatuses status)
        {
            List<Stat> stats = new List<Stat>();

            foreach (Stat stat in this.StatEntries.Where(e => e.IsNumeric))
            {
                if (stat.GetStatus() == status)
                {
                    stats.Add(stat);
                }
            }

            return stats;
        }

        public List<Stat> GetStatsByBasicStatus(StatBasicStatuses status)
        {
            List<Stat> stats = new List<Stat>();

            foreach (Stat stat in this.StatEntries.Where(e => e.IsNumeric))
            {
                if(stat.GetBasicStatus() == status)
                {
                    stats.Add(stat);
                }
            }

            return stats;
        }

        public Stat GetWorstStatByStatus(StatBasicStatuses status)
        {
            List<Stat> stats = this.GetStatsByBasicStatus(status);

            if (stats.Count == 0)
            {
                return null;
            }

            Stat maxStat = null;
            double maxDiff = 0;

            foreach (Stat stat in stats)
            {
                double diff = stat.GetAbsoluteDifference(StatBasicStatuses.Fatal);

                if (Math.Abs(diff) > Math.Abs(maxDiff))
                {
                    maxDiff = diff;
                    maxStat = stat;
                }
            }

            return maxStat;
        }

        public Stat GetWorstStat(StatBasicStatuses highestInclusiveGroup)
        {
            Stat maxStat = null;

            maxStat = this.GetWorstStatByStatus(StatBasicStatuses.Fatal);

            if(maxStat == null 
                && highestInclusiveGroup != StatBasicStatuses.Fatal)
            {
                maxStat = this.GetWorstStatByStatus(StatBasicStatuses.Danger);

                if (maxStat == null
                    && highestInclusiveGroup != StatBasicStatuses.Danger)
                {
                    maxStat = this.GetWorstStatByStatus(StatBasicStatuses.Warning);

                    if (maxStat == null
                        && highestInclusiveGroup != StatBasicStatuses.Warning)
                    {
                        maxStat = this.GetWorstStatByStatus(StatBasicStatuses.Nominal);
                    }
                }
            }

            return maxStat;
        }
    }
}
