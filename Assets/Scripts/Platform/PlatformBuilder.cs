using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class PlatformBuilder : MonoBehaviour
    {
        public PlatformSprites platformSprites;

        private int platformLength = 0;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(platformSprites.sprites);
        }

        public GameObject generateNewPlatform(){

            
            return new GameObject();
        }
    }
}
