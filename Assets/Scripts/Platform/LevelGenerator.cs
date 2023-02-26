using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class LevelGenerator : MonoBehaviour
    {
        public Transform generationPoint;
        public float xPositionOffset;
        public float yPositionOffset;

        public int platformLayoutColumns;
        public int platformLayoutRows;

        public ObjectPooler[] platformObjectPools;
        public ObjectPooler[] mobObjectPools;
        public ObjectPooler[] obstacleObjectPools;
        private int platformSelector;
        private Dictionary<string, int> objectPoolerTypeMap;
        private Dictionary<int, Vector2[]> layoutArray;
        private Vector2[] prevGeneratedLayoutRow;
        private Vector2[] platformDimensions;

        // Start is called before the first frame update
        void Start()
        {
            layoutArray = new Dictionary<int, Vector2[]>();
            objectPoolerTypeMap = new Dictionary<string, int>();
            prevGeneratedLayoutRow = new Vector2[platformLayoutColumns];

            objectPoolerTypeMap["PLATFORM"] = platformObjectPools.Length;
            objectPoolerTypeMap["MOBS"] = mobObjectPools.Length;
            objectPoolerTypeMap["OBSTACLES"] = obstacleObjectPools.Length;
            platformDimensions = new Vector2[objectPoolerTypeMap["PLATFORM"]];

            for (int i = 0; i < platformObjectPools.Length; i++)
            {
                platformDimensions[i] = new Vector2(platformObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x, platformObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.y);
            }

            for (int row = 0; row < platformLayoutRows; row++)
            {
                layoutArray[row] = new Vector2[platformLayoutColumns];
                for (int col = 0; col < layoutArray[row].Length; col++)
                {
                    layoutArray[row][col] = new Vector2(0, 0);

                    if (row == 0)
                    {
                        prevGeneratedLayoutRow[col] = new Vector2(0, 0);
                    }
                }
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
            for (int row = 0; row < platformLayoutRows; row++)
            {
                int itemsInRow = 0;
                layoutStr += " { ";
                layoutArray[row] = new Vector2[platformLayoutColumns];

                transform.position = new Vector3(transform.position.x, transform.position.y + yPositionOffset, transform.position.z);
                for (int col = 0; col < layoutArray[row].Length; col++)
                {
                    bool hasPlatform = false;
                    bool hasMob = true;
                    bool hasObstacle = true;
                    Vector2 tempVector = (row == 0 ? prevGeneratedLayoutRow[col] : layoutArray[row - 1][col]);
                    if (tempVector.y >= 0 && tempVector.y < 10)
                    {
                        hasPlatform = Random.Range(0f, 1f) > (0.2 + itemsInRow / 10.0);
                        if (col > 0)
                        {
                            if (layoutArray[row][col - 1].x > 6)
                            {
                                hasPlatform = false;
                                if (layoutArray[row][col - 1].x > 10)
                                {
                                    col = col - 1 + (int) Mathf.Ceil(layoutArray[row][col - 1].x) / 10;
                                    if ( col > layoutArray[row].Length){
                                        col = layoutArray[row].Length;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                hasPlatform = Random.Range(0f, 1f) > (0.5 - (platformLayoutColumns - itemsInRow) / 10.0);
                            }
                        }
                    }

                    if (hasPlatform)
                    {
                        int platformSelection = filteredPlatformPool();
                        itemsInRow += 1;

                        GameObject platformObj = platformObjectPools[platformSelection].GetPooledObject();
                        layoutArray[row][col] = platformDimensions[platformSelection];
                        platformObj.transform.position = new Vector3((xPositionOffset + (col * 10)) , transform.position.y + surfaceOfPlatform(platformDimensions[platformSelection]).y, platformObj.transform.position.z);
                        platformObj.SetActive(true);

                        if(hasObstacle){
                            platformObj = obstacleObjectPools[0].GetPooledObject();
                            platformObj.transform.position = new Vector3((xPositionOffset + (col * 10)) , transform.position.y + 0.5f + 2 * surfaceOfPlatform(platformDimensions[platformSelection]).y, platformObj.transform.position.z);
                            platformObj.SetActive(true);
                        }

                        if(hasMob){
                            platformObj = mobObjectPools[0].GetPooledObject();
                            platformObj.transform.position = new Vector3((xPositionOffset + (col * 10)) , transform.position.y + 0.5f + 2 * surfaceOfPlatform(platformDimensions[platformSelection]).y, platformObj.transform.position.z);
                            platformObj.SetActive(true);
                        }

                        if (row == platformLayoutRows - 1)
                        {
                            prevGeneratedLayoutRow[col] = layoutArray[row][col];
                        }

                        layoutStr = layoutStr + layoutArray[row][col] + " ";
                    }

                }


                layoutStr += "}";
            }

            layoutStr += " ]";

        }

        private Vector2 surfaceOfPlatform(Vector2 platformDim, string side = "TOP")
        {
            if (side == "LEFT")
            {
                return new Vector2(0.50f * platformDim.x, platformDim.y);
            }
            else if (side == "RIGHT")
            {
                return new Vector2(-0.50f * platformDim.x, platformDim.y);
            }
            else if (side == "BOTTOM")
            {
                return new Vector2(platformDim.x, -0.50f * platformDim.y);
            }

            // By default return TOP surface position
            return new Vector2(platformDim.x, 0.50f * platformDim.y);
        }

        private int filteredPlatformPool(int maxWidth = -1, int maxHeight = -1)
        {
            return (int)Random.Range(0, objectPoolerTypeMap["PLATFORM"] - 0.0000001f);
        }

    }
}
