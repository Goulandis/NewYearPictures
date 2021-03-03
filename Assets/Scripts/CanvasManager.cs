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
    Canvas stateCanvas;
    Canvas stateCanvasClone;


    //存储UI界面的栈
    Stack<Canvas> UIStack = new Stack<Canvas>();

    GameObject canvasManager;

    AnswerCanvasUI kownHowMuchUI;
    GameStart gameStart;

    void Start()
    {
        try
        {
            startGameCanvas = GameObject.Find(ConstLib.STARTGAMECANVAS).GetComponent<Canvas>();
            startGameCanvas.enabled = true;

            mainCanvas = GameObject.Find(ConstLib.MAINCANVAS).GetComponent<Canvas>();
            mainCanvas.enabled = false;

            eliminateToLeCanvas = GameObject.Find(ConstLib.ELIMINATETOLECANVAS).GetComponent<Canvas>();
            eliminateToLeCanvas.enabled = false;

            kownHowMuchCanvas = GameObject.Find(ConstLib.KOWNHOWMUCHCANVAS).GetComponent<Canvas>();
            kownHowMuchCanvas.enabled = false;

            fishingToLeCanvas = GameObject.Find(ConstLib.FISHINGTOLECANVAS).GetComponent<Canvas>();
            fishingToLeCanvas.enabled = false;

            emigratedCanvas = GameObject.Find(ConstLib.ABOUTCANVAS).GetComponent<Canvas>();
            emigratedCanvas.enabled = false;

            canvasManager = GameObject.Find(ConstLib.CANVASMANAGER);

            kownHowMuchUI = mainCanvas.transform.Find(ConstLib.BTNKOWNHOWMUCH).GetComponent<AnswerCanvasUI>();

            gameStart = startGameCanvas.transform.Find(ConstLib.BTNSTARTGAME).GetComponent<GameStart>();

            Push(startGameCanvas);

            stateCanvas = Resources.Load<Canvas>(ConstLib.PREFAB_STATECANVAS);
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (stateCanvasClone == null)
            {               
                stateCanvasClone = Instantiate(stateCanvas);
                stateCanvasClone.GetComponent<StateCanvasScript>().InitState();
            }           
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (stateCanvasClone != null)
            {
                Destroy(stateCanvasClone.gameObject);
            }           
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (PlayerState.prop.BoomNum > 0)
            {
                if (PlayerState.usingBoom == false)
                {
                    PlayerState.usingBoom = true;
                    ToolLib.Tig(UIStack.Peek().transform, "炸弹已激活");
                }
            }
            else
            {
                ToolLib.Tig(UIStack.Peek().transform, "炸弹已用完");
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Canvas canvas = UIStack.Peek();
            if (ConstLib.GAMECANVAS.Contains(canvas.name))
            {
                PlayerState.usingTimeAdd = true;
            }            
            if (PlayerState.usingTimeAdd == true)
            {
                if (canvas.transform.Find(ConstLib.COUNDOWNBAR) != null)
                {
                    canvas.transform.Find(ConstLib.COUNDOWNBAR).GetComponent<Timer>().TimerAdd();
                }
                else if (canvas.name == ConstLib.ANSWERCANVAS)
                {
                    canvas.transform.parent.transform.Find(ConstLib.COUNDOWNBAR).GetComponent<Timer>().TimerAdd();
                }
            }
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
