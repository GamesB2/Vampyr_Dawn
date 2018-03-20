using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

	public int damage;
	public bool heavy = false;

    public void Awake()
    {

    }

    public void Start()
    {

    }

    private void OnTriggerEnter (Collider other)
    {
		Enemy enemy = other.GetComponent<Enemy> ();
		if (enemy != null) 
		{
			if (heavy) {
				enemy.TookHeavyDamage (damage);
			} else {
				enemy.TookDamage (damage);
			}
			Debug.Log ("Hit");
		}
    }

}
