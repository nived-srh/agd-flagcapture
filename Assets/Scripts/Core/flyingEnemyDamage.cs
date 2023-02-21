using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingEnemyDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;

    [Tooltip("Damage done to the player on physical hit")]
    public int damage;
    [Tooltip("Knockback applied to the player on physical hit")]
    public int enemykbForce;
    public PlayerController playerController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.kbForce = enemykbForce;
            playerController.kbCounter = playerController.kbTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerController.knockFromRight = true;
            }
            if (collision.transform.position.x >= transform.position.x)
            {
                playerController.knockFromRight = false;
            }
             playerHealth.TakeDamage(damage);
        }

    }
}
