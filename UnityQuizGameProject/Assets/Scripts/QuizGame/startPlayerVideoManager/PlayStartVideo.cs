using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayStartVideo : MonoBehaviour
{

    [SerializeField] private string videoName;
    private VideoPlayer video;
    void Start()=>playeVideo();
    private void playeVideo()
    {
        GameObject cam = GameObject.Find("Main Camera");
        video = cam.AddComponent<VideoPlayer>(); ;
        string videoUrl = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);
        video.url = videoUrl;
        video.Play();
    }
}