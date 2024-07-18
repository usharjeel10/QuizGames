using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReportsPannel : MonoBehaviour
{
    [SerializeField] GameObject LevelReportBtn;
    [SerializeField] GameObject content;
    [SerializeField] loadQuiz levelManager;
    [SerializeField] EachLevelReportsPannel eachLevelReportsPannel;

    [SerializeField] List<LevelReportsBtn> allLevelReportsBtns = new List<LevelReportsBtn>();
    private void Awake()
    {
        SpawnLevelBtns();
        UpdateAllReportLevelBtns();
    }

    private void SpawnLevelBtns()
    {
        for(int i = 0; i < levelManager.GetLevelsCount(); i++)
        {
            GameObject newLevelReportBtn = Instantiate(LevelReportBtn, content.transform.position, Quaternion.identity, content.transform);

            allLevelReportsBtns.Add(newLevelReportBtn.GetComponent<LevelReportsBtn>());
        }
    }

    private void UpdateAllReportLevelBtns()
    {
        for(int i = 0; i < allLevelReportsBtns.Count; i++)
        {
            int levelNum = i + 1;

            allLevelReportsBtns[i].UpdateTitleText("Level " + levelNum + " reports");
           

            allLevelReportsBtns[i].UpdateReportBtnOnClick(() => {
                eachLevelReportsPannel.gameObject.SetActive(true);
                gameObject.SetActive(false);
                eachLevelReportsPannel.UpdatePannel(levelNum);
            });
        }
    }
}
