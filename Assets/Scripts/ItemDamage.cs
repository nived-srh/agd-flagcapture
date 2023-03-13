using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class ItemDamage : MonoBehaviour
    {
        public int damage;
        public float knockbackForce = 20f;
        private GameObject player;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                player = collision.gameObject;
                player.GetComponent<Player>().TakeDamage(damage);

                Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 velocity = rb.velocity;
                    velocity.y = knockbackForce;
                    velocity.x = -1 * velocity.x;
                    rb.velocity = velocity;
                }
            }

        }
    }
}
