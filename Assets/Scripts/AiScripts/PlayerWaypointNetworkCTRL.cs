using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringBehaviours;

public class PlayerWaypointNetworkCTRL : MonoBehaviour {

    private GameObject m_PlayerRef;

    [SerializeField]
    private Transform m_AttackingWaypointCTRL;

    [SerializeField]
    private Transform m_ReadyWaypointCTRL;

    [SerializeField]
    private Transform m_RetreatingWaypointCTRL;

    private Vector3 m_PlayerRightOffset = new Vector3(-0.45f, 0.65f, 0.0f);
    private Vector3 m_PlayerLeftOffset = new Vector3(0.45f, 0.65f, 0.0f);
    public GameObject m_BoundingBox;

    public float boundingBoxXMin = 0;
    public float boundingBoxXMax = 0;
    public float boundingBoxYMin = 0;
    public float boundingBoxYMax = 0;

    void Start()
    {
        m_PlayerRef = GameManager.GetPlayer();

        m_BoundingBox = GameObject.FindGameObjectWithTag("BoundingBox");
        BoxCollider2D collider = m_BoundingBox.GetComponent<BoxCollider2D>();
        boundingBoxXMin = m_BoundingBox.transform.position.x - (collider.size.x / 2);
        boundingBoxXMax = m_BoundingBox.transform.position.x + (collider.size.x / 2);
        boundingBoxYMin = m_BoundingBox.transform.position.y - (collider.size.y / 2);
        boundingBoxYMax = m_BoundingBox.transform.position.y + (collider.size.y / 2);
    }

    /* This probably shouldn't be LateUpdate, but when it wasn't the waypoints moved in a jittery mannor.
	 * The problem is that the AI might update ahead of this in update so they'll be moving to where this 
	 * system was. this should move in Update and the AI should move in LateUpdate - but it currently works
	 * so change and test this when there aren't more important things to do.
	*/
    void LateUpdate()
    {
        Vector3 playerPos;
        if (m_PlayerRef.GetComponent<Character2D>().m_FacingRight)
        {
            playerPos = m_PlayerRef.transform.position + m_PlayerRightOffset;
        }
        else
        {
            playerPos = m_PlayerRef.transform.position + m_PlayerLeftOffset;
        }

        m_AttackingWaypointCTRL.position = playerPos;

        Vector3 readyAdd = Steering.Arrive(playerPos, m_ReadyWaypointCTRL.position, 10.0f, 1.0f) * Time.deltaTime;

        //bounding box constraints on the ready to fight nodes.
        if ((m_ReadyWaypointCTRL.position.y + 0.35f) >= boundingBoxYMax && readyAdd.y > 0)
        {
            readyAdd.y = 0.0f;
        }
        else if ((m_ReadyWaypointCTRL.position.y - 1.65f) <= boundingBoxYMin && readyAdd.y < 0)
        {
            readyAdd.y = 0.0f;
        }


        m_ReadyWaypointCTRL.position += readyAdd;
    }
}
