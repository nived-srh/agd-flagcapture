using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class LavaMovement : MonoBehaviour
    {
        public GameObject mainCamera;
        public Vector3 oldPosition;
        public float lerpSpeed = 0.01f;
        // Start is called before the first frame update
        void Start()
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            oldPosition = transform.position;
        }

        void FixedUpdate()
        {

            if (mainCamera.transform.position.y - oldPosition.y > 17)
            {
                if (mainCamera.transform.position.y - oldPosition.y > 22)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(oldPosition.x, mainCamera.transform.position.y + 22, oldPosition.z), lerpSpeed);
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(oldPosition.x, oldPosition.y + 1, oldPosition.z), lerpSpeed);
                }

            }
            else if (GameManager.instance.state != GameManager.GameState.ENDED)
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 1, mainCamera.transform.position.z), lerpSpeed);
                transform.position = Vector3.Lerp(transform.position, new Vector3(oldPosition.x, oldPosition.y + 1, oldPosition.z), lerpSpeed);
            }

            oldPosition = transform.position;

        }

    }
}
