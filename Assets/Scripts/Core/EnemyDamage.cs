using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damage;
    // public int damage, enemykbForce;
    // public PlayerController playerController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // playerController.kbForce = enemykbForce;
            // playerController.kbCounter = playerController.kbTotalTime;
            // if (collision.transform.position.x <= transform.position.x)
            // {
            //     playerController.knockFromRight = true;
            // }
            // if (collision.transform.position.x >= transform.position.x)
            // {
            //     playerController.knockFromRight = false;
            // }
             playerHealth.TakeDamage(damage);
        }

    }
}
