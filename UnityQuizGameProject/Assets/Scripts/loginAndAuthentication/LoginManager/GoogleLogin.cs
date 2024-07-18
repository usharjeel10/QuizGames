using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Izhguzin.GoogleIdentity;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using RequestFailedException = Izhguzin.GoogleIdentity.RequestFailedException;

public class GoogleLogin : MonoBehaviour
{
    private TokenResponse _googleTokenResponse;

    #region Singleton
    public static GoogleLogin instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    private async void Start()
    {
        await InitServicesAsync();
    }

    public async Task InitServicesAsync()
    {
        // Get credentials from environment variables
        string clientId = Environment.GetEnvironmentVariable("GoogleOAuthClientID");
        string clientSecret = Environment.GetEnvironmentVariable("GoogleOAuthClientSecret");

        // Ensure that the environment variables were retrieved successfully
        if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
        {
            Debug.LogError("The Google OAuth Client ID or Secret is not set in the environment variables.");
            return;
        }

        GoogleAuthOptions.Builder optionsBuilder = new();
        optionsBuilder.SetCredentials(clientId, clientSecret)
            .SetScopes(Scopes.OpenId, Scopes.Email, Scopes.Profile)
            .SetListeningTcpPorts(new[] { 5000, 5001, 5002, 5003, 5005 });

        try
        {
            await GoogleIdentityService.InitializeAsync(optionsBuilder.Build());
            await UnityServices.InitializeAsync();
        }
        catch (AuthorizationFailedException exception)
        {
            Debug.LogException(exception);
        }
        catch (ServicesInitializationException)
        {
            Debug.LogError("Critical error, unable to log in anonymously.");
            throw;
        }
    }

    public async Task SignInWithGoogleAsync()
    {
        try
        {
            _googleTokenResponse = await GoogleIdentityService.Instance.AuthorizeAsync();
            await AuthenticationService.Instance.SignInWithGoogleAsync(_googleTokenResponse.IdToken);
            Debug.Log("Google token: " + _googleTokenResponse.IdToken + "  Google Acces token: " + _googleTokenResponse.AccessToken);
        }
        // Google Identity Exception
        catch (AuthorizationFailedException exception)
        {
            Debug.LogException(exception);
        }
    }
}
