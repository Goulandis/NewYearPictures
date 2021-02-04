using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

public class GameStart : MonoBehaviour
{
    const int width = 1920;
    const int height = 1080;

    Canvas startGameCanvas;
    Canvas mainCanvas;
    Canvas kownHowMuchCanvas;
    Canvas eliminateToLeCanvas;

    /// <summary>
    /// UI管理
    /// </summary>
    CanvasManager manager;

    /// <summary>
    /// 视频文件
    /// </summary>
    VideoPlayer vedio;

    void Start()
    {
        startGameCanvas = GameObject.Find("StartGameCanvas").GetComponent<Canvas>();

        eliminateToLeCanvas = GameObject.Find("EliminateToLeCanvas").GetComponent<Canvas>();
        eliminateToLeCanvas.enabled = false;

        kownHowMuchCanvas = GameObject.Find("KownHowMuchCanvas").GetComponent<Canvas>();
        kownHowMuchCanvas.enabled = false;

        mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();

        manager = GameObject.Find("CanvasManager").GetComponent<CanvasManager>();

        vedio = startGameCanvas.transform.Find("StartVedio").GetComponent<VideoPlayer>();
    }

    void Update()
    {
        if (!startGameCanvas.enabled && vedio.isPlaying)
        {
            vedio.Pause();
            vedio.enabled = false;        
        }
        if (startGameCanvas.enabled && !vedio.isPlaying)
        {
            vedio.enabled = true;
            vedio.Play();
            mainCanvas.GetComponent<Animator>().SetBool("Player", false);
            mainCanvas.GetComponent<Animator>().Play("Idel");
        }
    }

    public void Click()
    {
        manager.Push(mainCanvas);
        mainCanvas.GetComponent<Animator>().SetBool("Player", true);
    }
}
