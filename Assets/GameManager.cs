using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace AGD
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public enum GameMode { SOLO, VS, COOP }
        public GameMode mode;

        public enum GameState { HOME, LOAD, PLAY, PAUSED, ENDED }
        public GameState state;

        public Dictionary<string, GameObject> playerMap;
        [SerializeField] private KeyCode pause;

        private Scene scene;
        private int activePlayers;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                mode = GameMode.VS;
                state = GameState.HOME;
                activePlayers = 1;
                playerMap = new Dictionary<string, GameObject>();
                DontDestroyOnLoad(this.gameObject);
            }
        }

        void Start()
        {
            initializePlayer("P1", true, false, new Vector2(-26, -3.4f));
            initializePlayer("P2", true, false, new Vector2(26, -3.4f));
        }

        void Update()
        {

            scene = SceneManager.GetActiveScene();

            if (scene.buildIndex == 1)
            {
                if (state == GameState.LOAD)
                {
                    initializePlayer("P1", true, true, new Vector2(-24, 1.5f));
                    if (mode != GameMode.SOLO)
                    {
                        initializePlayer("P2", true, true, new Vector2(24, 1.5f));
                        activePlayers = 2;
                    }
                    else
                    {
                        initializePlayer("P2", false, true, new Vector2(24, 1.5f));
                    }
                    state = GameState.PLAY;
                }

                if (Input.GetKeyDown(pause) && state == GameState.PLAY)
                {
                    state = GameState.PAUSED;
                    Time.timeScale = 0;
                    Debug.Log("PAUSED");
                }
                else if (Input.GetKeyDown(pause) && state == GameState.PAUSED)
                {
                    state = GameState.PLAY;
                    Time.timeScale = 1;
                    Debug.Log("PLAY");
                }


                if (state == GameState.PLAY)
                {
                    if (playerMap["P1"].activeSelf && playerMap["P1"].GetComponent<PlayerHealth>().enabled && playerMap["P1"].GetComponent<PlayerHealth>().currentHealth <= 0)
                    {
                        playerMap["P1"].SetActive(false);
                        activePlayers -= 1;
                    }

                    if (playerMap["P2"].activeSelf && playerMap["P2"].GetComponent<PlayerHealth>().enabled && playerMap["P2"].GetComponent<PlayerHealth>().currentHealth <= 0)
                    {
                        playerMap["P2"].SetActive(false);
                        activePlayers -= 1;
                    }

                    if (activePlayers == 0)
                    {
                        state = GameState.ENDED;
                        Time.timeScale = 0;
                        Debug.Log("ENDED");
                    }

                }
            }
        }

        public void changeScene(string newScene)
        {
            switch (newScene)
            {
                case "HOME":
                    state = GameState.HOME;
                    SceneManager.LoadScene(0);
                    break;
                case "PLAY":
                    state = GameState.LOAD;
                    Debug.Log("PLAY");
                    SceneManager.LoadScene(1);
                    break;
                case "EXIT":
                    mode = GameMode.COOP;
                    break;

            }
        }

        public void changeGameMode(string newMode)
        {
            switch (newMode)
            {
                case "SOLO":
                    mode = GameMode.SOLO;
                    break;
                case "VS":
                    mode = GameMode.VS;
                    break;
                case "COOP":
                    mode = GameMode.COOP;
                    break;

            }
        }

        private void initializePlayer(string name, bool isActive, bool hasHealth, Vector2 spawnPosition)
        {

            playerMap[name] = GameObject.Find(name);

            if (isActive)
            {
                playerMap[name].transform.position = spawnPosition;

                if (hasHealth)
                {
                    playerMap[name].GetComponent<PlayerHealth>().enabled = true;
                }

            }
            else
            {
                playerMap[name].SetActive(false);
            }

        }

    }
}
