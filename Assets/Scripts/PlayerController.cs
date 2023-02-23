using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, jump, move;
    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool facingRight;

    public bool isGrounded;
    // private Animator anim;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        //Move player left or right
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        //Check Player state and set animations accordingly
        if (move == 0)
        {
            // anim.SetBool("isRunning", false);
        }
        else if (move > 0)
        {
            facingRight = true;
            // anim.SetBool("isRunning", true);
        }
        else if (move < 0)
        {
            facingRight = false;
            // anim.SetBool("isRunning", true);
        }

        //Flip player based on direction
        if (facingRight)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        
        Debug.DrawRay(transform.position, -transform.up * 0.2f);

        //Check if Player is grounded
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));

        //Jump only if Player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }

    }


}
