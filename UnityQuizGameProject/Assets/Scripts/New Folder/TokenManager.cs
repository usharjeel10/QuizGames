using Izhguzin.GoogleIdentity;
using System.Threading.Tasks;
using UnityEngine;
namespace TokenStore.token
{
public class TokenManager : ITokenStorage
{
    public Task<string> LoadTokenAsync(string userId)
    {
        string jsonToken = PlayerPrefs.GetString(userId);
        return Task.FromResult(jsonToken);
    }

    public Task<bool> SaveTokenAsync(string userId, string jsonToken)
    {
        PlayerPrefs.SetString(userId, jsonToken);
        return Task.FromResult(true);
    }
}
}