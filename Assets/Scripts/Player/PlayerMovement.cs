using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AGD
{
    public class PlayerMovement : MonoBehaviour
    {   
        public Rigidbody2D theRB; 
        public float moveSpeed;
        public float jumpForce;
        private float inputX, inputY;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            theRB.velocity = new Vector2(inputX * moveSpeed, theRB.velocity.y);
        }
        public void Move(InputAction.CallbackContext context)
        {
            inputX = context.ReadValue<Vector2>().x;
            
        }
        public void Jump(InputAction.CallbackContext context)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            
        }
    }

}
