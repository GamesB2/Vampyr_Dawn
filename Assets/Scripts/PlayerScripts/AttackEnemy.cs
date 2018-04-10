using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{

	public int damage;

    public void Awake()
    {

    }

    public void Start()
    {

    }

    private void OnTriggerEnter (Collider other)
    {
		Character2D player = other.GetComponent<Character2D> ();
		if (player != null) {
			player.TookDamage (damage);
			Debug.Log ("Hit");
		}
    }

}
