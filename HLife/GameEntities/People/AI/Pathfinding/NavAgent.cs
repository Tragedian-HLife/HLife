using HLife.GameEntities.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HLife.GameEntities.People.AI.Pathfinding
{
    public class NavAgent
    {
        public AIAgent AIAgent { get; set; }

        public List<Location> Path { get; set; }

        [XmlIgnore]
        public EventHandler ReachedDestination;

        public NavAgent()
        {
            this.Path = new List<Location>();
        }

        public NavAgent(AIAgent aiAgent)
            : this()
        {
            this.AIAgent = aiAgent;
        }

        public void PathfindTo(Location destination)
        {
            this.Path = this.AIAgent.Agent.Location.PathfindTo(destination);
            this.Path.RemoveAt(0);
        }

        public void PathfindTo(string destination)
        {
            this.PathfindTo(Location.Get(destination));
        }

        public void Update()
        {
            if (this.Path != null && this.Path.Count > 0)
            {
                this.AIAgent.Agent.Location = this.Path.First();

                this.Path.RemoveAt(0);

                if(this.Path.Count == 0)
                {
                    this.OnArrive(EventArgs.Empty);
                }
            }
        }

        protected void OnArrive(EventArgs e)
        {
            EventHandler handler = this.ReachedDestination;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
