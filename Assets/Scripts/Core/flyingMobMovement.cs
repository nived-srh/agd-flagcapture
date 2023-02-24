using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingMobMovement : MonoBehaviour
{

    // public Transform[] patrolPoints;
    // public int patrolDestination;

    // public Transform obstacle;
    // public bool isChasing;
    // public float chaseDistance;
    // public bool isClose;
    // public Transform playerTransform;
    public Transform feetPosition, middlePosition, middlePositionRight;
    private Rigidbody2D rb;
    private bool isGrounded, isBlocked;
    public float moveSpeed, groundCheckDistance = 3f;
    public LayerMask platformLayer;
    public int scale = 1;
    private float rayDirectionRight = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {


        // // Check if the enemy is moving to the right and flip enemy to the right
        // if (rb.velocity.x > 0)
        // {
        //     transform.localScale = new Vector3(1, 1, 1);
        // }

        // // Check if the enemy is moving to the left and flip enemy to the left
        // if (rb.velocity.x < 0)
        // {
        //     transform.localScale = new Vector3(-1, 1, 1);
        // }

        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        //Check if above a platform
        RaycastHit2D hit = Physics2D.Raycast(feetPosition.position, Vector2.down, groundCheckDistance, platformLayer);
        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }



        //Check if blocked
        Vector2 rayDirection = new Vector2(rayDirectionRight, 0);
        RaycastHit2D hitWallRight = Physics2D.Raycast(middlePositionRight.position, rayDirection, 1f, platformLayer);

        if (hitWallRight.collider != null)
        {
            isBlocked = true;
        }
        else
        {
            isBlocked = false;
        }

        if (!isGrounded || isBlocked)
        {
            scale *= -1;
            transform.localScale = new Vector3(scale, 1, 1);
            moveSpeed *= -1;
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            rayDirectionRight *= -1;
        }
    }
}
