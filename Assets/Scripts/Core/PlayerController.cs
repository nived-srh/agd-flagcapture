using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float moveSpeed, jumpForce;
    public float input;
    public SpriteRenderer spriteRenderer;

    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;
    public float kbForce, kbCounter, kbTotalTime;
    public bool knockFromRight;


    
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        if (input < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (input > 0)
        {
            spriteRenderer.flipX = false;
        }

        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);


        if (isGrounded == true && Input.GetButton("Jump"))
        {
            playerRb.velocity = Vector2.up * jumpForce;
        }
    }

    void FixedUpdate()
    {

        if (kbCounter <= 0)
        {
            playerRb.velocity = new Vector2 (input * moveSpeed, playerRb.velocity.y);
        }
        else 
        {
            if (knockFromRight == true)
            {
                playerRb.velocity = new Vector2(-kbForce, kbForce);
            }
            if (knockFromRight == false)
            {
                playerRb.velocity = new Vector2(kbForce, kbForce);
            }

            kbCounter -= Time.deltaTime;
        }
        
    }

}
