using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectoryCanvasBtn : MonoBehaviour
{
    public void Click()
    {
        CanvasManager manager = GameObject.Find("CanvasManager").GetComponent<CanvasManager>();
        //判断UI栈栈顶元素是否为主界面
        while (manager.Peek().name != "MainCanvas")
        {
            manager.Pop();
        }

        Destroy(gameObject.transform.parent.gameObject);
    }
}
