using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class PlatformGenerator : MonoBehaviour
    {
        public Transform generationPoint;
        public float xPositionOffset;
        public float yPositionOffset;

        public int platformLayoutColumns;
        public int platformLayoutRows;

        public ObjectPooler[] platformObjectPools;
        private int numberOfPlatformTypes;

        private int platformSelector;
        private int[][] layoutArray;
        private int[] prevGeneratedLayoutRow;
        private Vector2[] platformPositionOffset;

        // Start is called before the first frame update
        void Start()
        {
            layoutArray = new int[platformLayoutRows][];
            prevGeneratedLayoutRow = new int[platformLayoutColumns];

            numberOfPlatformTypes = platformObjectPools.Length;
            platformPositionOffset = new Vector2[numberOfPlatformTypes];

            for (int i = 0; i < platformObjectPools.Length; i++)
            {
                platformPositionOffset[i] = new Vector2( platformObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x, platformObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.y / 2 );
            }
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
                    bool hasPlatform = true ;//Random.Range(0f, 1f) > 0.5;

                    if (hasPlatform)
                    {
                        layoutArray[i][j] = (int)Random.Range(0, numberOfPlatformTypes - 0.0000001f);
                        itemsInRow += 1;

                        Debug.Log(layoutArray[i][j]);
                        GameObject platformObj = platformObjectPools[layoutArray[i][j]].GetPooledObject();
                        platformObj.transform.position = new Vector3((xPositionOffset + (j * 10)), transform.position.y + platformPositionOffset[layoutArray[i][j]].y, platformObjectPools[i].pooledObject.transform.position.z);
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
