using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{

    public BoxCollider2D trigger;
    private List<GameObject> collidingObjects;
    public GameObject explosionPrefab;

    public void Awake()
    {
        trigger = GetComponent<BoxCollider2D>();
        collidingObjects = new List<GameObject>();
    }

    public void Start()
    {

    }

    public void Attack(ushort damage)
    {

        foreach (GameObject enemy in collidingObjects)
        {
            Health healthScript;
            if ((healthScript = enemy.GetComponent<Health>()) != null)
            {
                healthScript.TakeDamage(damage);
                Vector3 hit = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 0.7f);
                Instantiate(explosionPrefab, hit, Quaternion.identity);
            }
        }
    }

    public void EndAttack()
    {
        trigger.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        collidingObjects.Add(coll.gameObject);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        collidingObjects.Remove(coll.gameObject);
    }
}
