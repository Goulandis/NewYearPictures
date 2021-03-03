using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System;

public class PlayerState : MonoBehaviour
{
    public static Prop prop = new Prop();
    public static bool usingBoom = false;
    public static bool usingTimeAdd = false;

    private static JObject state;
    private static string path = string.Empty;

    public static void InitPropsNum()
    {
        path = Application.streamingAssetsPath + ConstLib.FILEPATH_PLAYERSTATE;

        string json = File.ReadAllText(path);

        //将json字符串反序列化为对象
        state = (JObject)JsonConvert.DeserializeObject(json);

        prop.BoomNum = Convert.ToInt32(state["BoomNum"]);
        prop.TimeAddNum = Convert.ToInt32(state["TimeAddNum"]);
    }

    public static void SetBoomNum(int num = 0)
    {
        if (num != 0)
        {
            prop.BoomNum = num;
        }
        
        string json = JsonConvert.SerializeObject(prop);
        if (path != string.Empty)
        {
            File.WriteAllText(path, json);
        }
    }

    public static void SetTimeAddNum(int num = 0)
    {
        if (num != 0)
        {
            prop.TimeAddNum = num;
        }       
        string json = JsonConvert.SerializeObject(prop);
        if (path != string.Empty)
        {
            File.WriteAllText(path, json);
        }
    }
}

public class Prop
{
    public int BoomNum = 0;
    public int TimeAddNum = 0;
}
