using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class mobMovement : MonoBehaviour
{

    // public Transform[] patrolPoints;
    // public float moveSpeed;
    // public int patrolDestination;
    // public Transform playerTransform, obstacle;
    // public bool isChasing;
    // public float chaseDistance;
    // public bool isClose;

    public Transform isBlockedCheck, feetPosition;
    private bool isBlocked, isGrounded;
    public bool playerHit;
    public LayerMask platformLayer;
    private float rayDirectionRight = 0.1f;
    public int scale = 1;
    public float moveSpeed;
    private Rigidbody2D rb;
    Animator animator;
    private float timer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Set speed and movement direction;
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        // //Check if grounded
        // RaycastHit2D hit = Physics2D.Raycast(feetPosition.position, Vector2.down, 0.1f, platformLayer);
        // if (hit.collider != null)
        // {
        //     isGrounded = true;
        // }
        // else
        // {
        //     isGrounded = false;
        // }

        //Set ray direction depending on the direction the mob is facing
        Vector2 rayDirection = new Vector2(rayDirectionRight, 0);

        //Create a raycast in the direction the mob is facing
        RaycastHit2D hitWallRight = Physics2D.Raycast(isBlockedCheck.position, rayDirection, 1f);

        Debug.DrawRay(isBlockedCheck.position, rayDirection * 1f, Color.yellow);

        //Set isBlocked to true if ray collides with anything with the layer "Platform" or false if not
        if (hitWallRight.collider != null && hitWallRight.collider.CompareTag("Platform"))
        {
            isBlocked = true;
        }
        else
        {
            isBlocked = false;
        }

        Debug.DrawRay(feetPosition.position, Vector3.down * 1f, Color.yellow);
        RaycastHit2D hitGround = Physics2D.Raycast(feetPosition.position, Vector2.down, 1f, platformLayer);

        if (hitGround.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (!isGrounded || isBlocked)
        {
            scale *= -1;
            transform.localScale = new Vector3(scale, 1, 1);
            moveSpeed *= -1;
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            rayDirectionRight *= -1;
        }

        RaycastHit2D hitPlayer = Physics2D.Raycast(isBlockedCheck.position, rayDirection, 1f);

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






        // if (Vector2.Distance(transform.position, obstacle.position) <= .1f)
        // {
        //     isChasing = false;
        //     transform.localScale = new Vector3(-1, 1, 1);
        // }

        // if (Vector2.Distance(transform.position, playerTransform.position) > 1)
        // {
        //     isClose = false;
        // }
        // else if (Vector2.Distance(transform.position, playerTransform.position) <=1)
        // {
        //     isClose = true;
        // }

        // if (isChasing)
        // {
        //     if (transform.position.x > playerTransform.position.x)
        //     {
        //         transform.localScale = new Vector3(-1, 1, 1);
        //         transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        //     }

        //     if (transform.position.x < playerTransform.position.x)
        //     {
        //         transform.localScale = new Vector3(1, 1, 1);
        //         transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        //     }
        // }
        // else
        // {
        //     if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
        //     {
        //         isChasing = true;
        //     }

        //     if (patrolDestination == 1)
        //     {
        //         transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);

        //         if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
        //         {
        //             transform.localScale = new Vector3(1, 1, 1);
        //             patrolDestination = 0;
        //         }
        //     }


        //     if (patrolDestination == 0)
        //     {
        //         transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);

        //         if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
        //         {
        //             transform.localScale = new Vector3(-1, 1, 1);
        //             patrolDestination = 1;
        //         }
        //     }
        // }

    }



    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {

    //     }
    // }

    // void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {

    //     }
    // }


}
