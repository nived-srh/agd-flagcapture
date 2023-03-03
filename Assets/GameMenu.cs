using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class GameMenu : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI menuTitle;

        void Awake() {
            menuTitle.text = "";    
        }

        public void setTitle(string title)
        {
            menuTitle.text = title;
        }

        public void restartGame(){
            GameManager.instance.changeScene("PLAY");
        }

        public void exitToMenu(){
            GameManager.instance.changeScene("HOME");
        }

        public void exitToDesktop(){
            GameManager.instance.changeScene("EXIT");
        }
    }
}
