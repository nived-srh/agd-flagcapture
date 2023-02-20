using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class PlatformGenerator : MonoBehaviour
    {
        public GameObject thePlatform;
        public int numberOfPlatformTypes;
        public Transform generationPoint;
        public float xPositionOffset;
        public float yPositionOffset;

        public ObjectPooler[] theObjectPools;
        private int[][] layoutArray = new int[4][];

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position.y < generationPoint.position.y)
            {
                randomizeLayoutArray();
            }
        }

        private void randomizeLayoutArray()
        {
            layoutArray[0] = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            layoutArray[1] = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            layoutArray[2] = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            layoutArray[3] = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };

            string layoutStr = " layoutArray [";
            int[] itemsInColumn = new int[4];
            for (int i = 0; i < layoutArray.Length; i++)
            {
                int itemsInRow = 0;
                layoutStr += " { ";

                transform.position = new Vector3(transform.position.x, transform.position.y + yPositionOffset, transform.position.z);
                for (int j = 0; j < layoutArray[i].Length; j++)
                {
                    bool hasPlatform = Random.Range(0f, 1f) > 0.5;
                    //int platformType = (int)Random.Range(0, numberOfPlatformTypes);

                    if (hasPlatform)
                    {
                        layoutArray[i][j] = 1;//platformType;
                        itemsInRow += 1;
                    }

                    if (layoutArray[i][j] == 1)
                    {
                        GameObject platformObj = theObjectPools[0].GetPooledObject();
                        platformObj.transform.position = new Vector3((xPositionOffset + (j * 10)), transform.position.y, transform.position.z);
                        platformObj.SetActive(true);

                        //Instantiate(thePlatform, platformPos, transform.rotation);
                    }
                    
                    layoutStr = layoutStr + layoutArray[i][j] + " ";
                }

                layoutStr += "}";
            }

            layoutStr += " ]";
            Debug.Log(layoutStr);

        }
    }
}
