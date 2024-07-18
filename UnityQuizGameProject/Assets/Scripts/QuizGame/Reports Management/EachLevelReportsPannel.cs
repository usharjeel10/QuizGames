using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EachLevelReportsPannel : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] GameObject EachLevelReportBtn;

    [Header("Certificate related stuff")]
    [SerializeField] private certificate Certificate;
    [SerializeField] private string[] QuizTopic;
    public async void UpdatePannel(int _levelNumber)
    {
        //Destroy all previous reports
        for (int i = 0; i < content.childCount; i++)
        {
            Transform child = content.GetChild(i);
            Destroy(child.gameObject);
            //Destroy(child);
            Debug.Log("Pannel updated  "+i+content.GetChild(i).name);
        }

        //Spawn new reports Btns
        LevelReportsAmount Reports =  await CloudSaveManager.instance.LoadData<LevelReportsAmount>("Level" + _levelNumber.ToString() + "Reports-Amount");
        Debug.Log("Level number: " + _levelNumber);
        int NoOfReports=0;
        if (Reports != null)
        {
            NoOfReports = Reports.amount;
        }
        else
        {
            return;
        }

        for (int i = NoOfReports; i >= 1; i--)
        {
            GameObject newLevelReportBtn = Instantiate(EachLevelReportBtn, content.transform.position, Quaternion.identity, content.transform);

            LevelData levelData = await CloudSaveManager.instance.LoadData<LevelData>("Level" + _levelNumber.ToString() + "-" + i.ToString() + "Data");

            float percentage = levelData.playerScore;
            percentage = (percentage / 5) * 100;
            string dateTime = levelData.dateAndTime;

            newLevelReportBtn.GetComponent<EachLevelReportVtn>().UpdateBtnTexts("Level " + _levelNumber,
                percentage.ToString(),
                "Date: " + dateTime,
                ()=>
                {
                    if (percentage >= 80)
                    {
                        Certificate.ShowCertificate(QuizTopic[_levelNumber -1], percentage, dateTime);
                        //gameObject.SetActive(false);
                    }

                });


        }
    }
}
