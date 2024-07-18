using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizReport : MonoBehaviour
{
    [SerializeField] private Text _displayTotal;
    [SerializeField] private Text _displayCorrect;
    [SerializeField] private Text _displayWrong;
    [SerializeField] private Text _displayPercentage;
    void Start()
    {

    }
    public void Display(int correct)
    {
        
        _displayTotal.text = "Total Mcqs: 5";
        _displayCorrect.text = "Correct Mcqs : " + correct;
        _displayWrong.text = "Wrong Mcqs : " + (5-correct);
        float newCorrect = correct;
        _displayPercentage.text = "Total Percentage: " + (newCorrect / 5)*100;

    }
}
