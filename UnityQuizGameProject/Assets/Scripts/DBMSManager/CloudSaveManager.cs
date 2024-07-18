using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;

public class Player
{
    public string playerName;
    //public int PlayerScore;
    //public string DateAndTime;
}

public class LevelReportsAmount
{
    public int amount;
}
public class TotaLevels
{
    public int TotalLevelComplete;
}

public class LevelData
{
    //public int levelindex;
    public int playerScore;
    //public int TotalQuizPass;
    public string dateAndTime;
}

public class SubscriptionData
{
    public string subscriptionStartDate;
    public bool isSubscribed;
    public bool isActivated;
    public string order_id;
}

public class CloudSaveManager : MonoBehaviour
{
    public static CloudSaveManager instance;
   // public int GetPlayerScore;
   // public string GetDateAndTime;

    private async void Awake()
    {
        instance = this;

        if (UnityServices.State != ServicesInitializationState.Initialized)
            await UnityServices.InitializeAsync();

    }
    //public async void StoreDate_Score(int _score, int levelNumber)
    //{
    //    DateTime currentDateAndTime = DateTime.Now;
    //    LevelData levelData = new LevelData()
    //    {
    //        dateAndTime = currentDateAndTime.ToString(),
    //        playerScore = _score
    //    };

    //    await ForceSaveObjectData("Level" + levelNumber + "Data", levelData);
       
    //    //GetDateAndTime = _player.DateAndTime;
    //}
    public async void SaveData<T>(string Key, T data)
    {
        await ForceSaveObjectData<T>(Key, data);
    }
    public async void SaveSingleData(string Key, string data)
    {
        await ForceSaveSingleData(Key, data);
    }
    public async Task<T> LoadData<T>(string key)
    {
        T data = await RetrieveSpecificData<T>(key);
        return (T)(object) data;
    }
     
    #region For string value
    private async Task ForceSaveSingleData(string key, string value)
    {
        try
        {
            Dictionary<string, object> oneElement = new Dictionary<string, object>();

            // It's a text input field, but let's see if you actually entered a number.
            if (Int32.TryParse(value, out int wholeNumber))
            {
                oneElement.Add(key, wholeNumber);
            }
            else if (Single.TryParse(value, out float fractionalNumber))
            {
                oneElement.Add(key, fractionalNumber);
            }
            else
            {
                oneElement.Add(key, value);
            }

            await CloudSaveService.Instance.Data.ForceSaveAsync(oneElement);

            Debug.Log($"Successfully saved {key}:{value}");
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }
    }
    #endregion

    #region For Object value
    private async Task ForceSaveObjectData<T>(string key, T value)
    {
        try
        {
            // Although we are only saving a single value here, you can save multiple keys
            // and values in a single batch.
            Dictionary<string, object> oneElement = new Dictionary<string, object>
                {
                    { key, value }
                };

            await CloudSaveService.Instance.Data.ForceSaveAsync(oneElement);

            Debug.Log($"Successfully saved {key}:{value}");
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }
    }
    #endregion
    private async Task<T> RetrieveSpecificData<T>(string key)
    {
        Debug.Log(key);
        try
        {
            var results = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { key });

            if (results.TryGetValue(key, out string value))
            {
                return JsonUtility.FromJson<T>(value);
                Debug.Log(results);
            }
            else
            {
                Debug.Log($"There is no such key as {key}!");
            }
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }

        return default;
    }

    //private async Task RetrieveEverything()
    //{
    //    try
    //    {
    //        // If you wish to load only a subset of keys rather than everything, you
    //        // can call a method LoadAsync and pass a HashSet of keys into it.
    //        var results = await CloudSaveService.Instance.Data.LoadAllAsync();

    //        Debug.Log($"Elements loaded!");

    //        foreach (var element in results)
    //        {
    //            Debug.Log($"Key: {element.Key}, Value: {element.Value}");
    //        }
    //    }
    //    catch (CloudSaveValidationException e)
    //    {
    //        Debug.LogError(e);
    //    }
    //    catch (CloudSaveRateLimitedException e)
    //    {
    //        Debug.LogError(e);
    //    }
    //    catch (CloudSaveException e)
    //    {
    //        Debug.LogError(e);
    //    }
    //}

    //private async Task ForceDeleteSpecificData(string key)
    //{
    //    try
    //    {
    //        await CloudSaveService.Instance.Data.ForceDeleteAsync(key);

    //        Debug.Log($"Successfully deleted {key}");
    //    }
    //    catch (CloudSaveValidationException e)
    //    {
    //        Debug.LogError(e);
    //    }
    //    catch (CloudSaveRateLimitedException e)
    //    {
    //        Debug.LogError(e);
    //    }
    //    catch (CloudSaveException e)
    //    {
    //        Debug.LogError(e);
    //    }
    //}
}
