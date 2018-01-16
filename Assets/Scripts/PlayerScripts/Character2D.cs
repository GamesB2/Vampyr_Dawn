using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InputManager))]
public class Character2D : MonoBehaviour
{
    private InputManager m_InputManager;

    public float         m_WalkSpeed = 2;

    public Animator      m_Anim;                                                    
	public bool          m_FacingRight = false;
    private bool         m_Attacking = false;
    private bool         m_recoil = false;
    private enum States { standing, crouching, grabbing, attacking };

    private AttackTrigger attacktrigger;

    bool feeding;
    int feedingTime;
    GameObject feedingenemy;

    private States m_currentState = States.standing;
    // More states to come

    public int health;

    List<GameObject> currentCollisions = new List<GameObject>();

    public GameObject m_BoundingBox;

	public GameObject Assure;

	//bounding box stuff
	public float boundingBoxXMin = 0;
	public float boundingBoxXMax = 0;
	public float boundingBoxYMin = 0;
	public float boundingBoxYMax = 0;
    
    private void Awake()
    {
        m_Anim         = GetComponent<Animator>();
        m_InputManager = GetComponent<InputManager>();
        attacktrigger =  GetComponentInChildren<AttackTrigger>();
		m_FacingRight = false;

		m_BoundingBox = GameObject.FindGameObjectWithTag ("BoundingBox");
		BoxCollider2D collider = m_BoundingBox.GetComponent<BoxCollider2D> ();
		boundingBoxXMin = m_BoundingBox.transform.position.x - (collider.size.x / 2);
		boundingBoxXMax = m_BoundingBox.transform.position.x + (collider.size.x / 2);
		boundingBoxYMin = m_BoundingBox.transform.position.y - (collider.size.y / 2);
		boundingBoxYMax = m_BoundingBox.transform.position.y + (collider.size.y / 2);

        feedingTime = 0;
        feeding = false;
    }

    private void FixedUpdate()
    {
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        currentCollisions.Add(col.gameObject);
        Debug.Log("enter");
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        currentCollisions.Remove(col.gameObject);
        Debug.Log("Exit");
    }

    void Feeding()
    {
        if (feedingTime != 4)
        {
            GetComponent<PlayerHealthScript>().AddHealth(5);
            Invoke("Feeding", 0.5f);
            feedingTime++;
        }
        else
        {
            feedingTime = 0;
            feeding = false;
            m_Anim.SetBool("Crouching", false);
        }
    }

    public void Update()
    {
        if (GetComponent<PlayerHealthScript>().isDead) return;

        if (feeding) return;

        m_Anim.SetBool("Moving", false);

        bool m_Button1 = m_InputManager.m_Button1;
        bool m_Button2 = m_InputManager.m_Button2;
        bool m_Button3 = m_InputManager.m_Button3;
        bool m_Button4 = m_InputManager.m_Button4;

        bool moving = (m_InputManager.m_DirectionHorizontal != 0 || m_InputManager.m_DirectionVertical != 0);

        m_Anim.SetBool("Moving", moving);

        if (!m_Attacking)
        {
            if (m_Button1)
            {
                m_Anim.SetTrigger("Button1Press");
                Invoke("FinishAnim", 0.9f);
                m_Attacking = true;
                AttackButton1Pressed();
                
            }
            if (m_Button2)
            {
                m_Anim.SetTrigger("Button2Press");
                Invoke("FinishAnim", 0.75f);
                m_Attacking = true;
                AttackButton2Pressed();
            }
            if (m_Button3)
            {
                m_Anim.SetBool("Moving", false);
                m_Anim.SetBool("Crouching", true);
                moving = false;

                foreach (GameObject g in currentCollisions)
                {
                    if (g.tag == "EnemyAi")
                    {
                        if (g.GetComponent<Health>().isDead)
                        {
                            feeding = true;
                            Feeding();
                            break;
                        }
                    }
                }
            }
            if (!m_Button3)
            {
                m_Anim.SetBool("Crouching", false);
            }
                if (m_Button4)
            {
                //m_Anim.SetBool("Grabbing", true);
                //moving = false;

                GetComponent<PlayerHealthScript>().TakeDamage(1);
            }
            if (!m_Button4)
            {
                m_Anim.SetBool("Grabbing", false);
            }
        }


        if (moving && !m_Attacking)
        {
            Move();
        }

		//check if player walks off scene
		if (transform.position.x < boundingBoxXMin || transform.position.x > boundingBoxXMax) 
		{
			Assure.SetActive (true);
		}

    }
		

    public void FinishAnim()
    {
        m_Attacking = false;
    }

    public void FinishDamageAnim()
    {
        m_recoil = false;
        m_Anim.SetBool("Damage", false);
    }

    public void Move()
    {
            if (m_InputManager.m_DirectionHorizontal < 0)
            {
                FaceRight();
            }
            else
            {
                FaceLeft();              
            }



        //Do the actual movement.
        Vector3 moveby = Vector3.zero;
        moveby.x = m_InputManager.m_DirectionHorizontal;
        moveby.y = m_InputManager.m_DirectionVertical;
        moveby.z = 0;

		if (transform.position.y >= boundingBoxYMax) { //player has gone above the bounding box;
			if (moveby.y > 0) //player tryna move up
				moveby = new Vector3(moveby.x, moveby.y *-1, moveby.x);
		} else if (transform.position.y <= boundingBoxYMin) { //player has gone below the bounding box;
			if (moveby.y < 0) //player tryna move down
				moveby = new Vector3(moveby.x, moveby.y *-1, moveby.x);
		}

        Vector3.Normalize(moveby);
        moveby *= m_WalkSpeed * Time.deltaTime;

        transform.position += moveby;
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

    public void AttackButton1Pressed()
    {
        attacktrigger.Attack(20);
    }

    public void AttackButton2Pressed()
    {
        attacktrigger.Attack(20);
    }
}