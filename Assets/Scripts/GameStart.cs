using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;
using System;

public class GameStart : MonoBehaviour
{
    const int width = 1920;
    const int height = 1080;

    Canvas startGameCanvas;
    Canvas mainCanvas;
    Canvas kownHowMuchCanvas;
    Canvas eliminateToLeCanvas;
    Canvas finshingToLeCanvas;
    Canvas shopCanvas;

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
        startGameCanvas = GameObject.Find(ConstLib.STARTGAMECANVAS).GetComponent<Canvas>();

        eliminateToLeCanvas = GameObject.Find(ConstLib.ELIMINATETOLECANVAS).GetComponent<Canvas>();
        eliminateToLeCanvas.enabled = false;

        kownHowMuchCanvas = GameObject.Find(ConstLib.KOWNHOWMUCHCANVAS).GetComponent<Canvas>();
        kownHowMuchCanvas.enabled = false;

        finshingToLeCanvas = GameObject.Find(ConstLib.FISHINGTOLECANVAS).GetComponent<Canvas>();
        finshingToLeCanvas.enabled = false;

        shopCanvas = GameObject.Find(ConstLib.SHOPCANVAS).GetComponent<Canvas>();
        shopCanvas.enabled = false;

        mainCanvas = GameObject.Find(ConstLib.MAINCANVAS).GetComponent<Canvas>();
        mainCanvas.enabled = false;

        manager = GameObject.Find(ConstLib.CANVASMANAGER).GetComponent<CanvasManager>();

        vedio = startGameCanvas.transform.Find(ConstLib.STARTVEDIO).GetComponent<VideoPlayer>();

        InitState();
        PlayerState.InitPropsNum();
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

    /// <summary>
    /// 加载以前游戏记录的分数
    /// </summary>
    void InitState()
    {
        string path = Application.streamingAssetsPath + ConstLib.INTEGRAL_FILE_PATH;
        ConstLib.Integral = Convert.ToInt32(File.ReadAllText(path));
        
    }
}
