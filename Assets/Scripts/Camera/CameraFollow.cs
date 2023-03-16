using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class CameraFollow : MonoBehaviour
    {
        private GameObject thePlayer;
        private GameObject theLava;
        private Vector3 lastPlayerPosition;
        private float distanceToMove;
        // Start is called before the first frame update
        void Start()
        {
            thePlayer = GameObject.FindGameObjectWithTag("Player");
            if (thePlayer != null){
                lastPlayerPosition = thePlayer.transform.position;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (thePlayer != null)
            {
                if (thePlayer.transform.position.y > transform.position.y  || (false && thePlayer.transform.position.y < transform.position.y && thePlayer.transform.position.y > transform.position.y - 15 && transform.position.y > 20))
                {
                    distanceToMove = thePlayer.transform.position.y - transform.position.y;

                    transform.position = new Vector3(transform.position.x, transform.position.y + distanceToMove, transform.position.z);

                    lastPlayerPosition = transform.position;

                }

            }
            else
            {
                thePlayer = GameObject.FindGameObjectWithTag("Player");
            }
        }

    }
}
