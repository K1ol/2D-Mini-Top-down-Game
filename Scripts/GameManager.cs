using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

public enum GameState
{
    menu,
    inGame,
    gameOver,
    mainMenu,
    win
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.mainMenu;
    public static GameManager instance;
    public Canvas mainMenuCanvas;
    public Canvas menuCanvas;
    public Canvas inGameCanvas;
    public Canvas gameOverCanvas;
    public Canvas winCanvas;
    public float timer = 10;
    private int spacePressCount = 0;
    int firstSceneIndex=0;
    int secondSceneIndex = 1;
    public int score;




    void Awake()
    {
        instance = this;
        

    }

    void Start()
    {
        currentGameState = GameState.mainMenu;
        ObstaclesManager.Instance.usedIndex.Clear();
        ObstaclesManager.Instance.StartObs();
        if (SceneManager.GetActiveScene().buildIndex == firstSceneIndex)
        {
            mainMenuCanvas.enabled = true;
            //SoundManager.PlayBgm();
            Global.PlayerScore = 0;
        }
        else
        {
            score = Global.PlayerScore;
            
            StartGame();
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.N))
            
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressCount++; 

            switch (spacePressCount)
            {
                case 1:
                    
                    BackToMenu(); 
                    break;
                case 2:
                    
                    Resume(); 
                    break;
                default:
                    spacePressCount = 0; 
                    break;
            }
        }

        if(currentGameState == GameState.inGame)
        {
            if (timer - Time.deltaTime > 0f)
            {
                timer -= Time.deltaTime;
            } 
            else if (timer - Time.deltaTime <= 0f && SceneManager.GetActiveScene().buildIndex == firstSceneIndex)
            {
                timer = 0;
                NextLevel();
            }
            else 
            {
                Win();
            }
        }
        
    }

    public void StartGame()
    {
        //Debug.Log("StartGame!!!");

        SetGameState(GameState.inGame);
    }

    public void RestartGame()
    {
        //Debug.Log("RestartGame!!!");
        PlayerMovement.instance.StartGame();
        Hero.instance.MaxHealth();
        timer = 30;
        SetGameState(GameState.inGame);
        spacePressCount = 0;
    }

    public void Win()
    {
        SaveHighScore(score);
        SetGameState(GameState.win);
    }


    public void Resume()
    {
        SetGameState(GameState.inGame);
        spacePressCount = 0;

    }


    public void MainMenu()
    {
        SetGameState(GameState.mainMenu);
    }


    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
        spacePressCount = 1;

    }

    void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {
            //setup Unity for menu state
            menuCanvas.enabled = true;
            inGameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
            mainMenuCanvas.enabled = false;
            winCanvas.enabled = false;

        }
        else if(newGameState == GameState.inGame)
        {
            //setup for inGame
            menuCanvas.enabled = false;
            inGameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
            mainMenuCanvas.enabled = false;
            winCanvas.enabled = false;
        }
        else if(newGameState == GameState.gameOver)
        {
            //setup for gameOver state
            menuCanvas.enabled = false;
            menuCanvas.enabled = false;
            gameOverCanvas.enabled = true;
            mainMenuCanvas.enabled = false;
            winCanvas.enabled = false;
        }
        else if(newGameState == GameState.mainMenu)
        {
            menuCanvas.enabled = false;
            menuCanvas.enabled = false;
            gameOverCanvas.enabled = false;
            mainMenuCanvas.enabled = true;
            winCanvas.enabled = false;
        }
        else if(newGameState == GameState.win)
        {
            menuCanvas.enabled = false;
            menuCanvas.enabled = false;
            gameOverCanvas.enabled = false;
            mainMenuCanvas.enabled = false;
            winCanvas.enabled = true;
        }


        currentGameState = newGameState;

    }


    public void ReloadToMain()
    {
        SceneManager.LoadScene(firstSceneIndex, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        
        //UnityEditor.EditorApplication.isPlaying = false; // 在Unity Editor环境下停止游戏
        
        Application.Quit(); 
        
    }

    void NextLevel()
    {
        Global.PlayerScore = score;
        SceneManager.LoadScene(secondSceneIndex, LoadSceneMode.Single);
    }

    public static void SaveHighScore(int newScore)
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (newScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", newScore);
            PlayerPrefs.Save(); // 保存更改
        }
    }
}
