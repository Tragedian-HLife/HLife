using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HLife
{
    public class AIController
        : Controller
    {
        private Task[] _Tasks { get; set; }

        private int _ThreadCount { get; set; }

        public double ThreadTime { get; set; }

        public double Time { get; set; }

        public override void Initialize()
        {
            
        }

        public override void Update()
        {
            //Debug.WriteLine("Updating AI.");

            DateTime start = DateTime.Now;

            _ThreadCount = 0;
            this._Tasks = new Task[Game.Instance.City.NavMap.Nodes.Count];

            UpdateLocationRecursive(Game.Instance.City);

            Task.WaitAll(_Tasks);

            DateTime end = DateTime.Now;

            ThreadTime += (end - start).TotalMilliseconds;

            Debug.WriteLine("AI Update finished in " + (end - start) + ".");
        }

        public void UpdateSingleThread()
        {
            //Debug.WriteLine("Updating AI.");

            DateTime start = DateTime.Now;

            UpdateLocationRecursiveSingleThread(Game.Instance.City);

            DateTime end = DateTime.Now;

            Time += (end - start).TotalMilliseconds;

            Debug.WriteLine("AI Update finished in " + (end - start) + ".");
        }

        private void UpdateLocationRecursiveSingleThread(Location location)
        {
            //Debug.WriteLine("   Starting AI task for " + location.DisplayName);
            
            UpdateLocationOccupants(location);

            foreach (Location loc in location.Children)
            {
                this.UpdateLocationRecursiveSingleThread(loc);
            }
        }

        private void UpdateLocationRecursive(Location location)
        {
            //Debug.WriteLine("   Starting AI task for " + location.DisplayName);

            _Tasks[_ThreadCount] = Task.Factory.StartNew(() => UpdateLocationOccupants(location));
            _ThreadCount++;

            foreach (Location loc in location.Children)
            {
                this.UpdateLocationRecursive(loc);
            }
        }

        private void UpdateLocationOccupants(Location loc)
        {
            foreach(Person occupant in loc.Occupants)
            {
                if(occupant == Game.Instance.Player)
                {
                    continue;
                }

                //Debug.Write("       Updating AI for " + occupant.Name);

                DateTime start = DateTime.Now;

                occupant.Update();

                DateTime end = DateTime.Now;

                //Debug.Write(".\tTook " + (end- start) + ".\n");
            }
        }
    }
}
