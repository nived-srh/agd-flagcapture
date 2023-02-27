using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AGD
{
    public class PlayerMovement : MonoBehaviour
    {
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

        // private float inputX;
        
        // Start is called before the first frame update
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>(); 
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {

            MovementState state;

            if (Input.GetKey(left))
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                state = MovementState.running;
                // anim.SetBool("running", true);
                sprite.flipX = true;
            }
            else if(Input.GetKey(right))
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

        // public void Move(InputAction.CallbackContext context)
        // {
        //     MovementState state;
        //     if (context.performed)
        //     {
        //         float inputX = context.ReadValue<Vector2>().x;
        //         if (inputX < 0)
        //         {
        //             sprite.flipX = true;
        //         }
        //         else if (inputX > 0)
        //         {
        //             sprite.flipX = false;
        //         }
        //         rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
                
        //         if (rb.velocity.x != 0)
        //         {
        //             state = MovementState.running;
        //         }
        //         else
        //         {
        //             state = MovementState.idle;
        //         }
        //     }

        //     else { state = MovementState.idle; }

        //     anim.SetInteger("state", (int)state);
        // }

        // public void Jump(InputAction.CallbackContext context)
        // {
        //     MovementState state;

        //     if (context.performed)
        //     {
        //         rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //         if (rb.velocity.y > 0)
        //         {
        //             state = MovementState.jumping;
        //             if (rb.velocity.y < 0)
        //             {
        //                 state = MovementState.falling;
        //             }
        //         }
        //         else
        //         {
        //             state = MovementState.falling;
        //         }
        //     }

        //     else { state = MovementState.idle; }

        //     anim.SetInteger("state", (int)state);
            
        // }
    }
}
