using HLife.GameEntities.Locations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife.GameEntities.People.AI.Pathfinding
{
    /// <summary>
    /// A single node used for AI pathfinding
    /// </summary>
    public class PathNode
    {
        /// <summary>
        /// Worldspace coordinates of this node
        /// </summary>
        public Point Position { get; set; }

        public Location Location { get; set; }

        public PathNode backPointer;

        public double DistanceEstimated { get; set; }

        public double DistanceExact { get; set; }

        public double DistanceTotal { get; set; }

        public NavMap Map { get; set; }
        
        public bool IsVisited { get; set; }

        public List<PathNode> neighbors = new List<PathNode>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public PathNode(NavMap map)
        {
            Position = new Point(0, 0);
            Map = map;
        }

        public PathNode(NavMap map, Point position)
            : this(map)
        {
            this.Position = position;
        }

        public PathNode(NavMap map, Location location)
            : this(map)
        {
            this.Location = location;
        }
    }
}
