using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class WallGenerator : MonoBehaviour
    {
        public GameObject sideWall;
        public Transform generationPoint;
        public float wallGap;

        public ObjectPooler theObjectPool;


        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.y < generationPoint.position.y)
            {
                GameObject sideWall = theObjectPool.GetPooledObject();
                sideWall.transform.position = new Vector3(transform.position.x, transform.position.y + wallGap, transform.position.z);
                sideWall.SetActive(true);
                transform.position = new Vector3(transform.position.x, transform.position.y + wallGap, transform.position.z);
            }

        }
    }
}
