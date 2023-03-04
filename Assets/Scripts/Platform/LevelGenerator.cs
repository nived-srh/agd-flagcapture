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
        public ObjectPooler[] floorPlatformObjectPools;
        public ObjectPooler[] mobObjectPools;
        public ObjectPooler[] obstacleObjectPools;
        public ObjectPooler[] collectibleObjectPools;
        private int platformSelector;
        private Dictionary<string, int> objectPoolerTypeMap;
        private Dictionary<int, Vector2[]> layoutArray;
        private Dictionary<int, bool[]> platformMap;
        private Dictionary<int, GameObject[]> layoutObjectArray;
        private Vector2[] prevGeneratedLayoutRow;
        private GameObject[] prevGeneratedObjectRow;

        // Start is called before the first frame update
        void Start()
        {
            platformMap = new Dictionary<int, bool[]>();
            layoutArray = new Dictionary<int, Vector2[]>();
            layoutObjectArray = new Dictionary<int, GameObject[]>();
            objectPoolerTypeMap = new Dictionary<string, int>();
            prevGeneratedLayoutRow = new Vector2[platformLayoutColumns];
            prevGeneratedObjectRow = new GameObject[platformLayoutColumns];

            objectPoolerTypeMap["PLATFORM"] = platformObjectPools.Length;
            objectPoolerTypeMap["FLOORPLATFORM"] = floorPlatformObjectPools.Length;
            objectPoolerTypeMap["MOBS"] = mobObjectPools.Length;
            objectPoolerTypeMap["OBSTACLES"] = obstacleObjectPools.Length;
            objectPoolerTypeMap["COLLECTIBLE"] = collectibleObjectPools.Length;

            for (int row = 0; row < platformLayoutRows; row++)
            {
                platformMap[row] = new bool[platformLayoutColumns];
                layoutArray[row] = new Vector2[platformLayoutColumns];
                layoutObjectArray[row] = new GameObject[platformLayoutColumns];
                for (int col = 0; col < platformLayoutColumns; col++)
                {
                    platformMap[row][col] = true;
                    layoutArray[row][col] = new Vector2(0, 0);
                    layoutObjectArray[row][col] = null;

                    if (row == 0)
                    {
                        prevGeneratedLayoutRow[col] = new Vector2(0, 0);
                        prevGeneratedObjectRow[col] = null;
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
                float maxHeightInRow = 0f;
                layoutStr += " { ";
                platformMap[row][(int)Mathf.Floor(Random.Range(0, platformLayoutColumns - 0.000001f))] = (Random.Range(0f, 1f) > 0.5);
                platformMap[row][(int)Mathf.Floor(Random.Range(1, platformLayoutColumns - 1f))] = false;

                transform.position = new Vector3(transform.position.x, transform.position.y + yPositionOffset, transform.position.z);
                for (int col = 0; col < layoutArray[row].Length; col++)
                {
                    bool hasPlatform = platformMap[row][col];
                    bool hasMob = (col > 0 && col < 7 ? true : false);
                    bool hasObstacle = Random.Range(0f, 1f) > 0.5;
                    bool hasCollectible = Random.Range(0f, 1f) > 0.5;

                    Vector2 tempVector = (row == 0 ? prevGeneratedLayoutRow[col] : layoutArray[row - 1][col]);
                    Vector2 platformRandOffset = new Vector2(0, 0);

                    // if (tempVector.y > 7){
                    //     float offsetY =  tempVector.y - 5;
                    //     //platformRandOffset = new Vector2(platformRandOffset.x, -10);
                    // }

                    // if (tempVector.y >= 0 )
                    // {
                    //     hasPlatform = Random.Range(0f, 1f) > (0.3 + (itemsInRow / 10.0));
                    //     if (col > 0)
                    //     {
                    //         if (layoutArray[row][col - 1].x > 6)
                    //         {
                    //             hasPlatform = false;
                    //             if (layoutArray[row][col - 1].x > 10)
                    //             {
                    //                 col = col - 1 + (int) Mathf.Ceil(layoutArray[row][col - 1].x) / 10;
                    //                 if ( col > layoutArray[row].Length){
                    //                     col = layoutArray[row].Length;
                    //                     break;
                    //                 }
                    //             }
                    //         }
                    //         else
                    //         {
                    //             hasPlatform = Random.Range(0f, 1f) > (0.5 - (platformLayoutColumns - itemsInRow) / 10.0);
                    //         }

                    //         if(row > 0 ){
                    //             if(layoutArray[row - 1][col-1].y > 7){
                    //                 //platformRandOffset = new Vector2( ( Random.Range(0f, 1f) > 0.5 ? -1 : 1) * Random.Range(4f, 8f) , platformRandOffset.y);
                    //             }
                    //         }
                    //     }
                    // }

                    if (hasPlatform)
                    {

                        GameObject platformObj = floorPlatformObjectPools[0].GetPooledObject();

                        int platformSelection = (Random.Range(0f, 1f) > 0.7 ? filteredPlatformPool("FLOORPLATFORM") : 0);

                        platformObj = floorPlatformObjectPools[platformSelection].GetPooledObject();
                        layoutArray[row][col] = platformObj.GetComponent<BoxCollider2D>().size;
                        platformObj.transform.position = new Vector3((xPositionOffset + (col * 10)) + platformRandOffset.x, transform.position.y + surfaceOfPlatform(platformObj.GetComponent<BoxCollider2D>().size).y + platformRandOffset.y, platformObj.transform.position.z);
                        platformObj.SetActive(true);

                        if (Random.Range(0f, 1f) > 0.5 && platformSelection == 0)
                        {
                            platformSelection = filteredPlatformPool("PLATFORM");
                            platformObj = platformObjectPools[platformSelection].GetPooledObject();
                            layoutArray[row][col] = platformObj.GetComponent<BoxCollider2D>().size;
                            platformObj.transform.position = new Vector3((xPositionOffset + (col * 10)) + platformRandOffset.x, transform.position.y + surfaceOfPlatform(platformObj.GetComponent<BoxCollider2D>().size).y + platformRandOffset.y, platformObj.transform.position.z);
                            platformObj.SetActive(true);

                        }

                        itemsInRow += 1;

                        if (maxHeightInRow < layoutArray[row][col].y)
                        {
                            maxHeightInRow = layoutArray[row][col].y;
                            yPositionOffset = maxHeightInRow + 7;
                        }

                        GameObject renderObj;
                        if (hasObstacle)
                        {
                            renderObj = obstacleObjectPools[0].GetPooledObject();
                            renderObj.transform.position = new Vector3((xPositionOffset + (col * 10)) + platformRandOffset.x, transform.position.y + 0.5f + 2 * surfaceOfPlatform(platformObj.GetComponent<BoxCollider2D>().size).y, renderObj.transform.position.z);
                            renderObj.SetActive(true);
                        }

                        if (hasCollectible)
                        {
                            renderObj = collectibleObjectPools[0].GetPooledObject();
                            renderObj.transform.position = new Vector3((xPositionOffset + (col * 10)) + platformRandOffset.x, transform.position.y + 0.5f + 2 * surfaceOfPlatform(platformObj.GetComponent<BoxCollider2D>().size).y + 4f, renderObj.transform.position.z);
                            renderObj.SetActive(true);
                        }

                        if (hasMob && layoutArray[row][col].x > 5)
                        {
                            renderObj = mobObjectPools[(int)Random.Range(0, objectPoolerTypeMap["MOBS"] - 0.0000001f)].GetPooledObject();
                            renderObj.transform.position = new Vector3((xPositionOffset + (col * 10)) + platformRandOffset.x, transform.position.y + 0.5f + 2 * surfaceOfPlatform(platformObj.GetComponent<BoxCollider2D>().size).y, renderObj.transform.position.z);
                            renderObj.SetActive(true);
                        }

                        if (row == platformLayoutRows - 1)
                        {
                            prevGeneratedLayoutRow[col] = layoutArray[row][col];
                        }

                    }

                    layoutStr = layoutStr + platformMap[row][col] + " ";

                }


                layoutStr += "}";
            }

            layoutStr += " ]";

            Debug.Log(layoutStr);

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

        private int filteredPlatformPool(string platformType, int maxWidth = -1, int maxHeight = -1)
        {
            return (int)Random.Range(0, objectPoolerTypeMap[platformType] - 0.0000001f);
        }

    }
}
