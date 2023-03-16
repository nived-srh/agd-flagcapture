using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class Firetrap : MonoBehaviour
    {
        Animator animator;
        private Player playerHealth;
        public int damage;
        private GameObject player;
        void Start()
        {
            animator = GetComponent<Animator>();
            // GameObject player = GameObject.FindGameObjectWithTag("Player");
            // playerHealth = player.GetComponent<Player>();
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
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                player = other.gameObject;
                player.GetComponent<Player>().TakeDamage(damage);
            }
        }
    }

}