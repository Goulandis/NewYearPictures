using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutScript : MonoBehaviour
{
    Canvas emigratedCanvas;
    CanvasManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("CanvasManager").GetComponent<CanvasManager>();
        emigratedCanvas = GameObject.Find("EmigratedCanvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        manager.Push(emigratedCanvas);
    }
}
