using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class TigBubbleScript : MonoBehaviour
{
    const string KOWNLEDGESPATH = "\\Files\\NewYearPicKownledge.json";
    const string KONWLEDGES = "Konwledges";
    const string TIG = "Tig";
    const string TIGBUBBLEPATH = "Prefabs/TigBubble";

    JArray kArr = new JArray();

    float timer = 0;
    public float interval = 5f;
    public float destroyInterval = 3f;
    Image tigBubble;

    Vector2 tigSVec;
    Vector2 tigEVec;
    CanvasManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("CanvasManager").GetComponent<CanvasManager>();
        ReadTopics();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval && tigBubble == null)
        {
            Canvas canvas = manager.Peek();
            if (canvas.name == "MainCanvas" || canvas.name == "StartAnswerCanvas" || canvas.name == "FishingToLeCanvas")
            {
                tigSVec = canvas.transform.Find("TigS").GetComponent<Image>().rectTransform.position;
                tigEVec = canvas.transform.Find("TigE").GetComponent<Image>().rectTransform.position;
                float x = UnityEngine.Random.Range(tigSVec.x, tigEVec.x);
                float y = UnityEngine.Random.Range(tigEVec.y, tigSVec.y);
                tigBubble = Instantiate(Resources.Load<Image>(TIGBUBBLEPATH), canvas.transform);

                SetTigToTigBubble(tigBubble.transform.Find("Tig").GetComponent<Text>());

                tigBubble.rectTransform.position = new Vector2(x, y);
            }
        }
        if (timer >= interval + destroyInterval && tigBubble != null)
        {
            timer = 0;
            Destroy(tigBubble.gameObject);
        }
    }

    /// <summary>
    /// 从json文件中读取题库到内存
    /// </summary>
    void ReadTopics()
    {
        //生成json文件路径
        string path = Application.streamingAssetsPath + KOWNLEDGESPATH;
        //读取json文件的内容到字符串
        string json = File.ReadAllText(path);

        //将json字符串反序列化为对象
        JObject obj = (JObject)JsonConvert.DeserializeObject(json);

        //将小知识库拆解到数组中存储
        kArr = JArray.Parse(obj[KONWLEDGES].ToString());
    }

    void SetTigToTigBubble(Text tig)
    {
        tig.text = Convert.ToString(kArr[UnityEngine.Random.Range(0, kArr.Count)][TIG]);
    }
}
