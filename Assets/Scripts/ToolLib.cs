using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ToolLib : MonoBehaviour
{
    public static void Tig(Transform transform ,string context,float time = 1)
    {
        Text txt = Instantiate(Resources.Load<Text>(ConstLib.PREFAB_TIG), transform);
        txt.GetComponent<TigScript>().time = time;
        txt.text = Convert.ToString(context);
    }

    public static Canvas EndTig(bool state, Transform transform)
    {
        Canvas canvas;
        if (state == true)
        {
            canvas = Instantiate(Resources.Load<Canvas>(ConstLib.PREFAB_VECTORYCANVASP), transform);
        }
        else
        {
            canvas = Instantiate(Resources.Load<Canvas>(ConstLib.PREFAB_FAILLCANVAS), transform);
        }
        return canvas;
    }
}
