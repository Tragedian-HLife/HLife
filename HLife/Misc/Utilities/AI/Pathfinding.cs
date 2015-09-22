using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife
{
    /// <summary>
    /// Collection of pathfinding algorithms and utilities
    /// </summary>
    public static class PathfindingUtilities
    {
        public enum PathfindingAlgorithms
        {
            BreadthFirst,
            Dijkstra,
            BestFirst,
            aStar,
        }

        public static void ResetMap(ref NavMap map)
        {
            if (map == null)
                return;

            foreach(PathNode node in map.Nodes.Values)
            {
                node.DistanceEstimated = 0;
                node.DistanceExact = 0;
                node.DistanceTotal = 0;
                node.backPointer = null;
                node.IsVisited = false;
            }
        }

        public static PathNode ReversePath(PathNode path)
        {
            // Get the current first element.
            // This will end up being our new last element.
            PathNode root = path;

            // The last element shouldn't have a backpointer
            // so this will start as null.
            PathNode next = null;

            // While we have elements to reverse...
            while (root != null)
            {
                // Get the next element.
                PathNode tmp = root.backPointer;

                // Set the current element's backpointer to our previous element.
                root.backPointer = next;

                // Set the next previous element to the current element.
                next = root;

                // Set the current element to our new element.
                root = tmp;
            }

            // Return the reversed list.
            return next;
        }

        public static PathNode FindPath(PathfindingAlgorithms algorithm, PathNode start, PathNode target, NavMap map)
        {
            if (start == null
                || target == null
                || map == null)
                return null;

            switch (algorithm)
            {
                case PathfindingAlgorithms.BreadthFirst:
                    return PathfindingUtilities.ReversePath(BreadthFirst(ref start, ref target));

                case PathfindingAlgorithms.Dijkstra:
                    return PathfindingUtilities.ReversePath(Dijkstra(ref start, ref target));

                case PathfindingAlgorithms.BestFirst:
                    return PathfindingUtilities.ReversePath(BestFirst(ref start, ref target));

                default:
                case PathfindingAlgorithms.aStar:
                    return PathfindingUtilities.ReversePath(aStar(ref start, ref target));
            }
        }

        public static PathNode BreadthFirst(PathNode start, PathNode target)
        {
            return BreadthFirst(ref start, ref target);
        }

        public static PathNode BreadthFirst(ref PathNode start, ref PathNode target)
        {
            NavMap map = start.Map;
            PathfindingUtilities.ResetMap(ref map);

            List<PathNode> queuedNodes = new List<PathNode>();

            start.IsVisited = true;
            queuedNodes.Add(start);
            while (queuedNodes.Count() > 0)
            {
                PathNode node = queuedNodes[0];
                queuedNodes.Remove(node);

                if (node == target)
                {
                    return node;
                }

                foreach (PathNode neighbor in node.neighbors)
                {
                    if (!neighbor.IsVisited)
                    {
                        neighbor.IsVisited = true;

                        neighbor.backPointer = node;

                        if (neighbor == target)
                        {
                            return neighbor;
                        }

                        queuedNodes.Add(neighbor);
                    }
                }
            }

            return null;
        }

        public static PathNode Dijkstra(PathNode start, PathNode target)
        {
            return Dijkstra(ref start, ref target);
        }

        public static PathNode Dijkstra(ref PathNode start, ref PathNode target)
        {
            NavMap map = start.Map;
            PathfindingUtilities.ResetMap(ref map);

            List<PathNode> queuedNodes = new List<PathNode>();

            start.IsVisited = true;
            queuedNodes.Add(start);

            while (queuedNodes.Count() > 0)
            {
                PathNode node = queuedNodes[0];
                queuedNodes.Remove(node);

                if (node == target)
                {
                    return node;
                }

                foreach (PathNode neighbor in node.neighbors)
                {
                    //double currentDistance = (node.DistanceExact + GeometryUtilities.Distance(neighbor.Position, node.Position));
                    double currentDistance = node.Location.Edges.First(e => Location.Get(e.Node) == neighbor.Location).Cost;

                    if (!neighbor.IsVisited)
                    {
                        neighbor.IsVisited = true;

                        neighbor.DistanceExact = currentDistance;

                        neighbor.backPointer = node;

                        if (neighbor == target)
                        {
                            return neighbor;
                        }

                        queuedNodes.Add(neighbor);
                    }
                    else
                    {
                        if (currentDistance < neighbor.DistanceExact)
                        {
                            neighbor.DistanceExact = currentDistance;

                            neighbor.backPointer = node;
                        }
                    }
                }

                queuedNodes = queuedNodes.OrderBy(e => e.DistanceExact).ToList<PathNode>();
            }

            return null;
        }

        public static PathNode BestFirst(PathNode start, PathNode target)
        {
            return BestFirst(ref start, ref target);
        }

        public static PathNode BestFirst(ref PathNode start, ref PathNode target)
        {
            NavMap map = start.Map;
            PathfindingUtilities.ResetMap(ref map);

            List<PathNode> queuedNodes = new List<PathNode>();

            start.IsVisited = true;
            queuedNodes.Add(start);

            while (queuedNodes.Count() > 0)
            {
                PathNode node = queuedNodes[0];
                queuedNodes.Remove(node);

                if (node == target)
                {
                    return node;
                }

                foreach (PathNode neighbor in node.neighbors)
                {
                    double currentDistance = GeometryUtilities.Distance(target.Position, neighbor.Position);

                    if (!neighbor.IsVisited)
                    {
                        neighbor.IsVisited = true;

                        neighbor.DistanceEstimated = currentDistance;

                        neighbor.backPointer = node;

                        if (neighbor == target)
                        {
                            return neighbor;
                        }

                        queuedNodes.Add(neighbor);
                    }
                    else
                    {
                        if (currentDistance < neighbor.DistanceEstimated)
                        {
                            neighbor.DistanceEstimated = currentDistance;

                            neighbor.backPointer = node;
                        }
                    }
                }

                queuedNodes = queuedNodes.OrderBy(e => e.DistanceEstimated).ToList<PathNode>();
            }

            return null;
        }

        public static PathNode aStar(PathNode start, PathNode target)
        {
            return aStar(ref start, ref target);
        }

        public static PathNode aStar(ref PathNode start, ref PathNode target)
        {
            NavMap map = start.Map;
            PathfindingUtilities.ResetMap(ref map);

            List<PathNode> queuedNodes = new List<PathNode>();

            start.IsVisited = true;
            queuedNodes.Add(start);

            while (queuedNodes.Count() > 0)
            {
                PathNode node = queuedNodes[0];
                queuedNodes.Remove(node);

                if (node == target)
                    return node;

                foreach (PathNode neighbor in node.neighbors)
                {
                    //float distance_exact = (node.location - neighbor.location).Length() + node.distance_exact;
                    //double distance_exact = GeometryUtilities.Distance(start.Position, neighbor.Position);
                    //double distance_estimated = GeometryUtilities.Distance(target.Position, neighbor.Position);
                    double distance_exact = node.Location.Edges.First(e => Location.Get(e.Node) == neighbor.Location).Cost;
                    double distance_estimated = target.Location.TravelTime(neighbor.Location);
                    double distance_total = distance_exact + distance_estimated;

                    if (!neighbor.IsVisited)
                    {
                        neighbor.IsVisited = true;

                        neighbor.DistanceEstimated = distance_estimated;
                        neighbor.DistanceExact = distance_exact;
                        neighbor.DistanceTotal = distance_total;

                        neighbor.backPointer = node;

                        if (neighbor == target)
                            return neighbor;

                        queuedNodes.Add(neighbor);
                    }
                    else if (distance_total < neighbor.DistanceTotal)
                    {
                        neighbor.DistanceEstimated = distance_estimated;
                        neighbor.DistanceExact = distance_exact;
                        neighbor.DistanceTotal = distance_total;

                        neighbor.backPointer = node;
                    }
                }

                queuedNodes = queuedNodes.OrderBy(e => e.DistanceTotal).ToList<PathNode>();
            }

            return null;
        }
    }
}
