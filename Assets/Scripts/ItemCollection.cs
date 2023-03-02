using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class ItemCollection : MonoBehaviour
    {
        public int points;
        private Dictionary<int, GameObject> playerObjMap;
        void Start()
        {
            playerObjMap = new Dictionary<int, GameObject>();
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
            {   
                playerObjMap[go.GetInstanceID()] = go;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                playerObjMap[collision.gameObject.GetInstanceID()].GetComponent<Player>().CollectPoints(points);
                gameObject.SetActive(false);
            }

        }
    }
}
