using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

/// <summary>
/// 题库类，用于装载与切换题目
/// </summary>
public class TopicLib : MonoBehaviour
{
    const string TOPICPATH = "\\Files\\KownHowMuch\\TopicLib.json";
    const string TOPICS = "Topics";
    const string TOPIC = "Topic";
    const string ANSWERS = "Answers";
    const string A = "A";
    const string B = "B";
    const string C = "C";
    const string D = "D";
    const string RIGHTANSWER = "RightAnswer";

    Text topic;
    Text answerA;
    Text answerB;
    Text answerC;
    Text answerD;
    Button btnStart;
    Button btnNext;
    Toggle toggleA;
    Toggle toggleB;
    Toggle toggleC;
    Toggle toggleD;

    List<Toggle> toggles = new List<Toggle>();

    Canvas kownHowMuchCanvas;
    Canvas answerCanvas;
    Slider countDownBar;
  
    Canvas faillCanvas;

    JArray topicArr = new JArray();

    int topicIndex = 0;

    //要求分数
    public int requstGrade = 2;
    //当前分数
    int currentGrade = 0;

    void Start()
    {
        kownHowMuchCanvas = GameObject.Find("KownHowMuchCanvas").GetComponent<Canvas>();
        answerCanvas = kownHowMuchCanvas.transform.Find("AnswerCanvas").GetComponent<Canvas>();
        countDownBar = kownHowMuchCanvas.transform.Find("CountDownBar").GetComponent<Slider>();

        try
        {
            topic = kownHowMuchCanvas.transform.Find("AnswerCanvas/Topic").GetComponent<Text>();

            toggleA = kownHowMuchCanvas.transform.Find("AnswerCanvas/AnswerA").GetComponent<Toggle>();
            toggleA.isOn = false;
            toggles.Add(toggleA);
            answerA = kownHowMuchCanvas.transform.Find("AnswerCanvas/AnswerA/Label").GetComponent<Text>();

            toggleB = kownHowMuchCanvas.transform.Find("AnswerCanvas/AnswerB").GetComponent<Toggle>();
            toggleB.isOn = false;
            toggles.Add(toggleB);
            answerB = kownHowMuchCanvas.transform.Find("AnswerCanvas/AnswerB/Label").GetComponent<Text>();

            toggleC = kownHowMuchCanvas.transform.Find("AnswerCanvas/AnswerC").GetComponent<Toggle>();
            toggleC.isOn = false;
            toggles.Add(toggleC);
            answerC = kownHowMuchCanvas.transform.Find("AnswerCanvas/AnswerC/Label").GetComponent<Text>();

            toggleD = kownHowMuchCanvas.transform.Find("AnswerCanvas/AnswerD").GetComponent<Toggle>();
            toggleD.isOn = false;
            toggles.Add(toggleD);
            answerD = kownHowMuchCanvas.transform.Find("AnswerCanvas/AnswerD/Label").GetComponent<Text>();

            btnStart = kownHowMuchCanvas.transform.Find("StartAnswerCanvas/BtnStart").GetComponent<Button>();
            btnNext = kownHowMuchCanvas.transform.Find("AnswerCanvas/BtnNext").GetComponent<Button>();
        }
        catch (ArgumentNullException e)
        {
            Debug.LogError(e);
            throw (e);
        }
    }

    void Update()
    {
        if (answerCanvas.enabled == false)
        {
            topicIndex = 0;
            currentGrade = 0;
        }
        if (faillCanvas == null && countDownBar.value <= countDownBar.minValue && currentGrade < requstGrade)
        {
            //弹出失败界面
            faillCanvas = Instantiate(Resources.Load<Canvas>(ConstLib.FAILLCANVAS_PATH), kownHowMuchCanvas.transform);
        }
    }

    public void Click()
    {       
        //遍历所有答案，判断当前勾选答案是否为正确答案
        if (topicIndex < topicArr.Count)
        {
            foreach (Toggle toggle in toggles)
            {
                if (toggle.isOn == true)
                {
                    if (toggle.tag == Convert.ToString(topicArr[topicIndex][RIGHTANSWER])
                        && countDownBar.value > countDownBar.minValue)
                    {
                        //计分
                        currentGrade++;
                    }
                }
            }


            //如果时间未结束，平且分数达到要求值，就弹出胜利框
            if (countDownBar.value > countDownBar.minValue && currentGrade >= requstGrade)
            {
                Instantiate(Resources.Load<Canvas>(ConstLib.VECTORYCANVASP_PATH), kownHowMuchCanvas.transform);
            }

            topicIndex++;
        }

        //在题库中至少有两个题时才激活下一题切换
        if (topicIndex < topicArr.Count)
        {
            SetTopics(topicIndex);
        }
    }

    /// <summary>
    /// 从json文件中读取题库到内存
    /// </summary>
    public void ReadTopics()
    {
        //生成json文件路径
        string path = Application.streamingAssetsPath + TOPICPATH;
        //读取json文件的内容到字符串
        string json = File.ReadAllText(path);

        //将json字符串反序列化为对象
        JObject obj = (JObject)JsonConvert.DeserializeObject(json);

        //将题库拆解到数组中存储
        topicArr = JArray.Parse(obj[TOPICS].ToString());
    }

    /// <summary>
    /// 设置题目到AnswerCanvas上
    /// </summary>
    /// <param name="i"></param>
    public void SetTopics(int i = 0)
    {
        //判断字库中是否有题目
        if (i >= topicArr.Count)
        {
            return;
        }

        topic.text = Convert.ToString(topicArr[i][TOPIC]);
        //把答案拆分到数组里
        JObject answer = JObject.Parse(Convert.ToString(topicArr[i]));

        answerA.text = Convert.ToString(answer[ANSWERS][A]);
        answerB.text = Convert.ToString(answer[ANSWERS][B]);
        answerC.text = Convert.ToString(answer[ANSWERS][C]);
        answerD.text = Convert.ToString(answer[ANSWERS][D]);
    }
}
