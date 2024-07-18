using UnityEngine;
using UnityEngine.UI;
public class GameplayController : MonoBehaviour {
	public static GameplayController instance;

	public int score;
	public Text scoreText;
	public GameObject notification;
	void Awake(){
		CreateInstance ();
	}
    private void Update()
    {
		UpdateGameplayController();
	}
    void Start () {
		if(GameController.instance != null && MusicController.instance != null){
			if (GameController.instance.isMusicOn) {
				MusicController.instance.PlayGameplaySound ();
			} else {
				MusicController.instance.StopAllSound ();
			}
		}
		InitialGameplayVariables ();
	}
	void CreateInstance(){
		if(instance == null){
			instance = this;
		}
	}
	void UpdateGameplayController(){
		GameController.instance.currentScore = score;
		scoreText.text = score.ToString ();
	}
	void InitialGameplayVariables(){
		GameController.instance.currentScore = 0;
		score = GameController.instance.currentScore;
		scoreText.text = score.ToString ();
	}
}