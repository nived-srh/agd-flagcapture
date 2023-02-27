using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private GameObject player;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float damage;
    float attackTimer;
    bool isAttacking = false;
    // public int damage, enemykbForce;
    // public PlayerController playerController;

    void Start()
    {
        audioSource.clip = audioClip;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            audioSource.Play();

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
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            isAttacking = true;
        } else {
            isAttacking = false;
        }
    }

    void Update()
    {
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= 1)
            {
                audioSource.Play();
                player.GetComponent<PlayerHealth>().TakeDamage(damage);
                attackTimer = 0;
            }
        }
    }



}
