using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    Animator animator;
    private Player playerHealth;
    public float damage;

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
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }
}

