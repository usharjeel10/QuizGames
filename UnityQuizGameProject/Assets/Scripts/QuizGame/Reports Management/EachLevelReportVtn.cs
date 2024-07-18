using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EachLevelReportVtn : MonoBehaviour
{
    [SerializeField] private Text levelNumberText;
    [SerializeField] private Text percentageText;
    [SerializeField] private Text DateTimeText;
    [SerializeField] private Button printCertificateBtn;

    public void UpdateBtnTexts(string _level, string _percentage, string _dateTime, UnityAction buttonAction)
    {
        levelNumberText.text = _level;
        percentageText.text = "Percentage: " +_percentage;
        DateTimeText.text = _dateTime;
        int percentage = int.Parse(_percentage);
        Action<bool>setActiveCallBack= isActive =>{
            printCertificateBtn.gameObject.SetActive(isActive);
        };
        setActiveCallBack(percentage >= 80);
        printCertificateBtn.onClick.AddListener(buttonAction);
    }
}
