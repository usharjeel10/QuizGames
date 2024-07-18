using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class LevelReportsBtn : MonoBehaviour
{
    [SerializeField] Text titleText;
    [SerializeField] Button reportsBtn;

    public void UpdateTitleText(string _text)
    {
        titleText.text = _text;
    }

    public void UpdateReportBtnOnClick(UnityAction clickEvent)
    {
        reportsBtn.onClick.RemoveAllListeners();
        reportsBtn.onClick.AddListener(clickEvent);
    }
}
