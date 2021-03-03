using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Bubble = UnityEngine.UI.Image;

public class BubbleWay
{
    public int index;
    public Vector2 position;
    public Image posBubble;
    public Image refBubble;

    public Image RefBubble
    {
        get
        {
            return refBubble;
        }
        set
        {
            refBubble = value;
            refBubble.transform.position = position;
        }
    }

    public BubbleWay(Image img,int index)
    {
        posBubble = img;
        position = img.rectTransform.position;
        this.index = index;
    }
}

/// <summary>
/// 泡泡队列
/// </summary>
public class BubbleQueue
{
    const int len = 48;

    public BubbleWay[] queue = new BubbleWay[len];

    //BubbleWay zeroBubble;
    public int ptr = 0;
    public int refPtr = 0;

    public Action ShowPic;

    public void Init(Bubble img)
    {
        if (ptr < len)
        {
            queue[ptr] = new BubbleWay(img,ptr);
            ptr++;  
        }
    }

    public int GetLen()
    {
        return len;
    }

    /// <summary>
    /// 向泡泡队头部添加一个泡泡
    /// </summary>
    /// <param name="bubble"></param>
    public void Enqueue(Bubble bubble)
    {        
        for (int i = len - 1; i > 0; i--)
        {
            if (queue[i - 1].RefBubble != null)
            {
                queue[i].RefBubble = queue[i - 1].RefBubble;
                queue[i].RefBubble.sprite = queue[i - 1].RefBubble.sprite;
            } 
        }
        if (refPtr >= len - 1)
        {
            MonoBehaviour.Destroy(queue[len-1].refBubble.gameObject);
        }
        queue[0].RefBubble = bubble;
        refPtr++; 
    }

    public BubbleWay GetItem(int i)
    {
        if (i < len)
        {
            return queue[i];
        }
        return null;
    }

    public void OnTrigger(Bubble bullet, Bubble bubble)
    {
        for (int i = 0; i < len; i++)
        {
            if (queue[i].RefBubble != null)
            {
                if (queue[i].RefBubble == bubble)
                {
                    for (int j = i; j > 0; j--)
                    {
                        queue[j].RefBubble.sprite = queue[j - 1].RefBubble.sprite;
                        queue[j].refBubble.transform.position = queue[j].position;
                    }
                }
            }
        }
        
        ShowPic();
    }

    private void Remove(Bubble bubble)
    {
        for (int i = 0; i < len; i++)
        {
            if (queue[i].RefBubble == null || queue[0].RefBubble == null)
            {
                return;
            }
            if (queue[i].RefBubble == bubble)
            {
                for (int j = i; j > 0; j--)
                {
                    queue[j].RefBubble = queue[j-1].RefBubble;                   
                }
            }
        }
        
    }
}

public class FishingToLeUI : MonoBehaviour
{
    const float offset = 100;

    Canvas fishingToLeCanvas;
    CanvasManager manager;

    RectTransform pathWay;
    RectTransform bubbleWay;
    RectTransform bubbleQueue;
    Slider countDownBar;

    Image fisher;
    Image lookAtPoint;
    Image initBullet;
    Image pic;
    InitBulletColor colorLib;
    Canvas vectoryCanvas;
    Canvas faillCanvas;

    BubbleQueue queue = new BubbleQueue();

    const string pathWayPath = "Prefabs/PathWay";
    const string bubblePath = "Prefabs/Bubble";
    const string bulletPath = "Prefabs/Bullet";

    Bubble bubblePrefab;
    Bubble bulletPrefab;

    float timer = 0f;
    bool start = false;
    bool randomSprite = false;
    int spriteNum = 0;
    int showedNum = 1;
    Vector2 pos = Vector2.zero;
    float rightfloat;
    float upfloat;
    Vector2 dis;
    Color picColor;

    //泡泡出现的几率，越小几率越高
    public int probability = 7;
    public float interval = 3f;

    Bubble bullet;

    void Start()
    {
        fishingToLeCanvas = GameObject.Find(ConstLib.FISHINGTOLECANVAS).GetComponent<Canvas>();

        pathWay = fishingToLeCanvas.transform.Find("PathWay").GetComponent<RectTransform>();
        bubbleWay = fishingToLeCanvas.transform.Find("BubbleWay").GetComponent<RectTransform>();
        bubbleQueue = fishingToLeCanvas.transform.Find("BubbleQueue").GetComponent<RectTransform>();
        pic = fishingToLeCanvas.transform.Find("Pic").GetComponent<Image>();
        picColor = pic.transform.Find(ConstLib.MASK1).GetComponent<Image>().color;
        fisher = fishingToLeCanvas.transform.Find("Fisher").GetComponent<Image>();
        lookAtPoint = fishingToLeCanvas.transform.Find("LookAtPoint").GetComponent<Image>();
        countDownBar = fishingToLeCanvas.transform.Find("CountDownBar").GetComponent<Slider>();
        initBullet = fisher.transform.Find("InitBullet").GetComponent<Image>();
        manager = GameObject.Find(ConstLib.CANVASMANAGER).GetComponent<CanvasManager>();

        initBullet.sprite = manager.GetComponent<UIResource>().RandomBubbleSprite();
        queue.ShowPic = ShowPic;
        InitBubbleQueue();
    }

