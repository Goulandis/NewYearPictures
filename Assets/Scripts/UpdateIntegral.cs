using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UpdateIntegral : MonoBehaviour
{
    bool readEnable = true;
    Canvas mainCanvas;
    Text integral;

    void Start()
    {
        mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        integral = mainCanvas.transform.Find("Integral").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (readEnable == true && mainCanvas.enabled == true)
        {
            string path = Application.streamingAssetsPath + ConstLib.INTEGRAL_FILE_PATH;
            integral.text = string.Format("总积分：" + File.ReadAllText(path));
            readEnable = false;
        }
        else if(readEnable == false && mainCanvas.enabled == false)
        {
            readEnable = true;
        }
    }
}
