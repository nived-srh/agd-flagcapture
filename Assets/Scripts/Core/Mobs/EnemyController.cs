using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform isBlockedCheck, feetPosition;
    private bool isBlocked, isGrounded;
    public bool playerHit;
    public LayerMask platformLayer;
    private float rayDirectionRight = 0.1f;
    public int scale = 1;
    public float moveSpeed, groundCheckDistance;
    private Rigidbody2D rb;
    Animator animator;
    private GameObject player;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float damage;
    float attackTimer;
    bool isAttacking = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource.clip = audioClip;
    }

    // Update is called once per frame
    void Update()
    {

        //SET SPEED AND MOVEMENT DIRECTION
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        //SET RAY DIRECTION DEPENDING ON THE DIRECTION THE MOB IS FACING
        Vector2 rayDirection = new Vector2(rayDirectionRight, 0);

        //CREATE A RAYCAST IN THE DIRECTION THE MOB IS FACING
        RaycastHit2D hitWallRight = Physics2D.Raycast(isBlockedCheck.position, rayDirection, 1f);

        Debug.DrawRay(isBlockedCheck.position, rayDirection * 1f, Color.yellow);

        //SET ISBLOCKED TO TRUE IF RAY COLLIDES WITH ANYTHING WITH THE LAYER "PLATFORM" OR FALSE IF NOT
        if (hitWallRight.collider != null && hitWallRight.collider.CompareTag("Platform"))
        {
            isBlocked = true;
        }
        else
        {
            isBlocked = false;
        }

        Debug.DrawRay(feetPosition.position, Vector3.down * 1f, Color.yellow);
        RaycastHit2D hitGround = Physics2D.Raycast(feetPosition.position, Vector2.down, groundCheckDistance, platformLayer);

        //SET ISGROUNDED VALUE
        if (hitGround.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //FLIP SPRITE AND DIRECTION IF NOT GROUNDED OR BLOCKED
        if (!isGrounded || isBlocked)
        {
            scale *= -1;
            transform.localScale = new Vector3(scale, 1, 1);
            moveSpeed *= -1;
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            rayDirectionRight *= -1;
        }

        RaycastHit2D hitPlayer = Physics2D.Raycast(isBlockedCheck.position, rayDirection, 1f);

        //SET ANIMATIONS BASED ON COLLISION OF PLAYER
        if (hitPlayer.collider != null && hitPlayer.collider.CompareTag("Player"))
        {
            playerHit = true;
            animator.ResetTrigger("isWalking");
            animator.SetTrigger("isAttacking");
        }
        else
        {
            playerHit = false;
            animator.ResetTrigger("isAttacking");
            animator.SetTrigger("isWalking");
        }

        //DEAL DAMAGE AND PLAY ATTACK AUDIO
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= 1)
            {
                audioSource.Play();
                player.GetComponent<Player>().TakeDamage(damage);
                attackTimer = 0;
            }
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            player.GetComponent<Player>().TakeDamage(damage);
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
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }
}
