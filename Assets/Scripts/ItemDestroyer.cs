using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class ItemDestroyer : MonoBehaviour
    {
        private GameObject itemDestructionPoint;
        // Start is called before the first frame update
        void Start()
        {
            itemDestructionPoint = GameObject.Find("ItemDestructionPoint");
        }

        // Update is called once per frame
        void Update()
        {
            
            if (transform.position.y < itemDestructionPoint.transform.position.y)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
