using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{

    public class ProjectileShooting : MonoBehaviour
    {


        public GameObject bullet;
        public AudioSource audioSource;
        public AudioClip audioClip;
        public Transform bulletPos;
        private float timer;
        private GameObject player;
        [Tooltip("Vertical distance checked to enable shooting projectiles to the player")]
        public float distanceCheck;
        private Animator animator;



        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            animator = GetComponent<Animator>();
            audioSource.clip = audioClip;
        }

        // Update is called once per frame
        void Update()
        {

            float distance = Mathf.Abs(transform.position.y - player.transform.position.y);

            if (distance <= distanceCheck)
            {
                timer += Time.deltaTime;
                if (timer > 3f)
                {
                    timer = 0f;
                    audioSource.Play();
                    shoot();
                }
            }

        }

        void shoot()
        {
            animator.Play("attack");
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
            StartCoroutine(ResetAnimation());
        }

        IEnumerator ResetAnimation()
        {
            yield return new WaitForSeconds(0.1f);
            animator.Play("fly");
        }
    }
}