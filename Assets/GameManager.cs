using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class GameManager : MonoBehaviour
    {
        GameObject p1;
        // Start is called before the first frame update
        void Start()
        {
            p1 = GameObject.Find("PlayerOne");
            p1.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
