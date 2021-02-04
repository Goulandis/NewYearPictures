using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KownHowMuchUI : MonoBehaviour
{
    Canvas kownHowMuchCanvas;
    Canvas startAnswerCanvas;
    CanvasManager manager;

    void Start()
    {
        try
        {
            kownHowMuchCanvas = GameObject.Find("KownHowMuchCanvas").GetComponent<Canvas>();
            startAnswerCanvas = kownHowMuchCanvas.transform.Find("StartAnswerCanvas").GetComponent<Canvas>();
            manager = GameObject.Find("CanvasManager").GetComponent<CanvasManager>();
        }
        catch (ArgumentNullException e)
        {
            Debug.LogError(e);
            throw (e);
        }
    }

    public void Click()
    {
        manager.Push(kownHowMuchCanvas);
        manager.Push(startAnswerCanvas);
    }
}
