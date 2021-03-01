using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class AnswerCanvasUI : MonoBehaviour
{
    const int texW = 372;
    const int texH = 562;

    Canvas kownHowMuchCanvas;
    Canvas answerCanvas;
    Canvas slideshowCanvas;
    Canvas startAnswerCanvas;
    Button btnNext;
    Slider countDownBar;

    string pictureDic;

    float timer = 0;
    int imageIndex = 0;

    List<Texture2D> textures = new List<Texture2D>();
    JArray topicArr = new JArray();

    CanvasManager manager;

    // Start is called before the first frame update
    void Start()
    {
        kownHowMuchCanvas = GameObject.Find("KownHowMuchCanvas").GetComponent<Canvas>();
        manager = GameObject.Find("CanvasManager").GetComponent<CanvasManager>();
        try
        {
            countDownBar = kownHowMuchCanvas.transform.Find("CountDownBar").GetComponent<Slider>();
            answerCanvas = kownHowMuchCanvas.transform.Find("AnswerCanvas").GetComponent<Canvas>();
            answerCanvas.enabled = false;
            btnNext = answerCanvas.transform.Find("BtnNext").GetComponent<Button>();
            slideshowCanvas = kownHowMuchCanvas.transform.Find("SlideshowCanvas").GetComponent<Canvas>();
            startAnswerCanvas = kownHowMuchCanvas.transform.Find("StartAnswerCanvas").GetComponent<Canvas>();
        }
        catch (ArgumentNullException e)
        {
            Debug.LogError(e);
            throw (e);
        }

        InitSlideshow();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        SetSlideshow();
        if (answerCanvas.enabled == false && countDownBar.GetComponent<Timer>().triggerTimer == true)
        {
            countDownBar.GetComponent<Timer>().TriggerTimer(false);
        }
    }

    void InitSlideshow()
    {
        //取当前目录
        pictureDic = Application.streamingAssetsPath;
        //合成可用目录
        pictureDic += ConstLib.SLIDESHOWSPRITE_PATH;
        //目录是否存在
        if (!Directory.Exists(pictureDic))
        {
            return;
        }
        //获取目录信息
        DirectoryInfo dic = new DirectoryInfo(pictureDic);
        //获取目录下的所有文件
        FileInfo[] imageFiles = dic.GetFiles("*", SearchOption.AllDirectories);

        for (int i = 0; i < imageFiles.Length; i++)
        {
            if (!imageFiles[i].Name.EndsWith(".meta"))
            {
                //度图片数据
                FileStream stream = new FileStream(imageFiles[i].FullName, FileMode.Open, FileAccess.Read);
                stream.Seek(0, SeekOrigin.Begin);
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                stream.Close();
                stream.Dispose();
                stream = null;
                //设置贴图
                textures.Add(new Texture2D(texW, texH));
                textures[textures.Count-1].LoadImage(bytes);
            }
        }
    }

    /// <summary>
    /// 设置轮播图
    /// </summary>
    void SetSlideshow()
    {
        //获取图片引用
        Image img = slideshowCanvas.GetComponent<Image>();

        if (img == null)
        {
            return;
        }
        if (imageIndex >= textures.Count)
        {
            imageIndex = 0;
        }
        while (timer >= ConstLib.KOWNHOWMUCH_TIMER_LEN && imageIndex < textures.Count)
        {
            //设置图片
            Sprite sprite = Sprite.Create(textures[imageIndex], new Rect(0, 0, 745, 1119),new Vector2(0,0));
            img.sprite = sprite;
            timer = 0;
            imageIndex++;
        }
    }

    public void Click()
    {
        manager.Push(answerCanvas);

        //启动倒计时
        countDownBar.GetComponent<Timer>().TriggerTimer(true);

        TopicLib topic = btnNext.GetComponent<TopicLib>();
        if (topic != null)
        {
            //加载题库
            topic.ReadTopics();
            //设置题库
            try
            {
                topic.SetTopics(topic.topicNO[0]);
            }
            catch (IndexOutOfRangeException e)
            {
                Debug.LogError(e);
                throw (e);
            }
        }
    }
}
