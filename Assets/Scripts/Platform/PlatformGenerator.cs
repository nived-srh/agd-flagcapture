using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class PlatformGenerator : MonoBehaviour
    {
        public GameObject thePlatform;
        public Transform generationPoint;
        public float xPositionOffset;
        public float yPositionOffset;

        public int platformLayoutColumns;
        public int platformLayoutRows;

        public ObjectPooler[] theObjectPools;
        private int numberOfPlatformTypes;

        private int platformSelector;
        private int[][] layoutArray;
        private int[] prevGeneratedLayoutRow;

        // Start is called before the first frame update
        void Start()
        {
            layoutArray = new int[platformLayoutRows][];
            prevGeneratedLayoutRow = new int[platformLayoutColumns];
            numberOfPlatformTypes = theObjectPools.Length;
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

            string layoutStr = " layoutArray [";
            int[] itemsInColumn = new int[4];
            for (int i = 0; i < layoutArray.Length; i++)
            {
                int itemsInRow = 0;
                layoutStr += " { ";
                layoutArray[i] = new int[platformLayoutColumns];

                transform.position = new Vector3(transform.position.x, transform.position.y + yPositionOffset, transform.position.z);
                for (int j = 0; j < layoutArray[i].Length; j++)
                {
                    layoutArray[i][j] = 0;
                    bool hasPlatform = Random.Range(0f, 1f) > 0.5;
                    //int platformType = (int)Random.Range(0, numberOfPlatformTypes);

                    if (hasPlatform)
                    {
                        layoutArray[i][j] = (int)Random.Range(0, numberOfPlatformTypes - 0.0000001f); //platformType;
                        itemsInRow += 1;

                        GameObject platformObj = theObjectPools[layoutArray[i][j]].GetPooledObject();
                        platformObj.transform.position = new Vector3((xPositionOffset + (j * 10)), transform.position.y, transform.position.z);
                        platformObj.SetActive(true);
                    }


                    if (i == layoutArray.Length - 1)
                    {
                        prevGeneratedLayoutRow[j] = layoutArray[i][j];
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
