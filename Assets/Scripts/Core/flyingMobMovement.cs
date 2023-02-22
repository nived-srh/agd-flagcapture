using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingMobMovement : MonoBehaviour
{

    public Transform[] patrolPoints;
    public float moveSpeed = 0f, baseSpeed;
    public int patrolDestination;
    public Transform playerTransform, obstacle;
    public bool isChasing;
    public float chaseDistance;
    public bool isClose;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the enemy is moving to the right and flip enemy to the right
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Check if the enemy is moving to the left and flip enemy to the left
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Check if player is close to start attack animation
        if (Vector2.Distance(transform.position, playerTransform.position) > 1)
        {
            isClose = false;
        }
        else if (Vector2.Distance(transform.position, playerTransform.position) <= 1)
        {
            isClose = true;
        }

        // Check if enemy is close to player and disable speed to not push character back
        float distancex = Mathf.Abs(transform.position.x - playerTransform.transform.position.x);
        Debug.Log(distancex);
        if (distancex <= 1.5)
        {
            moveSpeed = 0f;
        }
        else
        {
            moveSpeed = baseSpeed;
        }

        //Make enemy chase player based on vertical distance
        float distance = Mathf.Abs(transform.position.y - playerTransform.transform.position.y);
        if (distance <= chaseDistance)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
            moveSpeed = baseSpeed; //Re-enables base speed if enemy is too close to character and the speed is disabled
        }

        //Make enemy chase player if isChasing is true, or else go back to patrolling position;
        if (isChasing)
        {
            Vector3 playerDirection = (playerTransform.position - transform.position).normalized;
            rb.velocity = playerDirection * moveSpeed;
        }
        else
        {

            // Move to patrol destination 1
            if (patrolDestination == 1)
            {
                Vector3 patrol1Destination = (patrolPoints[1].position - transform.position).normalized;
                rb.velocity = patrol1Destination * moveSpeed;
                //Go to patrol destination 0 if close to patrol destination 1
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
                {
                    patrolDestination = 0;
                }
            }

            //Move to patrol destionation 0
            if (patrolDestination == 0)
            {
                Vector3 patrol0Destination = (patrolPoints[0].position - transform.position).normalized;
                rb.velocity = patrol0Destination * moveSpeed;
                //Go to patrol destination 1 if close to patrol destination 0
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
                {
                    patrolDestination = 1;
                }
            }
        }

    }
}
