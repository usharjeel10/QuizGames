using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetCheckout;
using UnityEngine.UI;
using System;
using Unity.Services.CloudSave;
public class SubscriptionPannel : MonoBehaviour
{
    [Header("Premium Package")]
    [SerializeField] Button purchaseBtn;
    [SerializeField] Button deActivateBtn;
    [SerializeField] GameObject purchasedTextObject;

    #region private vars
    private Checkout checkout;
    private StripeClient client;

    private string order_id;
    private bool isSubscribed;
    private bool isActivated;
    #endregion

    private async void Awake()
    {
        //Cloud code come here
        SubscriptionData data = await CloudSaveManager.instance.LoadData<SubscriptionData>("PremiumSubs");
        if (data == null)
        {
            isSubscribed = false;
            order_id = null;
            isActivated = false;
        }
        else
        {
            isSubscribed = data.isSubscribed;
            order_id = data.order_id;
            isActivated = data.isActivated;
        }

        //Other code
        client = new StripeClient();
        checkout = new Checkout(client);

        if (!isSubscribed)
        {
            purchaseBtn.GetComponentInChildren<Text>().text = "Subscribe";
            purchaseBtn.onClick.RemoveAllListeners();
            purchaseBtn.onClick.AddListener(OnSubscriptionBtn);
        }
        else
        {
            purchaseBtn.GetComponentInChildren<Text>().text = "Subscribe";
            purchaseBtn.onClick.RemoveAllListeners();
            purchaseBtn.onClick.AddListener(ActivateSubscription);

            if (isActivated)
                SetSubscribeObjectsActive(false, true, true);
            else
                SetSubscribeObjectsActive(true, false, false);
        }
        deActivateBtn.onClick.AddListener(DeactivateSubscription);

    }

    public void checkSub()
    {
        checkout.GetSubscriptionData(order_id, (bool success,object data) => 
        {
            if (!success)
            {
                Debug.Log("error");
            }
            else
            {
              //  Debug.Log(data);
            }
        });
    }



    private void OnSubscriptionBtn()
    {
        checkout.Subscribe("Premium", "99", PaymentPeriod.month, 1, (bool succesful, object data) => {
            if (!succesful)
                Debug.LogWarning("warning: " + data.ToString());
            else
            {
                string today = DateTime.Now.Date.ToString();
                CloudSaveManager.instance.SaveData<SubscriptionData>("PremiumSubs", new SubscriptionData
                {
                    isActivated = true,
                    isSubscribed = true,
                    
                    subscriptionStartDate=today,
                    
                    order_id = data.ToString()
                });
                purchaseBtn.GetComponentInChildren<Text>().text = "Subscribe";
                purchaseBtn.onClick.RemoveAllListeners();
                purchaseBtn.onClick.AddListener(ActivateSubscription);

                SetSubscribeObjectsActive(false, true, true);
            }
        });
    }

    private void ActivateSubscription()
    {
        checkout.ActivateSubscription(order_id, (bool succesful, object data) => {
            if (!succesful)
                Debug.LogWarning("Warning: " + data.ToString());
            else
            {
                CloudSaveManager.instance.SaveData<SubscriptionData>("PremiumSubs", new SubscriptionData
                {
                    isActivated = true,
                    isSubscribed = true,
                    order_id = order_id
                });
                SetSubscribeObjectsActive(false, true, true);
            }
        });
    }
    private void DeactivateSubscription()
    {
        checkout.DeactivateSubscription(order_id, (bool succesful, object data) => {
            if (!succesful)
                Debug.LogWarning("warning: " + data.ToString());
            else
            {
                CloudSaveManager.instance.SaveData<SubscriptionData>("premiumsubs", new SubscriptionData
                {
                    isActivated = false,
                    isSubscribed = true,
                    order_id = order_id
                });

                SetSubscribeObjectsActive(true, false, false);
            }
        });

        
    }

    private void SetSubscribeObjectsActive(bool _purchaseBtn, bool _deActivateBtn, bool _purchasedTextObject)
    {
        purchaseBtn.gameObject.SetActive(_purchaseBtn);
        deActivateBtn.gameObject.SetActive(_deActivateBtn);
        purchasedTextObject.SetActive(_purchasedTextObject);
    }

}
