using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class CameraFollow : MonoBehaviour
    {
        public PlayerMovement thePlayer;
        public int minYForFollow;
        private Vector3 lastPlayerPosition;
        private float distanceToMove;
        // Start is called before the first frame update
        void Start()
        {
            thePlayer = FindAnyObjectByType<PlayerMovement>();
            lastPlayerPosition = thePlayer.transform.position;
        }

        // Update is called once per frame
        void Update()
        {   
            if( thePlayer.transform.position.y >= minYForFollow){
                distanceToMove = thePlayer.transform.position.y - lastPlayerPosition.y;

                transform.position = new Vector3(transform.position.x, transform.position.y + distanceToMove, transform.position.z);

                lastPlayerPosition = thePlayer.transform.position;

            }
        }
    }
}
