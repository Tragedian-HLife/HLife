using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    /// <summary>
    /// A single node used for AI pathfinding
    /// </summary>
    public class NavMap
    {
        public Dictionary<Location, PathNode> Nodes { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public NavMap()
        { }

        public void GenerateArbitraryMap(Dictionary<PathNode, List<PathNode>> edges)
        {
            this.Nodes = new Dictionary<Location, PathNode>();

            foreach(PathNode node in edges.Keys)
            {
                this.Nodes.Add(node.Location, node);
            }

            foreach (var node in edges)
            {
                foreach (var edge in node.Value)
                {
                    node.Key.neighbors.Add(edge);
                    edge.neighbors.Add(node.Key);
                }
            }
        }
    }
}
