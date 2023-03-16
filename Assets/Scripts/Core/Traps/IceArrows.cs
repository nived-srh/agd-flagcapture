using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class IceArrows : MonoBehaviour
    {

        public int damage;
        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player>().currentHealth -= damage;
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("Platform"))
            {
                Destroy(gameObject);
            }
        }

    }

}