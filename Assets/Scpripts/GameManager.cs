using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEditor.Build;
using UnityEditor.ShaderGraph;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState {
        GS_PAUSEMENU,
        GS_GAME,
        GS_LEVELCOMPLETED,
        GS_GAME_OVER,
        GS_OPTIONS
    }
    public const string highScore = "HighScoreLevel1";
    public Canvas pauseMenuCanvas;

    public Canvas levelCompletedCanvas;

    public Canvas optionCanvas;

    public static GameManager instance;

    public Canvas inGameCanvas;

    public GameState currentGameState;

    public TMP_Text scoreText;

    public TMP_Text currentTime;

    public TMP_Text enemiesKilledText;
    public TMP_Text scoreTxt;
    public TMP_Text highScoreTxt;
    public TMP_Text currQualityLevel;

    private int score = 0;

    private float timer = 0.0f;

    public int keysFound = 0;

    private int enemiesKilled = 0;

    public int lifes = 3;

    public Image[] keysTab;

    public Image[] heartsTab;

    public Slider suwak;




    private void Awake()
    {
        instance = this;
        this.currentGameState = GameState.GS_GAME;
        this.scoreText.text = score.ToString();
        currentTime.text = string.Format("{0:00}:{1:00}", Math.Floor(timer / 60f), timer % 60f);
        this.enemiesKilledText.text = enemiesKilled.ToString();
        currQualityLevel.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
        if (!PlayerPrefs.HasKey("keyHighScore"))
        {
            PlayerPrefs.SetInt("keyHighScore", 0);
        }

        if (!PlayerPrefs.HasKey("keyHighScore2"))
        {
            PlayerPrefs.SetInt("keyHighScore2", 0);
        }

        //if (!PlayerPrefs.HasKey("lvl1Completed"))
        //{
        //    PlayerPrefs.SetInt("keyCompleted", 0);
        //}

        InGame();


        for (int i = 0; i < keysTab.Length; i++)
        {
            keysTab[i].color = Color.grey;
        }

        this.enemiesKilledText.text = enemiesKilled.ToString();
        for (int i = 0; i < 3 - enemiesKilled.ToString().Length; i++)
        {
            this.enemiesKilledText.text = '0' + enemiesKilledText.text;
        }

        this.scoreText.text = score.ToString();
        for (int i = 0; i < 3 - score.ToString().Length; i++)
        {
            this.scoreText.text = '0' + scoreText.text;
        }


    }



    // Start is called before the first frame update
    void Start()
    {
        suwak.onValueChanged.AddListener(delegate { SetVolume(suwak.value); });
    }

    // Update is called once per frame
    void Update()
    {



        currentTime.text = string.Format("{0:00}:{1:00}", Math.Floor(timer / 60f), timer % 60f);

        if (currentGameState == GameState.GS_GAME) timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentGameState == GameState.GS_PAUSEMENU)
            {
                InGame();
            }
            else if (currentGameState == GameState.GS_GAME)
            {
                PauseMenu();
            }

        }

    }

    public void OnResumeButtonClicked()
    {
        InGame();
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnReturnToMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnOptionsButtonClicked()
    {
        Options();
    }

    public void IncreseQuality()
    {
        QualitySettings.IncreaseLevel();
        currQualityLevel.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
    }
	public void DecreseQuality()
	{
		QualitySettings.DecreaseLevel();
		currQualityLevel.text = QualitySettings.names[QualitySettings.GetQualityLevel()];
	}

    public void SetVolume(float volume)
    {
        float vol = AudioListener.volume;

        AudioListener.volume = volume;
    }



    void SetGameState(GameState newGameState)
    {
        inGameCanvas.enabled = newGameState == GameState.GS_GAME;
        pauseMenuCanvas.enabled = newGameState == GameState.GS_PAUSEMENU;
        levelCompletedCanvas.enabled = newGameState == GameState.GS_LEVELCOMPLETED;
        optionCanvas.enabled = newGameState == GameState.GS_OPTIONS;


		if (newGameState == GameState.GS_LEVELCOMPLETED)
		{
			//pauseMenuCanvas.enabled = false;
			//inGameCanvas.enabled = false;
			//levelCompletedCanvas.enabled = true;

			var currentScene = SceneManager.GetActiveScene();
			if (currentScene.name == "Level1")
			{
			    var highScore = PlayerPrefs.GetInt("keyHighScore");
				if (highScore < score)
				{
					PlayerPrefs.SetInt("keyHighScore", score);
					highScore = score;
				}

				scoreTxt.text = "Your score = " + score.ToString();
				highScoreTxt.text = "You highscore = " + highScore.ToString();

                

			}

            if(currentScene.name == "Level2")
            {
				var highScore = PlayerPrefs.GetInt("keyHighScore2");
				if (highScore < score)
				{
					PlayerPrefs.SetInt("keyHighScore2", score);
					highScore = score;
				}

				scoreTxt.text = "Your score = " + score.ToString();
				highScoreTxt.text = "Your highscore = " + highScore.ToString();
			}


		}

		currentGameState = newGameState; 
    }

    public void PauseMenu()
    {
        SetGameState(GameState.GS_PAUSEMENU);
    }
    public void GameOver() {
        SetGameState(GameState.GS_GAME_OVER);
    }
    public void InGame()
    {
        SetGameState(GameState.GS_GAME);
    }
    public void LevelCompleted()
    {
        SetGameState(GameState.GS_LEVELCOMPLETED);
    }
    public void Options()
    {
        SetGameState(GameState.GS_OPTIONS);
    }

    public void AddPoints(int points)
    {
        this.score += points;
        
        this.scoreText.text = score.ToString();
        for(int i = 0; i < 3 - score.ToString().Length; i++)
        {
            this.scoreText.text = '0' + scoreText.text;
        }
    }

    public void AddKeys(GameObject keyFound)
    {

        if (keyFound.name == "gemG")
        {
            //Debug.Log(keyFound.GetComponent<Renderer>().material.color);
            keysTab[0].color = Color.green;
        }
        else if(keyFound.name == "gemR")
        {
            keysTab[2].color = Color.red;
        }
        else
        {
            keysTab[1].color = keyFound.GetComponent<Renderer>().material.color;
		}
        keysFound += 1;
    }

    public void AddHearth()
    {
        if(lifes < 3)
        {
            heartsTab[lifes].color = Color.red;
            lifes++;
        }
        else
        {
            AddPoints(30);
        }
    }

    public void TakeHearth()
    {
        if(lifes > 0)
        {
            heartsTab[lifes - 1].color = Color.gray;
            lifes--;
        }
    }

	public void IncreaseEnemiesKilledCounter()
    {
        enemiesKilled++;

        this.enemiesKilledText.text = enemiesKilled.ToString();
        for (int i = 0; i < 3 - enemiesKilled.ToString().Length; i++)
        {
            this.enemiesKilledText.text = '0' + enemiesKilledText.text;
        }
    }

}
