using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AttemptQuiz : MonoBehaviour
{
    [Header("Quiz-Answer")]
    [SerializeField] private int[] answers;
    bool canAttempt=true;
    bool isPlayBtnClick=true;
    [SerializeField] private GameObject[] questions;
    int counter, i;
    private bool isMenuButtonClick=true;
    [SerializeField] private GameObject[] pannel;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject _quizReport;
    [SerializeField] private Text TrueFalseText;
    [SerializeField] private UnityEvent correctAnswerSound;
    [SerializeField] private UnityEvent wrongAnswerSound;
    public void correctSkip()
    {
        i++;
        StartCoroutine(nextQuestion(i));
    }
    public void Attempt(int index)
    {
        if (!canAttempt) { return; }

        canAttempt = false;
        if (index == answers[i])
        {
            correctAnswerSound.Invoke();
            counter++;
            pannel[i].SetActive(true);
            TrueFalseText.text = "Correct";
            TrueFalseText.color = Color.green;
        }
        else
        {
            wrongAnswerSound.Invoke();
            TrueFalseText.text = "Wrong";
            TrueFalseText.color = Color.red;
        }
        i++;
        StartCoroutine(nextQuestion(i));
    }
    private IEnumerator nextQuestion(int i)
    {
        yield return new WaitForSeconds(1);
        TrueFalseText.text = "";
        canAttempt = true;
        if (i >= 5)
        {
            if (counter >= 2)
            {
                playGame();
            }
            else
            {
                questions[i - 1].SetActive(false);
                this.i = 0;
                i = 0;
                questions[i].SetActive(true);
            }
        }
        else
        {
            questions[i - 1].SetActive(false);
            questions[i].SetActive(true);
        }
    }

     async private void playGame()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
            await UnityServices.InitializeAsync();

        _quizReport.SetActive(true);
        QuizReport qr = FindObjectOfType<QuizReport>();
        qr?.Display(counter);
        int levelNumber = SceneManager.GetActiveScene().buildIndex-1;

        LevelReportsAmount levelDataAmountS = await CloudSaveManager.instance.LoadData<LevelReportsAmount>("Level" + levelNumber.ToString() + "Reports-Amount");

        int levelDataAmount;
        if (levelDataAmountS != null)
            levelDataAmount = levelDataAmountS.amount;
        else
            levelDataAmount = 0;

        Debug.Log("Level " + levelNumber + "reports amount: " + levelDataAmount);

        LevelData levelData = new LevelData
        {
            playerScore = counter,
            dateAndTime = DateTime.Now.ToString("yyyy-MM-dd")
    };

        levelDataAmount++;

        LevelReportsAmount newLevelReportsAmount = new LevelReportsAmount()
        {
            amount = levelDataAmount,
        };
        TotaLevels _totalLevel = new TotaLevels()
        {
            TotalLevelComplete = levelNumber
        };
        CloudSaveManager.instance.SaveData<LevelData>("Level" + levelNumber.ToString() + "-" + levelDataAmount.ToString() + "Data", levelData);
        CloudSaveManager.instance.SaveData<LevelReportsAmount>("Level" + levelNumber.ToString() + "Reports-Amount", newLevelReportsAmount);
        CloudSaveManager.instance.SaveData<TotaLevels>("Total-Level",_totalLevel);

        //int currentLevel = levelNumber;
        //LevelData _lvl = await CloudSaveManager.instance.LoadData<LevelData>("Level" + 0 + "Data");

        //Debug.Log("Level Completed " + levelNumber);
        //if (_lvl != null)
        //{
        //    if (_lvl.TotalQuizPass > levelNumber)
        //    {
        //        levelNumber = _lvl.TotalQuizPass;
        //    }
        //}
        //LevelData levelData = new LevelData
        //{
        //    levelindex =+1,
        //    dateAndTime = DateTime.Now.ToString(),
        //    playerScore = counter,
        //    TotalQuizPass = levelNumber
        //};
        //levelNumber = currentLevel;

        //CloudSaveManager.instance.SaveData<LevelData>("Level " + levelNumber + "." +  "Data", levelData);
        //CloudSaveManager.instance.SaveData<LevelData>("Level" + 0 + "Data", levelData);
    }
    public void StartGame(int GameIndex)
    {
        if (isPlayBtnClick)
        {
            isPlayBtnClick = false;
            SceneManager.LoadSceneAsync(GameIndex);
        }
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
