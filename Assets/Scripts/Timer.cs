using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeMax = 10f;
    //public float interval = 1f;

    //float timer = 0f;
    public bool autoTimer = true;
    public bool triggerTimer = false;
    Slider countDownBar;
    Canvas parentCanvas;
    // Start is called before the first frame update
    void Start()
    {
        countDownBar = GetComponent<Slider>();
        parentCanvas = gameObject.transform.parent.GetComponent<Canvas>();
        countDownBar.maxValue = timeMax;
        countDownBar.value = countDownBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        //自动倒计时
        if (parentCanvas.enabled == true && autoTimer == true)
        {
            countDownBar.value -= Time.deltaTime;
        }
        else if(parentCanvas.enabled == false)
        {
            countDownBar.maxValue = timeMax;
            countDownBar.value = countDownBar.maxValue;
        }
        //触发倒计时
        if (parentCanvas.enabled == true && autoTimer == false && triggerTimer == true)
        {
            countDownBar.value -= Time.deltaTime;
        }
    }

    public void UseUpTimer()
    {
        if (countDownBar.value <= countDownBar.minValue)
        {
            //Debug.Log("Use Up Time");
        }
    }

    public void TriggerTimer(bool triggerTimer)
    {
        this.triggerTimer = triggerTimer;
    }

    public void TimerAdd()
    { 
        if (PlayerState.prop.TimeAddNum > 0)
        {
            PlayerState.prop.TimeAddNum--;
            PlayerState.usingTimeAdd = false;
            PlayerState.SetTimeAddNum();
            countDownBar.value += ConstLib.TimeAddInterval;
            ToolLib.Tig(gameObject.transform, "时间已增加10秒");
        }        
    }
}
