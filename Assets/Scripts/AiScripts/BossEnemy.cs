﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteeringBehaviours;
using EnemyBehaviour;
using PathFinding;

namespace EnemyBehaviour
{
    public static class BossBehaviourType
    {
        public enum ENEMY_BEHAVIOUR
        {
            ATTACK,
            READY_TO_FIGHT,
            RETREAT
        };
    }
}

public class BossEnemy : MonoBehaviour
{
    [SerializeField]
    protected float m_Speed = 0;
    [SerializeField]
    protected float m_SteeringSensitivity = 0;
    protected Transform m_PlayertransformRef;
	[SerializeField]
    protected BossBehaviourType.ENEMY_BEHAVIOUR m_Behaviour;

	[SerializeField]
	protected float m_WaypointSensitivity = 0.1f;
	[SerializeField]
	protected float m_TimeMax;
	protected Health m_Health = null;
	protected float m_PersistantTime;
	protected List<Waypoint> m_Path = null;

    private Waypoint m_CurrentTarget;

	public bool          m_FacingRight = false;
	Vector3 Dest;

    public bool m_attacking;
    public float attackDelay = 1.0f; //delay in seconds between attacks.
    private float attackTimer = 0.0f;

    public bool superAttackReady = false;
    float superAttackTimer = 0.0f;
    public float superAttackDelay = 10.0f;

    public bool throwObjectReady = false;
    float throwObjectTimer = 0.0f;
    public float throwObjectDelay = 10.0f;

    


    public Animator anim;
    // Use this for initialization
	void Awake(){
		anim = GetComponentInParent <Animator> ();
        m_attacking = false;
	}

    void Start()
    {
		m_Health = GetComponent<Health> ();
		if (m_Health == null)
		{
			Destroy (this.gameObject, 0.0f);
		}

		m_PersistantTime = m_TimeMax;
        m_PlayertransformRef = GameManager.GetPlayer().GetComponent<Transform>();

        // initialises m_CurrentTarget to something non offensive
        ClaimNewWaypoint(WayPointType.PointType.WAYPOINT_TYPE.RETREATING, false);

        // changes the current target for one that fits the current behaviour
        UpdateState();
        UpdateNavigationTarget();
        m_Path = PathFinding.DumbSearch.findPath(transform.position, m_CurrentTarget.transform.position);
    }

	void FindDestintation(){
		Dest = m_Path [0].transform.position;
		if (gameObject.transform.position.x > Dest.x) {
			FaceRight ();
		} else {
			FaceLeft ();
		}
	}

    // Update is called once per frame
    void LateUpdate()
    {
        if (m_Behaviour == BossBehaviourType.ENEMY_BEHAVIOUR.ATTACK && !superAttackReady)
        {
            superAttackTimer += 1.0f * Time.deltaTime;
            //Debug.Log("super attack timer: " + superAttackTimer);
        }

        if (m_Behaviour == BossBehaviourType.ENEMY_BEHAVIOUR.RETREAT && !throwObjectReady)
        {
            throwObjectTimer += 1.0f * Time.deltaTime;

            if (throwObjectReady)
            {
                throwObjectReady = false;
                throwObjectTimer = 0.0f;
                Debug.Log("boss is throwing object");
            }
        }

        //reset attacking timer so the enemy wont attack constatly.
        if (m_attacking == true)
        {
            attackTimer += 1.0f * Time.deltaTime;
        }

        if (m_attacking && attackTimer > attackDelay)
        {
            attackTimer = 0.0f;
        }

        if (superAttackTimer > superAttackDelay)
        {
            superAttackReady = true;
            superAttackTimer = 0.0f;
            //Debug.Log("super attack timer: " + superAttackTimer);
        }

        if (throwObjectTimer > throwObjectDelay)
        {
            throwObjectReady = true;
            throwObjectTimer = 0.0f;
        }

        if (m_Health.GetHealthPoints() <= 0)
		{
			return;
		}
		Vector3 moveBy = Vector3.zero;

        if ((transform.position - m_CurrentTarget.transform.position).magnitude > 0.5f)
        {
            int count = m_Path.Count - 1;
            if (m_Path.Count == 0 || m_Path[count] != m_CurrentTarget)
            {
                m_Path = PathFinding.DumbSearch.findPath(transform.position, m_CurrentTarget.transform.position);
            }
            moveBy += FollowPath();
        }
        else
        {
            UpdateState();
            UpdateNavigationTarget();
            moveBy = Steering.Arrive(m_CurrentTarget.transform.position, transform.position, m_Speed, m_SteeringSensitivity);
        }

        transform.position += moveBy * Time.deltaTime;
    }

