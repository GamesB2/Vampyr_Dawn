using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InputManager))]
public class Character2D : MonoBehaviour
{

    public Animator      m_Anim;                                                    

    private enum States { standing, crouching, grabbing, attacking };

	public float maxSpeed = 2;
	public float jumpForce = 4;

	public float currentSpeed;
	private Rigidbody rb;
	private Transform groundCheck;
	private float hitDistance;
	public bool onGround = false;
	public bool facingRight = true;
	private bool jump = false;
	private bool isDead = false;
	public float minHeight, maxHeight;
	public LayerMask groundLayer;
	public bool damaged;
	public float damagetime = 1.0f;



	private float damageTimer;
	public int maxHealth;
    int currentHealth;
    
	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		m_Anim = GetComponent<Animator>();
		groundCheck = gameObject.transform.Find ("GroundCheck");
		currentSpeed = maxSpeed;
		currentHealth = maxHealth;
	}

    private void Awake()
    {
		facingRight = false;
    }

    private void FixedUpdate()
    {
		if (!isDead) {

			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			if (!onGround) {
				moveVertical = 0;
			}
				
			if (!damaged) {
				rb.velocity = new Vector3 (moveHorizontal * currentSpeed, rb.velocity.y, moveVertical * currentSpeed);
			}

			if (onGround) 
			{
				m_Anim.SetFloat ("Speed", Mathf.Abs (rb.velocity.magnitude));
			}

			if (moveHorizontal < 0 && !facingRight && currentSpeed > 0) 
			{
				FaceRight ();
			} 
			else if (moveHorizontal > 0 && facingRight && currentSpeed > 0) 
			{
				FaceLeft ();
			}

			if (jump && onGround && currentSpeed > 0) 
			{
				jump = false;
				rb.AddForce (Vector3.up * jumpForce);
			}

			float minWidth = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 10)).x;
			float maxWidth = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 10)).x;
			rb.position = new Vector3(Mathf.Clamp(rb.position.x, minWidth+1, maxWidth -1),
				rb.position.y,
				Mathf.Clamp(rb.position.z, minHeight +1, maxHeight -1));
		}
    }



    public void OnTriggerEnter2D(Collider2D col)
    {
        //currentCollisions.Add(col.gameObject);
        Debug.Log("enter");
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        //currentCollisions.Remove(col.gameObject);
        Debug.Log("Exit");
    }


    public void Update()
	{

		onGround = Physics.Raycast (groundCheck.position, Vector3.down, 0.15f,  groundLayer);
		Debug.DrawRay(groundCheck.position, Vector3.down);

		m_Anim.SetBool ("OnGround", onGround);
		m_Anim.SetBool ("Dead", isDead);

		if (Input.GetButtonDown ("Jump") && onGround) 
		{
			jump = true;
		}

		if (Input.GetButtonDown ("Fire1")) 
		{
			m_Anim.SetTrigger ("Attack");
		}

		if (damaged && !isDead) 
		{
			damageTimer += Time.deltaTime;
			if (damageTimer > damagetime) 
			{
				damaged = false;
				damageTimer = 0;
			}
		}
	}

    public void FinishDamageAnim()
    {
        m_Anim.SetBool("Damage", false);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
		facingRight = !facingRight;

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
        }

    }

    private void FaceLeft()
    {
        if (transform.localScale.x < 0.0f)
        {
            transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
            Flip();
        }
    }
	
	void ZeroSpeed()
	{
		currentSpeed = 0;
	}

	void ResetSpeed()
	{
		currentSpeed = maxSpeed;
	}

	public void TookDamage(int damage)
	{
		if(!isDead)
		{
			damaged = true;
			currentHealth -= damage;
			m_Anim.SetTrigger ("HitDamage");
			if (currentHealth <= 0) 
			{
				isDead = true;
				rb.AddRelativeForce (new Vector3 (3, 5, 0), ForceMode.Impulse);
			}
		}
	}
}