    private void Awake()
    {
        bubblePrefab = Resources.Load<Bubble>(bubblePath);
        bulletPrefab = Resources.Load<Bubble>(bulletPath);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (fishingToLeCanvas.enabled == false)
        {
            start = false;
        }
        if (start)
        {
            SpawnBubbles();
            FisherRotate();
            EmitterBullet();
        }
        if(fishingToLeCanvas.enabled == false && bubbleQueue.childCount > 0)
        {
            Clear();
        }
        if (randomSprite)
        {
            InitShowPic();
            randomSprite = false;
        }
        if (faillCanvas == null && showedNum < pic.transform.childCount && countDownBar.value <= countDownBar.minValue)
        {
            //faillCanvas = Instantiate(Resources.Load<Canvas>(ConstLib.PREFAB_FAILLCANVAS), fishingToLeCanvas.transform);
            faillCanvas = ToolLib.EndTig(false, fishingToLeCanvas.transform);
        }
    }

    public void Click()
    {
        manager.Push(fishingToLeCanvas);
        start = true;
        randomSprite = true;
    }

    void SpawnBubbles()
    {
        if (timer >= interval)
        {
            Bubble bubble = Instantiate(bubblePrefab, bubbleQueue);
            bubble.sprite = manager.GetComponent<UIResource>().RandomBubbleSprite();
            if (bubble.sprite.name != initBullet.sprite.name && spriteNum >= probability)
            {
                bubble.sprite = initBullet.sprite;
            }
            if(bubble.sprite.name == initBullet.sprite.name)
            {
                spriteNum = 0;
            }
            bubble.GetComponent<BubbleAndBulletScript>().OnTrigger = queue.OnTrigger;  
            queue.Enqueue(bubble);
            timer = 0;
            spriteNum++;
        }
    }

    void InitBubbleQueue()
    {
        foreach (Transform item in bubbleWay)
        {
            queue.Init(item.gameObject.GetComponent<Bubble>());
        }
    }

    /// <summary>
    /// 让发射器跟着鼠标旋转
    /// </summary>
    void FisherRotate()
    {
        ////屏幕坐标转换为本地坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            fishingToLeCanvas.GetComponent<RectTransform>(),
            Input.mousePosition, fishingToLeCanvas.worldCamera,
            out pos);

        dis = lookAtPoint.GetComponent<RectTransform>().anchoredPosition = pos;

        //dis = lookAtPoint.GetComponent<RectTransform>().anchoredPosition = Input.mousePosition;

        //fisher.transform.LookAt(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0));

        rightfloat = Vector2.Angle(dis, Vector2.right);
        upfloat = Vector2.Angle(dis, Vector2.up);
        if (rightfloat < 90)
        {
            fisher.transform.localEulerAngles = new Vector3(0, 0, -upfloat);
        }
        else
        {
            fisher.transform.localEulerAngles = new Vector3(0, 0, upfloat);
        }
    }

    void EmitterBullet()
    {
        if (Input.GetMouseButtonDown(0) && bullet == null)
        {
            bullet = Instantiate(bulletPrefab, fishingToLeCanvas.transform);
            bullet.rectTransform.position = fisher.rectTransform.position;
            bullet.GetComponent<BulletMove>().direction = pos;
            bullet.sprite = initBullet.sprite;
        }
    }

    void InitShowPic()
    {
        pic.sprite = manager.GetComponent<UIResource>().RandomNewYearPic();
        initBullet.sprite = manager.GetComponent<UIResource>().picDic[pic.sprite];
    }

    void ShowPic()
    {
        foreach (Transform mask in pic.transform)
        {
            if (showedNum >= pic.transform.childCount && countDownBar.value >= countDownBar.minValue && vectoryCanvas == null)
            {
                //vectoryCanvas = Instantiate(Resources.Load<Canvas>(ConstLib.PREFAB_VECTORYCANVASP), fishingToLeCanvas.transform);
                //vectoryCanvas = ToolLib.EndTig(true, fishingToLeCanvas.transform);
                NextLevel();
            }
            Image img = mask.gameObject.GetComponent<Image>();
            if (img.color.a != 0)
            {
                img.color = new Color(img.color.r,img.color.g,img.color.b,0);
                showedNum++;
                return;
            }
        }
    }

    void NextLevel()
    {
        ConstLib.Integral += ConstLib.FishingToLeIntegral;
        ConstLib.WriteIntegral();
        InitShowPic();
        countDownBar.value += ConstLib.FishingToLeTimeAddInterval;
        showedNum = 0;
        foreach (Transform mask in pic.transform)
        {           
            Image img = mask.gameObject.GetComponent<Image>();
            if (img.color.a == 0)
            {
                img.color = picColor;
            }
        }
    }

    void Clear()
    {
        foreach (Transform mask in pic.transform)
        {
            Image img = mask.gameObject.GetComponent<Image>();
            if (img.color.a == 0)
            {
                img.color = picColor;
            }
        }
        foreach (Transform child in bubbleQueue)
        {
            Destroy(child.gameObject);
        }

        queue.ptr = 0;
        queue.refPtr = 0;
        showedNum = 0;
    }
}
