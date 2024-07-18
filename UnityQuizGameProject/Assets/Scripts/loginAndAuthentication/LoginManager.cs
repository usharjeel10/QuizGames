using System;
using Unity.Services.Core;
using UnityEngine;
using System.Collections;
//using Unity.Services.PlayerAccounts;
using Unity.Services.Authentication;

class LoginManager : MonoBehaviour
{
    public static LoginManager instance;
    [SerializeField] MainMenuUI mainMenuUI;
    [SerializeField] GoogleLogin googleLogin;
    //async void Awake()
    //{
        //instance = this;
        //if (UnityServices.State != ServicesInitializationState.Initialized)
        //    await googleLogin.InitServicesAsync();

        //PlayerAccountSettings.Instance.clientId = "3beaf813-2ddb-40e9-8e4f-f6dad8a23c91";
    //}
    //private IEnumerator Start()
    //{
    //    yield return new WaitForSeconds(6);
    //    string authToken = PlayerPrefs.GetString("AuthToken");
    //    if (!string.IsNullOrEmpty(authToken))
    //    {

    //        SignInWithToken(authToken);
    //    }
    //        PlayerAccountService.Instance.SignedIn += () => {
    //        Debug.Log("Signedin with Player acc service");
    //        SignInWithUnity();
    //        string newAuthToken = PlayerAccountService.Instance.AccessToken;
    //        PlayerPrefs.SetString("AuthToken", newAuthToken);
    //    };

    //}
    //public async void StartSignInAsync()
    //{
        //await PlayerAccountService.Instance.StartSignInAsync();
   // }
    public  void SignOut()
    {
       // PlayerAccountService.Instance.SignOut();
        AuthenticationService.Instance.SignOut();

        PlayerPrefs.DeleteAll();
    }
    //async void SignInWithUnity()
    //{
    //    try
    //    {
    //        await AuthenticationService.Instance.SignInWithUnityAsync(PlayerAccountService.Instance.AccessToken);
    //        Debug.Log("SignIn is successful.");
    //    }
    //    catch (AuthenticationException ex)
    //    {
    //        Debug.LogException(ex);
    //        mainMenuUI.EnableLoadingPannel();
    //        mainMenuUI.DisplayError(ex.Message, () => {
    //        mainMenuUI.EnableSigninPannel();
    //        });
    //    }
    //    catch (RequestFailedException ex)
    //    {
    //        Debug.LogException(ex);
    //        mainMenuUI.EnableLoadingPannel();
    //        mainMenuUI.DisplayError(ex.Message, () => {
    //        mainMenuUI.EnableSigninPannel();
    //        });
    //    }
    //}

    //async void SignInWithToken(string authToken)
    //{
    //    try
    //    {
    //        Debug.Log(authToken);
    //        await AuthenticationService.Instance.SignInWithUnityAsync(authToken);
    //        Debug.Log("SignIn with token is successful.");
    //    }
    //    catch (AuthenticationException ex)
    //    {
    //        Debug.LogException(ex);
    //        mainMenuUI.EnableLoadingPannel();
    //        mainMenuUI.DisplayError(ex.Message, () =>
    //        {
    //            mainMenuUI.EnableSigninPannel();
    //        });
    //    }
    //    catch (RequestFailedException ex)
    //    {
    //        Debug.LogException(ex);
    //        mainMenuUI.EnableLoadingPannel();
    //        mainMenuUI.DisplayError(ex.Message, () =>
    //        {
    //            mainMenuUI.EnableSigninPannel();
    //        });
    //    }
    //}

}