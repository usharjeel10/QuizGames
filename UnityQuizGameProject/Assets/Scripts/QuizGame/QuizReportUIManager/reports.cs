using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reports : MonoBehaviour
{
    [SerializeField] private Button _backBtn;
    [SerializeField] private GameObject totalQuiz;
    [SerializeField] private GameObject EachQuizLevel;
    [SerializeField] private GameObject parent;
    private void Start()
    {
        _backBtn.onClick.AddListener(() =>
        {
            if (totalQuiz.activeInHierarchy)
            {
                parent.SetActive(false);
            }
            if (EachQuizLevel.activeInHierarchy)
            {
                EachQuizLevel.SetActive(false);
                totalQuiz.SetActive(true);
            }
        });
    }
}