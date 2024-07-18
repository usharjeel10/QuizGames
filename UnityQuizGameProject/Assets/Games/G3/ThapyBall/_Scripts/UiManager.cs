using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour {

    public GameObject gameOverPanel;
    public GameObject startUi;
    public GameObject gameOverText;
    public static UiManager instance;
    public Text scoreText;
    public Text highScoreText;

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = ThayBallScoreManager.instance.score.ToString();
	}

    public void GameStart()
    {
        startUi.SetActive(false);
    }

    public void GameOver()
    {
       // gameOverText.SetActive(true);
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        gameOverPanel.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene("Gameplay3");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Game3 Main Menu");
    }
}
