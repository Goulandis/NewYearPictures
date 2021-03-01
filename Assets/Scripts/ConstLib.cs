using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// 全局类，管理全局变量
/// </summary>
public class ConstLib : MonoBehaviour
{
    public static int KOWNHOWMUCH_TIMER_LEN = 3;

    public static string SLIDESHOWSPRITE_PATH = "\\SlideshowSprite";

    /// <summary>
    /// 挑战成功预设
    /// </summary>
    public static string VECTORYCANVASP_PATH = "Prefabs/VectoryCanvas";

    /// <summary>
    /// 挑战失败预设
    /// </summary>
    public static string FAILLCANVAS_PATH = "Prefabs/FaillCanvas";

    public static string INTEGRAL_PATH = "Prefabs/Integral";

    /// <summary>
    /// 记录游戏分数的文件路径
    /// </summary>
    public static string INTEGRAL_FILE_PATH = "/Files/Integral.txt";

    /// <summary>
    /// 游戏分数
    /// </summary>
    public static int Integral = 0;

    public static void WriteIntegral()
    {
        string path = Application.streamingAssetsPath + INTEGRAL_FILE_PATH;
        File.WriteAllText(path, Convert.ToString(Integral));
    }

}