    private void UpdateState()
    {
		if (m_Health.GetHealthPoints() < 50)
        {
			m_PersistantTime -= Time.deltaTime;
			if (m_PersistantTime <= 0.0f)
			{
				// act cowardly but not boring
				if (m_Behaviour != BossBehaviourType.ENEMY_BEHAVIOUR.RETREAT)
				{
					m_Behaviour = BossBehaviourType.ENEMY_BEHAVIOUR.RETREAT;
					m_PersistantTime = m_TimeMax * 2.0f;
				}
				else
				{
					m_Behaviour = BossBehaviourType.ENEMY_BEHAVIOUR.READY_TO_FIGHT;
					m_PersistantTime = m_TimeMax * 0.2f;
				}

			}
        }
        else
        {
			m_PersistantTime -= Time.deltaTime;
			// Every [m_TimeMax] seconds, flip between attack and defending
			if (m_PersistantTime <= 0.0f)
			{
				m_PersistantTime = m_TimeMax;
				if (m_Behaviour == BossBehaviourType.ENEMY_BEHAVIOUR.RETREAT ||
				    m_Behaviour == BossBehaviourType.ENEMY_BEHAVIOUR.ATTACK)
				{
					m_Behaviour = BossBehaviourType.ENEMY_BEHAVIOUR.READY_TO_FIGHT;
				}
				else
				{
					m_Behaviour = BossBehaviourType.ENEMY_BEHAVIOUR.ATTACK;
				}
			}
        }
    }

	public void UpdateNavigationTarget()
	{
		if (m_Behaviour == BossBehaviourType.ENEMY_BEHAVIOUR.RETREAT)
		{
			anim.SetTrigger ("walk");
			if (m_CurrentTarget.GetWayPointType() != WayPointType.PointType.WAYPOINT_TYPE.RETREATING)
			{
				ClaimNewWaypoint(WayPointType.PointType.WAYPOINT_TYPE.RETREATING, true);
				if (m_CurrentTarget == null)
				{
					ClaimNewWaypoint(WayPointType.PointType.WAYPOINT_TYPE.RETREATING, false);
				}
			}
		}
		else if(m_Behaviour == BossBehaviourType.ENEMY_BEHAVIOUR.READY_TO_FIGHT)
		{
			if (m_CurrentTarget.GetWayPointType() != WayPointType.PointType.WAYPOINT_TYPE.READY_TO_FIGHT)
			{
				anim.SetTrigger ("walk");
				ClaimNewWaypoint(WayPointType.PointType.WAYPOINT_TYPE.READY_TO_FIGHT, true);
				if (m_CurrentTarget == null)
				{
					ClaimNewWaypoint(WayPointType.PointType.WAYPOINT_TYPE.RETREATING, true);
					if (m_CurrentTarget == null)
					{
						ClaimNewWaypoint(WayPointType.PointType.WAYPOINT_TYPE.RETREATING, false);
					}
				}
			}
		}
		else if(m_Behaviour == BossBehaviourType.ENEMY_BEHAVIOUR.ATTACK)
		{
			if (m_CurrentTarget.GetWayPointType() != WayPointType.PointType.WAYPOINT_TYPE.ATTACKING)
			{
				ClaimNewWaypoint(WayPointType.PointType.WAYPOINT_TYPE.ATTACKING, true);
				anim.SetTrigger ("kick");
                if (!m_attacking)
                {
                    Attack();
                    m_attacking = true;
                    //Invoke("ResetAttack", 0.8f);
                }
				if (m_CurrentTarget == null)
				{
					ClaimNewWaypoint(WayPointType.PointType.WAYPOINT_TYPE.READY_TO_FIGHT, true);
					if (m_CurrentTarget == null)
					{
						ClaimNewWaypoint(WayPointType.PointType.WAYPOINT_TYPE.READY_TO_FIGHT, false);

					}
				}
			}
		}
	}

