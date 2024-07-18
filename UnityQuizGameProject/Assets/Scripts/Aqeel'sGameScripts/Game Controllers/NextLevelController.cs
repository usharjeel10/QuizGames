using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelController : MonoBehaviour {

	public Text scoreText;
	private bool isMenuButtonClick = true;
	[SerializeField] private GameObject parent;
	private int score;
	void Start () {
		InitializeVariables ();
		if (GameController.instance != null && MusicController.instance != null) {
			if(GameController.instance.isMusicOn){
				MusicController.instance.StopAllSound ();
				MusicController.instance.PlaySuccessSound ();
			}
		} 
	}

	void InitializeVariables(){
		score = GameController.instance.currentScore;
		scoreText.text = score.ToString ();
	}

	public void NextButton(){
		SceneManager.LoadScene ("Gameplay1");
	}

	public void ExitButton(){
		BackToLevelSection();
	}
	public void BackToLevelSection()
	{

		if (isMenuButtonClick)
		{
			isMenuButtonClick = false;
			SceneManager.LoadSceneAsync("Main Menu");
		}
	}
}
