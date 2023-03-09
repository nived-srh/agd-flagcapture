using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class Player : MonoBehaviour
    {

        [Tooltip("This is the max health of the player")]
        public int maxHealth = 100;
        [Tooltip("This is the current health of the player")]
        public int currentHealth;
        public int currentScore;
        // Start is called before the first frame update
        public GameObject playerStats;

        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private float jumpForce = 14f;

        [SerializeField] private KeyCode left;
        [SerializeField] private KeyCode right;
        [SerializeField] private KeyCode jump;

        private Rigidbody2D rb;
        private SpriteRenderer sprite;
        private Animator anim;

        private enum MovementState { idle, running, jumping, falling }
        private MovementState state;

        // dashing with key
        // [SerializeField] private KeyCode dash;
        private bool canDash;
        private bool isDashing;
        [SerializeField] private float dashingPower;
        private float dashingTime = 0.2f;
        private float dashingCooldown = 1f;
        private int leftTotal = 0;
        private float leftTimeDelay = 0;
        private int rightTotal = 0;
        private float rightTimeDelay = 0;
        // used for double jump
        private int jumpTotal = 0;
        private float jumpTimeDelay = 0;
        [SerializeField] private TrailRenderer TrailRenderer;

        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private bool isGrounded;

        void Awake()
        {
            currentHealth = maxHealth;
            currentScore = 0;
        }

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
            canDash = true;

            if (playerStats != null)
            {
                playerStats.GetComponent<PlayerStats>().SetMaxHealth(maxHealth);
            }
        }

        private void Update()
        {
            if (isDashing) { return; }
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

            MovementState state;

            // if (Input.GetKey(left))
            // {
            //     rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            //     state = MovementState.running;
            //     // anim.SetBool("running", true);
            //     sprite.flipX = true;


            //     if (Input.GetKeyDown(left))
            //         leftTotal += 1;

            //     if ((leftTotal == 1) && (leftTimeDelay < 0.5))
            //         leftTimeDelay += Time.deltaTime;

            //     if ((leftTotal == 1) && (leftTimeDelay >= 0.5))
            //     {
            //         leftTimeDelay = 0;
            //         leftTotal = 0;
            //     }

            //     if ((leftTotal == 2))
            //     {
            //         leftTotal = 0;
            //         if ((leftTimeDelay < 0.5) && canDash)
            //         {
            //             StartCoroutine(Dash(-1));
            //             Debug.Log("DASHED");
            //         }

            //         leftTimeDelay = 0;
            //     }
            // }
            if (Input.GetKey(left))
            {
                state = MovementState.running;
                sprite.flipX = true;
                if (leftTimeDelay > 0 && leftTotal == 1 && canDash && Input.GetKeyDown(left))
                {
                    leftTotal += 1;
                    StartCoroutine(Dash(-1));
                }
                else
                {
                    leftTotal = 1;
                    leftTimeDelay = 0.5f;
                    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                }

            }

            else if (Input.GetKey(right))
            {
                state = MovementState.running;
                sprite.flipX = false;
                if (rightTimeDelay > 0 && rightTotal == 1 && canDash && Input.GetKeyDown(right))
                {
                    rightTotal += 1;
                    StartCoroutine(Dash(1));
                }
                else
                {
                    rightTotal = 1;
                    rightTimeDelay = 0.5f;
                    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                }

            }

            // else if (Input.GetKey(right))
            // {
            //     rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            //     state = MovementState.running;
            //     // anim.SetBool("running", true);
            //     sprite.flipX = false;

            //     if (Input.GetKeyDown(right))
            //         rightTotal += 1;

            //     if ((rightTotal == 1) && (rightTimeDelay < 0.5))
            //         rightTimeDelay += Time.deltaTime;

            //     if ((rightTotal == 1) && (rightTimeDelay >= 0.5))
            //     {
            //         rightTimeDelay = 0;
            //         rightTotal = 0;
            //     }

            //     if ((rightTotal == 2))
            //     {
            //         rightTotal = 0;
            //          if((rightTimeDelay < 0.5) && canDash)
            //             {   
            //                 StartCoroutine(Dash(1));
            //                 Debug.Log("DASHED");
            //             }

            //         rightTimeDelay = 0;
            //     }
            // }

            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                state = MovementState.idle;
            }

            if (Input.GetKeyDown(jump))
            {
                if (jumpTimeDelay > 0 && jumpTotal == 1)
                {
                    jumpTotal += 1;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
                else if (isGrounded)
                {
                    jumpTotal = 1;
                    jumpTimeDelay = 0.5f;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }

                if (jumpTimeDelay > 0)
                {
                    jumpTimeDelay -= 1 * Time.deltaTime;
                }
                else
                {
                    jumpTotal = 0;
                }
            }

            if (rightTimeDelay > 0)
            {
                rightTimeDelay -= 0.5f * Time.deltaTime;
            }
            else
            {
                rightTotal = 0;
            }

            if (leftTimeDelay > 0)
            {
                leftTimeDelay -= 0.5f * Time.deltaTime;
            }
            else
            {
                leftTotal = 0;
            }
            // if (Input.GetKeyDown(jump) && isGrounded)
            // {
            //     rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // }

            // double jump logic
            // if (Input.GetKeyDown(jump))
            // {
            //     if (isGrounded)
            //     {
            //         rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //         jumpTotal += 1;
            //     }
            //     if ((jumpTotal == 1) && (jumpTimeDelay < 0.5))
            //         jumpTimeDelay += Time.deltaTime;

            //     if ((jumpTotal == 1) && (jumpTimeDelay >= 0.5))
            //     {
            //         jumpTimeDelay = 0;
            //         jumpTotal = 0;
            //     }

            //     if ((jumpTotal == 2))
            //     {
            //         jumpTotal = 0;
            //          if((jumpTimeDelay < 0.5))
            //             rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            //         jumpTimeDelay = 0;
            //     }

            // }

            if (rb.velocity.y > 0.1f)
            {
                state = MovementState.jumping;
            }
            else if (rb.velocity.y < -0.1f)
            {
                state = MovementState.falling;
            }

            anim.SetInteger("state", (int)state);
        }

        // dash routine -- need to identify why not working
        private IEnumerator Dash(int direction = 0)
        {
            canDash = false;
            isDashing = true;
            float OriginalGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            // probably error in below line
            rb.velocity = new Vector2(direction * dashingPower, 0f);

            TrailRenderer.emitting = true;
            yield return new WaitForSeconds(dashingTime);
            TrailRenderer.emitting = false;
            rb.gravityScale = OriginalGravity;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;

        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            if (playerStats != null)
            {
                playerStats.GetComponent<PlayerStats>().setHealth(currentHealth);
            }
        }

        public void CollectPoints(int points)
        {
            currentScore += points;
            if (playerStats != null)
            {
                playerStats.GetComponent<PlayerStats>().setScore(currentScore);
            }
        }

        public void ResetHealth(int newHealth = 0, bool resetToMax = true)
        {
            if (resetToMax)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth = newHealth;
            }
            playerStats.GetComponent<PlayerStats>().SetMaxHealth(currentHealth);
        }

        public void ResetScore()
        {
            currentScore = 0;
        }

    }
}

