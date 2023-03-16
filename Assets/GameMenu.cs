using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGD
{
    public class GameMenu : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI menuTitle;
        public TMPro.TextMeshProUGUI gameMode;
        public TMPro.TextMeshProUGUI timeElapsed;

        public GameObject gameOverMenu;
        public GameObject gamePauseMenu;

        public void setTitle(string title)
        {
            menuTitle.text = title;
        }


        public void setMode(string mode)
        {
            gameMode.text = mode;
        }
        
        public void setTime(float time)
        {
            System.TimeSpan timeSpan = System.TimeSpan.FromSeconds( time );
            timeElapsed.text = string.Format( "{0:D2}:{1:D2}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds );
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

        public void toggleGameOverMenu(){
            gameOverMenu.SetActive( !gameOverMenu.activeSelf);
        }
        
        public void togglePauseMenu(){
            gamePauseMenu.SetActive( !gamePauseMenu.activeSelf);
        }
    }
}
