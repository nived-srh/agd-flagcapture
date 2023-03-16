using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AGD
{
    public class GameInitiator : MonoBehaviour
    {
        public string gameMode = "SOLO";
        private void OnCollisionEnter2D(Collision2D collision)
        {   
            if (collision.gameObject.tag == "Player")
                GameManager.instance.changeGameMode(gameMode);
                GameManager.instance.changeScene("PLAY");
        }
    }
}
