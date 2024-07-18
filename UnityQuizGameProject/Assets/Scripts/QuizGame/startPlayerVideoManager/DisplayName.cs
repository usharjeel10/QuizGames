using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayName : MonoBehaviour
{
    [SerializeField] private Text playerName;
   private async void  Start()
    {
        Player _player = await CloudSaveManager.instance.LoadData<Player>("Player-info");
        if (_player != null)
        {
            playerName.text = "Player : " + _player.playerName.ToString();
        }
    }
}
