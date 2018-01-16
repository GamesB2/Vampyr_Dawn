using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WayPointType;

namespace PathFinding
{
    /*public static class AStar
	{
		public List<Waypoint> findPath(Vector3 startPos, Vector3 endPos)
		{
			
		}
	}*/

    public static class DumbSearch
    {
        public static List<Waypoint> findPath(Vector3 startPos, Vector3 endPos)
        {
            // Because the waypoint system has no dead ends it is possible for a dumb search to just return a list of consecutibely closest and connected way points
            List<Waypoint> returnPath = new List<Waypoint>();
            returnPath.Capacity = 10;
            uint loops = 0;
            Waypoint start = WayPointManager.ClosestWaypoint(startPos, WayPointManager.GetAllWayPoints(), false);
            Waypoint end = WayPointManager.ClosestWaypoint(endPos, WayPointManager.GetAllWayPoints(), false);

            returnPath.Add(start);
            List<Waypoint> connected = returnPath[returnPath.Count - 1].GetConnectedWayPoints();

			/* The waypoint network has no obstacles or dead ends inside it, so for efficiency I wrote this
			 * dumb pathfinder. All it does is get the starting waypoint, the end waypoint, and cycle through
			 * all connections. If the list of connections contains the end waypoint: break and add the end to the path
			 * otherwise look for the nearest waypoint to the end target in the list of connected waypoints.
			*/

			for (int i = 0; i < returnPath.Capacity - 1; i++)
			{
				if (ListContainsWaypoint (end, connected))
				{
					break;
				}
				returnPath.Add(WayPointManager.ClosestWaypoint(end.transform.position, connected, false));
				connected = returnPath[returnPath.Count - 1].GetConnectedWayPoints();
			}
            returnPath.Add(end);
            returnPath.TrimExcess();

            return returnPath;
        }

        private static bool ListContainsWaypoint(Waypoint target, List<Waypoint> list)
        {
            foreach (Waypoint wp in list)
            {
                if (wp == target)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

