﻿using HLife.GameEntities.Locations;
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
    public class NavMap
    {
        public SerializableDictionary<Location, PathNode> Nodes { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public NavMap()
        { }

        public void GenerateArbitraryMap(SerializableDictionary<PathNode, List<PathNode>> edges)
        {
            this.Nodes = new SerializableDictionary<Location, PathNode>();

            foreach(PathNode node in edges.Keys)
            {
                this.Nodes.Add(node.Location, node);
            }

            foreach (var node in edges)
            {
                foreach (var edge in node.Value)
                {
                    if (!node.Key.neighbors.Contains(edge))
                        node.Key.neighbors.Add(edge);

                    if (!edge.neighbors.Contains(node.Key))
                        edge.neighbors.Add(node.Key);
                }
            }
        }
    }
}
