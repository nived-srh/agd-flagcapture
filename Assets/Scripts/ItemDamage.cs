using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class ItemDamage : MonoBehaviour
    {
        public int damage;
        public float knockbackForce = 20f;
        // public int damage, enemykbForce;
        // public PlayerController playerController;

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
                // playerController.kbForce = enemykbForce;
                // playerController.kbCounter = playerController.kbTotalTime;
                // if (collision.transform.position.x <= transform.position.x)
                // {
                //     playerController.knockFromRight = true;
                // }
                // if (collision.transform.position.x >= transform.position.x)
                // {
                //     playerController.knockFromRight = false;
                // }
                playerObjMap[collision.gameObject.GetInstanceID()].GetComponent<Player>().TakeDamage(damage);

                Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
                if( rb != null ){
                    Vector2 velocity = rb.velocity;
                    velocity.y = knockbackForce;
                    rb.velocity = velocity;
                }
            }

        }
    }
}
