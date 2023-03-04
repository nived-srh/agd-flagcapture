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

            if (playerStats != null)
            {
                playerStats.GetComponent<PlayerStats>().SetMaxHealth(maxHealth);
            }
        }

        void Update()
        {
            MovementState state;

            if (Input.GetKey(left))
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                state = MovementState.running;
                // anim.SetBool("running", true);
                sprite.flipX = true;
            }
            else if (Input.GetKey(right))
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                state = MovementState.running;
                // anim.SetBool("running", true);
                sprite.flipX = false;
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                state = MovementState.idle;
                // anim.SetBool("running", false);
            }

            if (Input.GetKeyDown(jump))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

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
            if(resetToMax){
                currentHealth = maxHealth;
            }else{
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

