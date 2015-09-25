using HLife.GameEntities.Locations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HLife.GameEntities.People.AI
{
    public class AIController
        : Controller
    {
        private Task[] _Tasks { get; set; }

        private int _ThreadCount { get; set; }

        public double ThreadTime { get; set; }

        public override void Initialize()
        {
            
        }

        public override void Update()
        {
            //Debug.WriteLine("Updating AI.");

            DateTime start = DateTime.Now;

            _ThreadCount = 0;
            this._Tasks = new Task[Game.Instance.City.NavMap.Nodes.Count];

            UpdateLocation(Game.Instance.City);

            Task.WaitAll(_Tasks);

            ThreadTime = (DateTime.Now - start).TotalMilliseconds;

            Debug.WriteLine("AI Update finished in " + ThreadTime + ".");
        }

        private void UpdateLocation(Location location)
        {
            //Debug.WriteLine("   Starting AI task for " + location.DisplayName);

            _Tasks[_ThreadCount] = Task.Factory.StartNew(() => UpdateLocationOccupants(location));
            _ThreadCount++;

            foreach (Location loc in location.Children)
            {
                this.UpdateLocation(loc);
            }
        }

        private void UpdateLocationOccupants(Location loc)
        {
            for (int i = 0; i < loc.Occupants.Count; i++)
            {
                if (loc.Occupants[i] == Game.Instance.Player)
                {
                    continue;
                }

                //Debug.Write("       Updating AI for " + occupant.Name);

                DateTime start = DateTime.Now;

                loc.Occupants[i].Update();

                DateTime end = DateTime.Now;

                //Debug.Write(".\tTook " + (end- start) + ".\n");
            }
        }
    }
}
