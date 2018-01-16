using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WayPointType;

public class WayPointManager : MonoBehaviour
{
    private static WayPointManager m_WayPointManager = null;
    private static List<Waypoint> m_AllwayPoints = null;
    private static List<Waypoint> m_RetreatWayPoints = null;
    private static List<Waypoint> m_ReadyWayPoints = null;
    private static List<Waypoint> m_AttackWayPoints = null;

    void Awake()
    {
		/* When the scene is initialised this fetches every gameobject tagged "Way_Point" and
		 * get's their waypoint component. This means that any waypoints in a scene must be
		 * there when the scene is saved to disk so it can be loaded with the scene and found here.
		*/

        m_AllwayPoints = new List<Waypoint>();
        m_RetreatWayPoints = new List<Waypoint>();
        m_ReadyWayPoints = new List<Waypoint>();
        m_AttackWayPoints = new List<Waypoint>();
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Way_Point");
        foreach (GameObject wp in waypoints)
        {
            Waypoint wayPoint = wp.GetComponent<Waypoint>();
            if (wayPoint != null)
            {
                m_AllwayPoints.Add(wayPoint);
                Debug.Log(wp.name);

                PointType.WAYPOINT_TYPE type = wayPoint.GetWayPointType();
                switch (type)
                {
                    case PointType.WAYPOINT_TYPE.ATTACKING:
                        {
                            m_AttackWayPoints.Add(wayPoint);
                            break;
                        }
                    case PointType.WAYPOINT_TYPE.READY_TO_FIGHT:
                        {
                            m_ReadyWayPoints.Add(wayPoint);
                            break;
                        }
                    case PointType.WAYPOINT_TYPE.RETREATING:
                        {
                            m_RetreatWayPoints.Add(wayPoint);
                            break;
                        }
                }
            }
        }
    }

    static public WayPointManager GetInstance()
    {
        if (m_WayPointManager == null) m_WayPointManager = new WayPointManager();
        return m_WayPointManager;
    }

    static public List<Waypoint> GetAllWayPoints()
    {
        return m_AllwayPoints;
    }

    static public List<Waypoint> GetAllRetreatWayPoints()
    {
        return m_RetreatWayPoints;
    }

    static public List<Waypoint> GetAllReadyToAttackWayPoints()
    {
        return m_ReadyWayPoints;
    }

    static public List<Waypoint> GetAllAttackWayPoints()
    {
        return m_AttackWayPoints;
    }

	/* This method finds the closest wapoint to a specified position. The reason why you pass it a list of
	 * waypoints rather than just using the member level variables of this class is for things like the
	 * pathfinder - where given a waypoint's list of connected waypoints, find the closest waypoint to the
	 * target.
	*/
	public static Waypoint ClosestWaypoint(Vector3 position, List<Waypoint> waypoints, bool freeWaypointsOnly)
    {
        float nearestDistance = float.MaxValue;
        Waypoint nearestWaypoint = null;

        foreach (Waypoint wp in waypoints)
        {
            float dist = (position - wp.transform.position).magnitude;
            if (dist < nearestDistance)
            {
				if (freeWaypointsOnly)
				{
					if (!wp.IsOwned ()) {
						nearestDistance = dist;
						nearestWaypoint = wp;
					}
					else
					{
						continue;
					}
				}
				else
				{
					nearestDistance = dist;
					nearestWaypoint = wp;
				}
            }
        }
        return nearestWaypoint;
    }
}
