using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KamikazeDragonDamage : MonoBehaviour
{
    private Animator animator;
    private GameObject player;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("explode");
            StartCoroutine(WaitForHalfSecond());
        }

    }

    IEnumerator WaitForHalfSecond()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerHealth>().currentHealth -= 50;
        Destroy(transform.parent.gameObject);
    }
}

