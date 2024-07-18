using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Networking;
using Unity.Services.Authentication;
using System;
using Unity.Services.Core;
using Unity.Services.PlayerAccounts;

public class MainMenuUI : MonoBehaviour
{
    [Header("Signin-Pannel")]
    [SerializeField] private GameObject signinPannel;
    [SerializeField] private GameObject LauncherPannel;
    [SerializeField] private Button signinBtn;

    [Header("Loading-Pannel")]
    [SerializeField] GameObject loadingPannel;
    [SerializeField] Text loadingText;
    [SerializeField] Button loadingPannelBackBtn;

    [Header("Setname-Pannel")]
    [SerializeField] GameObject setnamePannel;
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] Button setNameDoneButton;
    
    [Header("Player-Info")]
    public string _pname;
    public static MainMenuUI instance;

    [Header("Wifi-Setup")]
    [SerializeField] private Text wifiConnection;
    private bool wifiActive = false;

    [Header("Sound-setup")]
    [SerializeField] private UnityEvent loginBtn;
    //[SerializeField] private Button startBtn;

    private async void Awake()
    {
        instance = this;
        if (UnityServices.State != ServicesInitializationState.Initialized)
            await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += async () => {
            Player player = await CloudSaveManager.instance.LoadData<Player>("Player-info");

            if (player != null)
            {
                // TurnOffAllPannels();
                //EnableStartBtn();
                Debug.Log("Player not null");
                //StartCoroutine(EnableMainMenuPannel());
                EnableMainMenuPannel();
            }
            else
            {
                Debug.Log("Player is null");
                // StartCoroutine(EnableSetNamePannel());
                EnableSetNamePannel();
            }
        };
    }

    private void EnableSetNamePannel()
    {
       // yield return new WaitForSeconds(7);
        TurnOffAllPannels();
        setnamePannel.SetActive(true);
    }

    void Start()
    {
        //StartCoroutine(CheckInternetConnection());
       // TurnOffAllPannels();
        signinBtn.onClick.AddListener(async () => {
            //if (wifiActive)
            //{
            loginBtn.Invoke();
                if (AuthenticationService.Instance.IsSignedIn || PlayerAccountService.Instance.IsSignedIn)
                    LoginManager.instance.SignOut();
                EnableLoadingPannel();
                await GoogleLogin.instance.SignInWithGoogleAsync();
            //}
            //else
            //{
            //    DisplayWifi();
            //}
        });
        setNameDoneButton.onClick.AddListener(() => {

            if (playerNameInputField.text.Length <= 0)
            {
                return;
            }

            Player _player = new Player()
            {
                playerName = playerNameInputField.text,
            };
            _pname = _player.playerName;

            CloudSaveManager.instance.SaveData("Player-info", _player);

            // StartCoroutine(EnableMainMenuPannel());
            EnableMainMenuPannel();
        });
    }
    #region signoutStuff
    /* private IEnumerator ContinueSignin()
     {
         yield return new WaitForSeconds(1);
         UnityEngine.SceneManagement.SceneManager.LoadScene("FirstScene");
         // LoginManager.instance.StartSignInAsync();
     }*/

    /* private void EnableStartBtn()
     {
         startBtn.gameObject.SetActive(true);
     }*/
    #endregion
    //private void DisplayWifi()
    //{
    //    wifiConnection.gameObject.SetActive(true);
    //}
    //private IEnumerator CheckInternetConnection()
    //{
    //    UnityWebRequest www = new UnityWebRequest("http://www.google.com");
    //    yield return www.SendWebRequest();

    //    if (www.isNetworkError || www.isHttpError)
    //    {
    //        wifiActive = false;
    //    }
    //    else
    //    {
    //        wifiActive = true;
    //    }
    //}
    private void TurnOffAllPannels()
    {
        loadingPannel.SetActive(false);
        setnamePannel.SetActive(false);
    }
    public void EnableLoadingPannel()
    {
        //TurnOffAllPannels();
        //loadingPannel.SetActive(true);
        //loadingText.text = "Loading...";
        //loadingPannelBackBtn.gameObject.SetActive(false);
    }
    public void EnableSigninPannel()
    {
        TurnOffAllPannels();
        signinPannel.SetActive(true);
    }
    private void EnableMainMenuPannel()
    {
       // yield return new WaitForSeconds(7);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Main Menu");
    }
    public void DisplayError(string error, UnityAction BackBtnAction)
    {
        //loadingPannelBackBtn.onClick.RemoveAllListeners();
        //loadingPannelBackBtn.onClick.AddListener(BackBtnAction);
        //loadingPannelBackBtn.gameObject.SetActive(true);

        //loadingText.text = error;
    }
}
