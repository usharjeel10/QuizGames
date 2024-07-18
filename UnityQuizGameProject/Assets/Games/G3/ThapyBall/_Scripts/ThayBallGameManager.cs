using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThayBallGameManager : MonoBehaviour {

    public static ThayBallGameManager instance;
    bool gameOver;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // stops it from being destroyed when changing from one scene to another

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject); // we only want 1 game manager. Loading scenes creates new game managers. If instance exists, destroy game manager
                                      // created with it
    }

	// Use this for initialization
	void Start () {
        gameOver = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameStart()
    {
        UiManager.instance.GameStart();
        GameObject.Find("PipeSpawner").GetComponent<PipeSpawner>().StartSpawningPipes();
    }

    public void GameOver()
    {
        gameOver = false;
        GameObject.Find("PipeSpawner").GetComponent<PipeSpawner>().StopSpawningPipes();

        ThayBallScoreManager.instance.SaveScore(); // save score and highscore if needed
        UiManager.instance.GameOver(); // bring up game over panel and show highscore
    }
}
