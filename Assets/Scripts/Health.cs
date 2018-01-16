using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int m_HealthPoints = 100;
    [SerializeField]
    private ushort m_MaxHealth = 100;

    public Animator anim;
    public bool isDead = false;
    public GameObject healthBar;

    private float healthScale;

    void awake()
    {
        anim = GetComponentInParent<Animator>();
        healthBar.GetComponent<Renderer>().enabled = false;
        
    }

    void Start()
    {
        healthScale = healthBar.transform.localScale.x;
    }

    public int GetHealthPoints()
    {
        //test
        return m_HealthPoints;
    }

    public void AddHealth(ushort HealthPoints)
    {
        m_HealthPoints += (int)HealthPoints;
        if (m_HealthPoints > m_MaxHealth)
        {
            m_HealthPoints = (int)m_MaxHealth;
        }
    }


    public void TakeDamage(ushort HealthPoints)
    {
        if (!isDead)
        {
            m_HealthPoints -= (int)HealthPoints;
            if (m_HealthPoints <= 0)
            {
                isDead = true;
                anim.SetTrigger("Die");
                Destroy(healthBar, 0);
            }
            else
            {
                if (m_HealthPoints > 0)
                    anim.SetTrigger("Damage");

                float healthPercent = (float)m_HealthPoints / (float)m_MaxHealth;

                Vector3 healthBarScale = healthBar.transform.localScale;
                healthBarScale.x = healthScale * healthPercent;
                healthBar.transform.localScale = healthBarScale;
            }

        }
    }

}
