using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class PlatformGenerator : MonoBehaviour
    {   
        public GameObject thePlatform;
        public Transform generationPoint;        
        public float xPositionMin;
        public float xPositionMax;

        private float platformWidth;
        private float distanceBetween;


        // Start is called before the first frame update
        void Start()
        {
            platformWidth = 10;
        }

        // Update is called once per frame
        void Update()
        {

            distanceBetween = Random.Range(xPositionMin, xPositionMax); 
            if(transform.position.y < generationPoint.position.y){
                transform.position = new Vector3(distanceBetween, transform.position.y + platformWidth, transform.position.z);
                Instantiate ( thePlatform, transform.position, transform.rotation);
            }
        
        }
    }
}
