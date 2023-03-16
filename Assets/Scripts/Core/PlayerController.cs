using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float moveSpeed, jumpForce, headJumpForce;
    public float input;
    public SpriteRenderer spriteRenderer;

    public LayerMask groundLayer, headLayer;
    private bool isGrounded, isOnHead;
    public Transform feetPosition;
    public float groundCheckCircle, headCheckCircle;
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
        isOnHead = Physics2D.OverlapCircle(feetPosition.position, headCheckCircle, headLayer);

        if (isOnHead == true)
        {
            playerRb.velocity = Vector2.up * headJumpForce;
        }


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
