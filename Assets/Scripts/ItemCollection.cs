using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class ItemCollection : MonoBehaviour
    {
        public int value;
        private GameObject player;

        public enum ItemType { POINTS, HEAL, POISON, POWERUP }

        public ItemType itemType = ItemType.POINTS;
        // private Dictionary<int, GameObject> playerObjMap;
        // void Start()
        // {
        //     playerObjMap = new Dictionary<int, GameObject>();
        //     foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        //     {   
        //         playerObjMap[go.GetInstanceID()] = go;
        //     }
        // }

        // private void OnTriggerEnter(Collision2D collision)
        // {
        //     if (collision.gameObject.tag == "Player")
        //     {
        //         player = collision.gameObject;
        //         if( itemType == ItemType.POINTS){
        //             player.GetComponent<Player>().CollectPoints(value);
        //         }else if (itemType == ItemType.HEAL){
        //             player.GetComponent<Player>().TakeDamage(-1 * value);
        //         }

        //         gameObject.SetActive(false);
        //     }

        // }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                player = other.gameObject;
                if (itemType == ItemType.POINTS)
                {
                    player.GetComponent<Player>().CollectPoints(value);
                }
                else if (itemType == ItemType.HEAL)
                {
                    player.GetComponent<Player>().TakeDamage(-1 * value);
                }

                gameObject.SetActive(false);
            }
        }
    }
}
