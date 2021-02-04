using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 用于管理UI界面切换的类
/// </summary>
public class CanvasManager : MonoBehaviour
{
    Canvas startGameCanvas;
    Canvas mainCanvas;
    Canvas kownHowMuchCanvas;
    Canvas eliminateToLeCanvas;
    Canvas fishingToLeCanvas;
    Canvas emigratedCanvas;

    //存储UI界面的栈
    Stack<Canvas> UIStack = new Stack<Canvas>();

    GameObject canvasManager;

    AnswerCanvasUI kownHowMuchUI;
    GameStart gameStart;

    void Start()
    {
        try
        {
            startGameCanvas = GameObject.Find("StartGameCanvas").GetComponent<Canvas>();
            startGameCanvas.enabled = true;

            mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
            mainCanvas.enabled = false;

            eliminateToLeCanvas = GameObject.Find("EliminateToLeCanvas").GetComponent<Canvas>();
            eliminateToLeCanvas.enabled = false;

            kownHowMuchCanvas = GameObject.Find("KownHowMuchCanvas").GetComponent<Canvas>();
            kownHowMuchCanvas.enabled = false;

            fishingToLeCanvas = GameObject.Find("FishingToLeCanvas").GetComponent<Canvas>();
            fishingToLeCanvas.enabled = false;

            emigratedCanvas = GameObject.Find("EmigratedCanvas").GetComponent<Canvas>();
            emigratedCanvas.enabled = false;

            canvasManager = GameObject.Find("CanvasManager");

            kownHowMuchUI = mainCanvas.transform.Find("BtnKownHowMuch").GetComponent<AnswerCanvasUI>();

            gameStart = startGameCanvas.transform.Find("BtnStartGame").GetComponent<GameStart>();

            Push(startGameCanvas);
        }
        catch (ArgumentNullException e)
        {
            Debug.LogError(e);
            throw (e);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pop();
        }      
    }

    /// <summary>
    /// UI入UI栈，显示栈顶UI，隐藏其他UI
    /// </summary>
    /// <param name="canvas"></param>
    public void Push(Canvas canvas)
    {
        //判断入栈UI是否为节点UI，若为节点UI则不隐藏当前UI
        if (UIStack.Count > 0 && canvas.GetComponent<Node>() == null)
        {
            UIStack.Peek().enabled = false;
        }

        UIStack.Push(canvas);
        UIStack.Peek().enabled = true;
    }

    /// <summary>
    /// UI出UI栈，出栈后显示栈顶UI
    /// </summary>
    public void Pop()
    { 
        if (UIStack.Count <= 0)
        {
            print(UIStack.Count);
            return;
        }

        if (UIStack.Count == 1 && UIStack.Peek() == startGameCanvas)
        {
            Application.Quit();
            return;
        }
        UIStack.Pop().enabled = false; ;
        UIStack.Peek().enabled = true;
    }

    public Canvas Peek()
    {
        return UIStack.Peek();
    }
}