	public Vector3 FollowPath()
	{
		
		if (m_Path.Count == 0)
		{
			return Vector3.zero;
		}

		if((transform.position - m_Path[0].transform.position).magnitude < m_WaypointSensitivity)
		{
			m_Path.RemoveAt (0);
			m_Path.TrimExcess ();
        }

        if (m_Path.Count != 0)
        {
            Debug.DrawLine(transform.position, m_Path[0].transform.position, Color.red);
        }

        if (m_Path.Count > 1)
		{
			for(int i = 0; i < m_Path.Count - 1; i++)
			{
				Debug.DrawLine(m_Path[i].transform.position, m_Path[i + 1].transform.position, Color.red);
			}
			FindDestintation ();
			return Steering.Seek(m_Path[0].transform.position, transform.position, m_Speed);

		}
		FindDestintation ();
        return Steering.Arrive(m_Path[0].transform.position, transform.position, m_Speed, m_SteeringSensitivity);
	}

	public void ClaimNewWaypoint(WayPointType.PointType.WAYPOINT_TYPE wantedType, bool mustBeFree)
	{
		Waypoint newTarget = null;
		if (wantedType == WayPointType.PointType.WAYPOINT_TYPE.RETREATING)
		{
			newTarget = WayPointManager.ClosestWaypoint(transform.position, WayPointManager.GetAllRetreatWayPoints(), mustBeFree);
		}
		else if (wantedType == WayPointType.PointType.WAYPOINT_TYPE.READY_TO_FIGHT)
		{
			newTarget = WayPointManager.ClosestWaypoint(transform.position, WayPointManager.GetAllReadyToAttackWayPoints(), mustBeFree);
		}
		else if (wantedType == WayPointType.PointType.WAYPOINT_TYPE.ATTACKING)
		{
			newTarget = WayPointManager.ClosestWaypoint(transform.position, WayPointManager.GetAllAttackWayPoints(), mustBeFree);
		}
		if (m_CurrentTarget != null)
		{
			m_CurrentTarget.SetOwner (null);
		}
		if (newTarget != null)
		{
			m_CurrentTarget = newTarget;
			m_CurrentTarget.SetOwner (this.gameObject);
		}
		else
		{
			m_CurrentTarget = null;
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void FaceRight()
	{
		if (transform.localScale.x > 0.0f)
		{
			transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y , transform.position.z);
			Flip();
			m_FacingRight = true;
		}

	}

	private void FaceLeft()
	{
		if (transform.localScale.x < 0.0f)
		{
			transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
			Flip();
			m_FacingRight = false;
		}
	}

    private void Attack()
    {
        RaycastHit2D hit;

        if (m_FacingRight)
        {
            Vector3 originCentral = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.95f, transform.position.z);
            Debug.DrawRay(originCentral, new Vector3(-1, 0, 0), Color.blue, 10.0f, false);
            hit = Physics2D.Raycast(originCentral, new Vector3(-1, 0, 0), 10);
        }
        else
        {
            Vector3 originCentral = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.95f, transform.position.z);
            Debug.DrawRay(originCentral, new Vector3(1, 0, 0), Color.blue, 10.0f, false);
            hit = Physics2D.Raycast(originCentral, new Vector3(1, 0, 0), 3);
        }

        if (hit.collider != null)
        {
            //Debug.Log("hit");
            PlayerHealthScript PlayerHealth = hit.collider.gameObject.GetComponent<PlayerHealthScript>();
            if (PlayerHealth != null)
            {
                if (superAttackReady)
                {
                    PlayerHealth.TakeDamage(30);
                    Debug.Log("Super Attack");
                    superAttackReady = false;
                }
                else
                {
                    PlayerHealth.TakeDamage(10);
                }
            }
        }
    }
}