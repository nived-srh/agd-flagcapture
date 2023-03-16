using UnityEngine;

namespace AGD
{
    public class FireArrowTrap : MonoBehaviour
    {
        public GameObject spritePrefab;
        public float spawnInterval, spriteSpeed;
        public Transform shootingPosition;

        private float timer = 0.0f;

        void Update()
        {
            // increment timer
            timer += Time.deltaTime;

            // spawn sprite every spawnInterval seconds
            if (timer >= spawnInterval)
            {
                timer = 0.0f;

                // instantiate new sprite
                GameObject sprite = Instantiate(spritePrefab, shootingPosition.position, Quaternion.identity);

                // set sprite velocity to move to the right
                Rigidbody2D rb = sprite.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.right * spriteSpeed;
            }
        }
    }

}