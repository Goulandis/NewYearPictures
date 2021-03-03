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
    public static readonly int KOWNHOWMUCH_TIMER_LEN = 3;

    public static readonly string SLIDESHOWSPRITE_PATH = "\\SlideshowSprite";

    /// <summary>
    /// 挑战成功预设
    /// </summary>
    public static readonly string PREFAB_VECTORYCANVASP = "Prefabs/VectoryCanvas";
    /// <summary>
    /// 挑战失败预设
    /// </summary>
    public static readonly string PREFAB_FAILLCANVAS = "Prefabs/FaillCanvas";
    public static readonly string PREFAB_STATECANVAS = "Prefabs/StateCanvas";
    public static readonly string PREFAB_TIG = "Prefabs/Tig";

    /// <summary>
    /// 记录游戏分数的文件路径
    /// </summary>
    public static readonly string INTEGRAL_FILE_PATH = "/Files/Integral.txt";
    public static readonly string FILEPATH_PLAYERSTATE = "/Files/PlayerState.json";

    /// <summary>
    /// 游戏分数
    /// </summary>
    public static int Integral = 0;
    public static int TimeAddInterval = 10;
    /// <summary>
    /// 通过一关年画消消乐所获取的积分
    /// </summary>
    public static int EliminateToLeIntegral = 50;
    /// <summary>
    /// 通过一个年画知多少所获取得积分
    /// </summary>
    public static int KownHowMuchIntegral = 20;
    /// <summary>
    /// 通过一关年画钓钓乐所获得的积分
    /// </summary>
    public static int FishingToLeIntegral = 30;
    /// <summary>
    /// 通过一关年画钓钓乐倒计时增加30秒
    /// </summary>
    public static int FishingToLeTimeAddInterval = 30;

    public static void WriteIntegral()
    {
        string path = Application.streamingAssetsPath + INTEGRAL_FILE_PATH;
        File.WriteAllText(path, Convert.ToString(Integral));
    }

    /// <summary>
    /// 界面名称管理
    /// </summary>
    public static readonly string STARTGAMECANVAS = "StartGameCanvas";
    public static readonly string MAINCANVAS = "MainCanvas";
    public static readonly string KOWNHOWMUCHCANVAS = "KownHowMuchCanvas";
    public static readonly string ELIMINATETOLECANVAS = "EliminateToLeCanvas";
    public static readonly string FISHINGTOLECANVAS = "FishingToLeCanvas";
    public static readonly string SHOPCANVAS = "ShopCanvas";
    public static readonly string STATECANVAS = "StateCanvas";
    public static readonly string CANVASMANAGER = "CanvasManager";
    public static readonly string ABOUTCANVAS = "EmigratedCanvas";
    public static readonly string ANSWERCANVAS = "AnswerCanvas";

    public static readonly string BTNBUYBOOM = "BtnBuyBoom";
    public static readonly string BTNBUYTIMEADD = "BtnBuyTimeAdd";
    public static readonly string BTNSHOP = "BtnShop";
    public static readonly string STARTVEDIO = "StartVedio";
    public static readonly string BTNKOWNHOWMUCH = "BtnKownHowMuch";
    public static readonly string BTNSTARTGAME = "BtnStartGame";
    public static readonly string BOOMNUMTXT = "BoomNumTxt";
    public static readonly string TOTALINTEGRALTXT = "TotalIntegralTxt";
    public static readonly string TIMEADDNUMTXT = "TimeAddNumTxt";
    public static readonly string COUNDOWNBAR = "CountDownBar";
    public static readonly string MASK1 = "Mask1";

    public static readonly List<string> GAMECANVAS = new List<string>()
    {
        ANSWERCANVAS,
        ELIMINATETOLECANVAS,
        FISHINGTOLECANVAS
    };
    
}
