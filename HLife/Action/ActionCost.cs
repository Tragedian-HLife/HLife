using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    /// <summary>
    /// Set of estimated costs to perform an action under
    /// given conditions.
    /// </summary>
    public class ActionCost
    {
        /// <summary>
        /// Number of edges the doer must traverse.
        /// </summary>
        public double NodalDistance { get; set; }

        /// <summary>
        /// Time, in seconds, that the doer must spend traveling.
        /// </summary>
        public double TravelTime { get; set; }

        /// <summary>
        /// Time, in seconds, to complete the performance of the action.
        /// </summary>
        public double PerformanceTime { get; set; }

        /// <summary>
        /// Total time, in seconds, to complete this action.
        /// </summary>
        public double TotalTime
        {
            get
            {
                // Add all of the times.
                return this.TravelTime
                    + this.PerformanceTime;
            }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ActionCost()
        {
            this.NodalDistance = 0;
            this.TravelTime = 0;
            this.PerformanceTime = 0;
        }
    }
}
