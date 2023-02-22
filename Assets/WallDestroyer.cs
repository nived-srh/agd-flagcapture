using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class WallDestroyer : MonoBehaviour
    {
        private GameObject wallDestructionPoint;
        // Start is called before the first frame update
        void Start()
        {
            wallDestructionPoint = GameObject.Find("PlatformDestructionPoint");
        }

        // Update is called once per frame
        void Update()
        {
            
            if (transform.position.y < wallDestructionPoint.transform.position.y)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
