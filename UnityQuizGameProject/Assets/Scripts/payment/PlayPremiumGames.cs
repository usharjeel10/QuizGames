using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayPremiumGames : MonoBehaviour
{
    public async void playGame(string GameIndex)
    {
        SubscriptionData data = await CloudSaveManager.instance.LoadData<SubscriptionData>("PremiumSubs");
        if (data == null) { return; }
        if (data.isSubscribed)
        {

            DateTime subscriptionStartDate = DateTime.Parse(data.subscriptionStartDate);
            Debug.Log(subscriptionStartDate.Date);
            int daysToAdd = 30;
            DateTime subscriptionEndDate = subscriptionStartDate.AddDays(daysToAdd);
            Debug.Log(subscriptionEndDate.Date);
            if (DateTime.Now.Date <= subscriptionEndDate.Date)
            {
                SceneManager.LoadSceneAsync(GameIndex);
            }
            else
            {
                CloudSaveManager.instance.SaveData<SubscriptionData>("PremiumSubs", new SubscriptionData { isSubscribed=false });
                Debug.Log("Subscription has expired. Please renew your subscription.");
            }
        }
    }


}
