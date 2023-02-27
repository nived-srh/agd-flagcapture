using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class PlayerHealth : MonoBehaviour
    {

        [Tooltip("This is the max health of the player")]
        public int maxHealth = 100;
        [Tooltip("This is the current health of the player")]
        public int currentHealth;
        // Start is called before the first frame update

        public HealthBar healthBar;

        void Start()
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            healthBar.setHealth(currentHealth);
            if (currentHealth <= 0)
            {
                //Kill Player
                // Destroy(gameObject);
            }
        }
    }
}

