using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackVolume : MonoBehaviour {

    public BoxCollider2D trigger;
    public GameObject explosionPrefab;
    public GameObject player;
    bool playerinRange;

    public void Awake()
    {
        trigger = GetComponent<BoxCollider2D>();
    }

    public void Start()
    {
        
    }

    public void Attack(ushort damage)
    {
        PlayerHealthScript playerhealth = player.GetComponent<PlayerHealthScript>();

        playerhealth.TakeDamage(10);
    }

    public void EndAttack()
    {
        trigger.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            playerinRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            playerinRange = false;
        }
    }
}
