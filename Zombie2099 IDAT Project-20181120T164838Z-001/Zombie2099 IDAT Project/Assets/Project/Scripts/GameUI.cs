using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

    public GameObject gameWinUI;
    public GameObject infectedUI;
    public GameObject spottedUI;
    public GameObject HUD;
    bool gameIsOver;

	// Use this for initialization
	void Start () {
        Guard.OnGuardHasSpottedPlayer += ShowSpottedUI;
        FindObjectOfType<Player>().OnReachedEndOfLevel += ShowGameWinUI;
        FindObjectOfType<PlayerHealth>().OnVirusTakenOver += ShowInfectedUI;
    }
	
	// Update is called once per frame
	void Update () {
		if(gameIsOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
	}

    void ShowGameWinUI()
    {
        OnGameOver(gameWinUI);
    }

    void ShowInfectedUI()
    {
        OnGameOver(infectedUI);
    }

    void ShowSpottedUI()
    {
        OnGameOver(spottedUI);
    }

    void OnGameOver(GameObject gameOverUI)
    {
        HUD.SetActive(false);
        gameOverUI.SetActive(true);
        gameIsOver = true;
        Guard.OnGuardHasSpottedPlayer -= ShowSpottedUI;
        FindObjectOfType<Player>().OnReachedEndOfLevel -= ShowGameWinUI;
        FindObjectOfType<PlayerHealth>().OnVirusTakenOver -= ShowInfectedUI;
    }
}
