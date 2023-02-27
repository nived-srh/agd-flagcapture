using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AGD
{
    public class GameInitiator : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {   
            Debug.Log("Enter");
            if (collision.gameObject.tag == "Player")
                SceneManager.LoadScene(1);
        }
    }
}
