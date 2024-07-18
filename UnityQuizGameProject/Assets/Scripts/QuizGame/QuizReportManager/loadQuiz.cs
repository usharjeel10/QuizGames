using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class loadQuiz : MonoBehaviour,IlevelUnlock
{

    private bool isSceneLoad=true;
    [SerializeField] Button[] btn;
    int num = 0;
    private async void Start()
    {
        TotaLevels data = await CloudSaveManager.instance.LoadData<TotaLevels>("Total-Level");
        SubscriptionData subData = await CloudSaveManager.instance.LoadData<SubscriptionData>("PremiumSubs");
        if (subData != null)
        {
            if (subData.isSubscribed)
            {
                BuySubscription();
            }
            else
            {
                DidnotBuySubscription(data);
            }
        }
        else
        {
            DidnotBuySubscription(data);
        }
    }

    private void BuySubscription()
    {
        num = 4;
        for (int i = 0; i < num; i++)
        {
            btn[i].interactable = true;
        }
    }

    private void DidnotBuySubscription(TotaLevels data)
    {
        num = 0;
        if (data != null)
        {
            num = data.TotalLevelComplete;
            num += 1;
        }
        else
        {
            num = 1;
        }
        if (num >= 4)
        {
            num = 4;
        }
        for (int i = 0; i < num; i++)
        {
            btn[i].interactable = true;
        }
    }

    public void playQuiz(int sceneIndex)
    {
        if (isSceneLoad)
        {
            isSceneLoad = false;
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public int GetLevelsCount()
    {
        return btn.Length;
    }
}