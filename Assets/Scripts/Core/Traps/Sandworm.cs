using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class Sandworm : MonoBehaviour
    {

        Animator animator;
        private Player playerHealth;
        public int damage;
        private float attackTimer;
        private bool attack = false;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<Player>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                animator.ResetTrigger("isIdle");
                animator.SetTrigger("isAttacking");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                animator.ResetTrigger("isAttacking");
                animator.SetTrigger("isIdle");
                attack = false;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                attack = true;
            }
        }

        void Update()
        {
            if (attack)
            {
                attackTimer += Time.deltaTime;

                if (attackTimer >= 0.66)
                {
                    playerHealth.TakeDamage(damage);
                    attackTimer = 0;
                }
            }
        }

    }

}