using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float maxSpeed;
	public LayerMask groundLayer;
	public float minHeight, maxHeight;
	public float damagetime = 0.5f;
	public int maxHealth;
	public float attackRate = 1f;

	private int currentHealth;
	private float currentSpeed;
	private Rigidbody rb;
	private Animator anim;
	private Transform groundCheck;
	private bool onGround;
	private bool facingRight = false;
	private Transform target;
	private bool isDead = false;
	private float zforce;
	private float walkTimer;
	private bool damaged = false;
	private float damageTimer;
	private float nextAttack;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		groundCheck = transform.Find ("GroundCheckEnemy");
		target = FindObjectOfType <Character2D> ().transform;
		currentSpeed = maxSpeed;
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		onGround = Physics.Raycast (groundCheck.position, Vector3.down, 0.15f,  groundLayer);
		anim.SetBool ("Grounded", onGround);
		anim.SetBool("Dead", isDead);


		facingRight = (target.position.x < transform.position.x) ? false : true;
		if (facingRight) {
			transform.eulerAngles = new Vector3 (0, 0, 0);
		} else {
			transform.eulerAngles = new Vector3 (0, 180, 0);
		}

		if (damaged && !isDead) {
			damageTimer += Time.deltaTime;
			if (damageTimer > -damagetime) {
				damaged = false;
				damageTimer = 0;
			}
		}
		walkTimer += Time.deltaTime;

	}

	private void FixedUpdate()
	{
		if (!isDead) {
			Vector3 targetDistance = target.position - transform.position;
			float hForce = targetDistance.x / Mathf.Abs (targetDistance.x);

			if (walkTimer >= Random.Range (1f, 2f)) {
				zforce = Random.Range (-1, 2);
				walkTimer = 0;
			}

			if (Mathf.Abs (targetDistance.x) < 1.5f) {
				hForce = 0;
			}

			if (!damaged) {
				rb.velocity = new Vector3 (hForce * currentSpeed, rb.velocity.y, zforce * currentSpeed);
			}

			anim.SetFloat ("Speed", Mathf.Abs (currentSpeed));

			rb.position = new Vector3 (
				rb.position.x,
				rb.position.y,
				Mathf.Clamp (rb.position.z, minHeight + 1, maxHeight - 1));

			if (Mathf.Abs (targetDistance.x) < 1.5f && Mathf.Abs (targetDistance.z) < 1.5f && Time.time > nextAttack) 
			{
				anim.SetTrigger ("Attack");
				currentSpeed = 0;
				nextAttack = Time.time + attackRate;
			}		
		}
	}

	public void TookDamage(int damage)
	{
		if(!isDead)
		{
			damaged = true;
			currentHealth -= damage;
			anim.SetTrigger ("HitDamage");
			if (currentHealth <= 0) 
			{
				isDead = true;
				rb.AddRelativeForce (new Vector3 (3, 5, 0), ForceMode.Impulse);
			}
		}
	}

	public void DisableEnemy()
	{
		gameObject.SetActive (false);
	}

	void ResetSpeed()
	{
		currentSpeed = maxSpeed;
	}
}
