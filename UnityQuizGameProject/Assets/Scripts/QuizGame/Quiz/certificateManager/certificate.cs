using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class certificate : MonoBehaviour
{
    [SerializeField] private Text QuizName;
    [SerializeField] private Text PlayerData;
    [SerializeField] private Image certificateImg;
    public async void ShowCertificate(string Qname, float perc, string dateTime)
    {
        Debug.Log("Call certificate");
        gameObject.SetActive(true);

        Player _player = await CloudSaveManager.instance.LoadData<Player>("Player-info");
        QuizName.text = Qname;
        PlayerData.text = _player.playerName+" score "+ perc + " %  on "+dateTime;
    }
    public void SaveToMemory()
    {
        Texture2D texture = certificateImg.sprite.texture;
        byte[] imageData = texture.EncodeToPNG();

        string filePath = System.IO.Path.Combine(Application.persistentDataPath, "image 6.png");
        System.IO.File.WriteAllBytes(filePath, imageData);
    }
}
