using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public LayerMask enemyMask;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth, myHeight;

    void Start() 
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y * 0.015f;
        speed = 5;
    }

    void FixedUpdate() 
    {

        // Check if grounded
        Vector2 lineCastPosition = myTrans.position + myTrans.right * myWidth;
        Debug.DrawLine(lineCastPosition, lineCastPosition + Vector2.down*5f);
        bool isGrounded = Physics2D.Linecast(lineCastPosition, lineCastPosition+Vector2.down, enemyMask);
        Debug.DrawLine(lineCastPosition, lineCastPosition + myTrans.right.toVector2() * 5f);
        bool isBlocked = Physics2D.Linecast(lineCastPosition, lineCastPosition + myTrans.right.toVector2()* 1f, enemyMask);




        // Turn around if not grounded, or if blocked
        if (!isGrounded || isBlocked)
        {
            Vector3 currentRotation = myTrans.eulerAngles;
            currentRotation.y += 180;
            myTrans.eulerAngles = currentRotation;
        }

        
        Vector2 myVelocity = myBody.velocity;
        myVelocity.x = myTrans.right.x * speed;
        myBody.velocity = myVelocity;

    }
}
