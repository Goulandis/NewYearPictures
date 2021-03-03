using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateCanvasScript : MonoBehaviour
{
    Text boomNumTxt;
    Text totalIntegralTxt;
    Text timeAddNumTxt;


    /// <summary>
    /// 初始化状态
    /// </summary>
    public void InitState()
    {
        boomNumTxt = gameObject.transform.Find(ConstLib.BOOMNUMTXT).GetComponent<Text>();
        totalIntegralTxt = gameObject.transform.Find(ConstLib.TOTALINTEGRALTXT).GetComponent<Text>();
        timeAddNumTxt = gameObject.transform.Find(ConstLib.TIMEADDNUMTXT).GetComponent<Text>();
        totalIntegralTxt.text = ConstLib.Integral.ToString();
        boomNumTxt.text = PlayerState.prop.BoomNum.ToString();
        timeAddNumTxt.text = PlayerState.prop.TimeAddNum.ToString();
    }
}
