using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.PlayerAccounts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LogoutBtn : MonoBehaviour
{
    private async void Awake()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
            await UnityServices.InitializeAsync();

        GetComponent<Button>().onClick.AddListener(() =>
        {
            PlayerAccountService.Instance.SignOut();
            AuthenticationService.Instance.SignOut();
            PlayerPrefs.DeleteAll();

            SceneManager.LoadSceneAsync(0);
        });
    }
}
