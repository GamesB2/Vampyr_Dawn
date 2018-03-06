using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

	public int damage;

    public void Awake()
    {

    }

    public void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
		Enemy enemy = other.GetComponent<Enemy> ();
		if (enemy != null) {
			enemy.TookDamage (damage);
		}
    }

}
