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

        public enum GameState { HOME, LOAD, PLAY, PAUSED, ENDED, EXIT }
        public GameState state;

        public Dictionary<string, GameObject> playerMap;
        [SerializeField] private KeyCode pause;

        private Scene scene;
        private int activePlayers;
        public GameObject gameMenu;
        public GameObject gameUI;
        public GameObject settingsMenu;

        public GameObject[] players;

        private float startTime;
        private float currentTime;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                mode = GameMode.SOLO;
                state = GameState.LOAD;
                activePlayers = 1;
                playerMap = new Dictionary<string, GameObject>();
                DontDestroyOnLoad(this.gameObject);
                DontDestroyOnLoad(GameObject.Find("OverlayCanvas"));
                foreach (GameObject player in this.players)
                {
                    Debug.Log(player.name);
                    playerMap[player.name] = player;
                    DontDestroyOnLoad(player);
                }
            }
        }

        void Start()
        {
            Debug.Log(scene.buildIndex);
        }

        void Update()
        {

            scene = SceneManager.GetActiveScene();

            if (scene.buildIndex != 1)
            {
                if (state == GameState.EXIT && scene.isLoaded)
                {
                    //initializePlayer("P1", true, new Vector2(-26, -3.4f));
                    //initializePlayer("P2", true, new Vector2(26, -3.4f));
                    Debug.Log("HOME");
                    state = GameState.HOME;

                    Time.timeScale = 1;
                }
            }

            if (scene.buildIndex == 1)
            {
                if (state == GameState.LOAD && scene.isLoaded)
                {
                    Debug.Log("INITIATE PLAY");
                    activePlayers = 1;
                    initializePlayer("P1", true, new Vector2(-24, 1.5f));
                    if (mode != GameMode.SOLO)
                    {
                        initializePlayer("P2", true, new Vector2(24, 1.5f));
                        playerMap["P2"].GetComponent<Player>().playerStats.SetActive(true);
                        activePlayers = 2;
                    }
                    else
                    {
                        initializePlayer("P2", false, new Vector2(24, 1.5f));
                        playerMap["P2"].GetComponent<Player>().playerStats.SetActive(false);
                    }
                    state = GameState.PLAY;
                    playerMap["P1"].GetComponent<Player>().ResetHealth(100, true);
                    playerMap["P2"].GetComponent<Player>().ResetHealth(100, true);
                    playerMap["P1"].GetComponent<Player>().ResetScore();
                    playerMap["P2"].GetComponent<Player>().ResetScore();
                    
                    gameMenu.GetComponent<GameMenu>().toggleGameOverMenu();
                    gameMenu.GetComponent<GameMenu>().togglePauseMenu();
                    gameMenu.gameObject.SetActive(false);
                    gameUI.gameObject.SetActive(true);
                    gameUI.GetComponent<GameMenu>().setMode(mode.ToString());

                    Time.timeScale = 1;
                    startTime = Time.time;
                    gameUI.GetComponent<GameMenu>().setTime((Mathf.Round((Time.time - startTime) * 100.0f) / 100.0f));
                }

                if (Input.GetKeyDown(pause) && state == GameState.PLAY)
                {
                    state = GameState.PAUSED;
                    gameMenu.gameObject.SetActive(true);
                    gameMenu.GetComponent<GameMenu>().setTitle("Game Paused");
                    gameMenu.GetComponent<GameMenu>().togglePauseMenu();
                    Time.timeScale = 0;
                    Debug.Log("PAUSED");
                }
                else if (Input.GetKeyDown(pause) && state == GameState.PAUSED)
                {
                    state = GameState.PLAY;
                    Time.timeScale = 1;
                    Debug.Log("PLAY");
                    gameMenu.GetComponent<GameMenu>().togglePauseMenu();
                    gameMenu.gameObject.SetActive(false);
                }


                if (state == GameState.PLAY)
                {
                    if (playerMap["P1"] != null)
                    {
                        if (playerMap["P1"].activeSelf && playerMap["P1"].GetComponent<Player>().currentHealth <= 0)
                        {
                            playerMap["P1"].SetActive(false);
                            activePlayers -= 1;
                        }

                    }
                    if (playerMap["P2"] != null)
                    {
                        if (playerMap["P2"].activeSelf && playerMap["P2"].GetComponent<Player>().currentHealth <= 0)
                        {
                            playerMap["P2"].SetActive(false);
                            activePlayers -= 1;
                        }
                    }

                    if (activePlayers == 0)
                    {
                        state = GameState.ENDED;
                        Time.timeScale = 0;
                        Debug.Log("ENDED");
                        gameMenu.gameObject.SetActive(true);
                        gameMenu.GetComponent<GameMenu>().setTitle("Game Over");
                        gameMenu.GetComponent<GameMenu>().setTime(currentTime);
                        gameMenu.GetComponent<GameMenu>().toggleGameOverMenu();
                    }
                    else if (activePlayers == 1 && mode == GameMode.COOP)
                    {
                        state = GameState.ENDED;
                        Time.timeScale = 0;
                        Debug.Log("ENDED");
                        gameMenu.gameObject.SetActive(true);
                        gameMenu.GetComponent<GameMenu>().setTitle("Game Over");
                        gameMenu.GetComponent<GameMenu>().setTime(currentTime);
                    }

                    currentTime = (Mathf.Round((Time.time - startTime) * 100.0f) / 100.0f);
                    gameUI.GetComponent<GameMenu>().setTime(currentTime);

                }
            }
        }

        public void changeScene(string newScene)
        {
            switch (newScene)
            {
                case "HOME":
                    state = GameState.EXIT;
                    gameMenu.gameObject.SetActive(false);
                    gameUI.gameObject.SetActive(false);
                    playerMap["P1"].SetActive(false);
                    playerMap["P2"].SetActive(false);
                    SceneManager.LoadScene(0);
                    break;
                case "PLAY":
                    state = GameState.LOAD;
                    Debug.Log("PLAY");
                    SceneManager.LoadScene(1);
                    break;
                case "EXIT":
                    state = GameState.EXIT;
                    Debug.Log("EXIT");
                    Application.Quit();
                    break;
            }
        }

        // void LateUpdate() {
        //     if (scene.buildIndex == 1)
        //     {
        //         gameUI.GetComponent<GameMenu>().setTime((Mathf.Round((Time.time - startTime) * 100.0f) / 100.0f));
        //     }
        // }

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

        private void initializePlayer(string name, bool isActive, Vector2 spawnPosition)
        {

            if (playerMap[name] != null)
            {
                if (isActive)
                {
                    playerMap[name].SetActive(true);
                    playerMap[name].transform.position = spawnPosition;
                }
                else
                {
                    playerMap[name].SetActive(false);
                }

            }

        }

    }
}
