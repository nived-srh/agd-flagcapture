using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Tooltip("This is the max health of the player")]
    public float maxHealth = 100;
    [Tooltip("This is the current health of the player")]
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            //Kill Player
            // Destroy(gameObject);
        }
    }
}
