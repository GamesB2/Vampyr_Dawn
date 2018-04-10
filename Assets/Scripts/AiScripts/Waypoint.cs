using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WayPointType;

namespace WayPointType
{
    public static class PointType
    {
        public enum WAYPOINT_TYPE
        {
            ATTACKING,
            READY_TO_FIGHT,
            RETREATING
        };
    }
}


public class Waypoint : MonoBehaviour {

    public List<Waypoint> m_ConnectedWaypoints;
    [SerializeField]
    private bool m_ShowDebugLines = false;
    [SerializeField]
    private PointType.WAYPOINT_TYPE m_Type;
    private GameObject m_Owner = null;

    public void FixedUpdate()
    {
        if (m_ShowDebugLines)
        {
            foreach (Waypoint connected in m_ConnectedWaypoints)
            {
                if (connected != null)
                {
                    Debug.DrawLine(this.transform.position, connected.transform.position, Color.green);
                }
            }
        }
    }

    public List<Waypoint> GetConnectedWaypoints()
    {
        return m_ConnectedWaypoints;
    }

    public PointType.WAYPOINT_TYPE GetWaypointType()
    {
        return m_Type;
    }

    public void SetOwner(GameObject newOwner)
    {
        m_Owner = newOwner;
    }

    public bool IsOwned()
    {
        return m_Owner != null;
    }
}
