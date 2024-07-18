using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartController : MonoBehaviour {
	public Text scoreText;
	private int score;
	void Start () {
		InitializeVariables ();
		if(GameController.instance != null && MusicController.instance != null){
			if(GameController.instance.isMusicOn){
				MusicController.instance.StopAllSound ();
				MusicController.instance.PlayFailedSound ();
			}
		}
	}

	void InitializeVariables(){
		score = GameController.instance.currentScore;
		scoreText.text = score.ToString ();
	}

	public void RestartButton(){
		SceneManager.LoadScene ("Gameplay1");
	}

	public void ExitButton(){
		SceneManager.LoadScene ("Main Menu");
	}
}
