using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiretrapDamage : MonoBehaviour
{
    // public PlayerHealth playerHealth;
    // public float damage;



    private PlayerHealth playerHealth;
    public float damage;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }

}

