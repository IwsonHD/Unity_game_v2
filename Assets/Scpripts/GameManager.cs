using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEditor.Build;
using UnityEditor.ShaderGraph;

public class GameManager : MonoBehaviour
{
    public enum GameState {
        GS_PAUSEMENU,
        GS_GAME,
        GS_LEVELCOMPLETED,
        GS_GAME_OVER
    }

    public static GameManager instance;

    public Canvas inGameCanvas; 

    public GameState currentGameState;

    public TMP_Text scoreText;

    public TMP_Text currentTime;

    public TMP_Text enemiesKilledText;

    private int score = 0;

    private float timer = 0.0f;

    private int keysFound = 0;

    private int enemiesKilled = 0;

    private int lifes = 3;

    public Image[] keysTab;

    public Image[] heartsTab;

    

	private void Awake()
	{
		instance = this;
        this.currentGameState = GameState.GS_GAME;
        this.scoreText.text = score.ToString();
		currentTime.text = string.Format("{0:00}:{1:00}", Math.Floor(timer / 60f), timer % 60f);
		this.enemiesKilledText.text = enemiesKilled.ToString();
        
        


        for(int i = 0; i < keysTab.Length; i++)
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
        
    }

    // Update is called once per frame
    void Update()
    {

       

        currentTime.text = string.Format("{0:00}: {1:00}", Math.Floor(timer / 60f), timer % 60f);

        if(currentGameState == GameState.GS_GAME)   timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentGameState == GameState.GS_PAUSEMENU)
            {
                InGame();
            }
            else if(currentGameState == GameState.GS_GAME)
            {
                PauseMenu();
            }

        }
        
    }

    void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.GS_GAME)
        {
            inGameCanvas.enabled = true;
        }
        else
        {
            inGameCanvas.enabled = false;   
        }

        currentGameState= newGameState; 
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
