using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
	[SerializeField]
	private int m_HealthPoints = 100;
	[SerializeField]
	private ushort m_MaxHealth = 100;

	public GameObject healthBar;

	public Animator anim;
	public bool isDead = false;

	private float healthScale; 


	void awake(){
		
	}
	public int GetHealthPoints()
	{
		//test


		return m_HealthPoints;
	}

	void Start()
	{
		anim = GetComponentInParent <Animator> ();
		healthScale = healthBar.transform.localScale.x;
	}

	public void AddHealth(int HealthPoints)
	{
		m_HealthPoints += (int)HealthPoints;
		if (m_HealthPoints > m_MaxHealth)
		{
			m_HealthPoints = (int)m_MaxHealth;
		}

         float healthPercent = (float)m_HealthPoints / (float)m_MaxHealth;

        Vector3 healthBarScale = healthBar.transform.localScale;
        healthBarScale.x = healthScale * healthPercent;
        healthBar.transform.localScale = healthBarScale;
    }


	public void TakeDamage(int HealthPoints)
	{
		if(!isDead){
			m_HealthPoints -= (int)HealthPoints;
			if (m_HealthPoints <= 0) {
				isDead = true;
				anim.SetTrigger ("Die");
				//Destroy (this.gameObject, 3);


			} else {
				if (m_HealthPoints > 0)
					anim.SetTrigger ("Damage");
			}

			float healthPercent = (float)m_HealthPoints / (float)m_MaxHealth;

			Vector3 healthBarScale = healthBar.transform.localScale;
			healthBarScale.x = healthScale * healthPercent;
			healthBar.transform.localScale = healthBarScale;
		}
	}
}
