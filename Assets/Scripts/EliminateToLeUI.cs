using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Block = UnityEngine.UI.Button;

//点击状态，
public enum HitInfo
{
    MISS,//十字领域没有相同色块时的状态
    HIT//十字领域有至少两块相同色块时的状态
}

public class EliminateToLeUI : MonoBehaviour
{
    Canvas eliminateToLeCanvas;
    Canvas mainCanvs;
    CanvasManager manager;

    Image newYearPic;
    Slider countDownBar;

    Canvas faillCanvas;
    
    //屏幕尺寸
    const int width = 1920;
    const int height = 1080;
    //年画的固定行索引
    const int newYearPicRow = 14;
    //年画的最小列索引
    const int newYearPicMixI = 9;
    //年画的最大列索引
    const int newYearPicMaxI = 40;

    int rowBlockNum;
    int colBlockNum;

    int blockWidth;
    int blockHeight;

    /// <summary>
    /// 存储所有的小方块
    /// </summary>
    List<List<Block>> blocks = new List<List<Block>>();
    /// <summary>
    /// 存储包含在图片内部的小方块
    /// </summary>
    List<Block> inPicBlocks = new List<Block>();

    BlockScript blockScript;

    bool hasClearArea = false;

    void Start()
    {
        eliminateToLeCanvas = GameObject.Find("EliminateToLeCanvas").GetComponent<Canvas>();
        newYearPic = eliminateToLeCanvas.transform.Find("NewYearPic").GetComponent<Image>();
        countDownBar = eliminateToLeCanvas.transform.Find("CountDownBar").GetComponent<Slider>();

        mainCanvs = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        manager = GameObject.Find("CanvasManager").GetComponent<CanvasManager>();
        Block block = Resources.Load<Block>("Prefabs/Block");

        block = Instantiate(block);

        blockWidth = Convert.ToInt32(block.GetComponent<RectTransform>().rect.width);
        blockHeight = Convert.ToInt32(block.GetComponent<RectTransform>().rect.height);

        //在右侧减少一列
        colBlockNum = width / blockWidth-1;
        rowBlockNum = height / blockHeight;

        this.blockWidth = blockWidth / 2;
        this.blockHeight = blockHeight / 2;

        blockScript = block.GetComponent<BlockScript>();

        DestroyImmediate(block.gameObject);
    }

    void Update()
    {
        if (eliminateToLeCanvas.enabled == false && eliminateToLeCanvas.transform.childCount > 0)
        {  
            foreach (Transform child in eliminateToLeCanvas.transform)
            {
                if (child.gameObject.name != newYearPic.name && child.gameObject.name != countDownBar.name)
                {
                    DestroyImmediate(child.gameObject);
                    blocks.Clear();
                    inPicBlocks.Clear();
                }
            }
            hasClearArea = false;
        }
        if (faillCanvas == null && hasClearArea == false && countDownBar.value <= countDownBar.minValue)
        {
            faillCanvas = Instantiate(Resources.Load<Canvas>(ConstLib.FAILLCANVAS_PATH), eliminateToLeCanvas.transform);
        }
    }

    public void Click()
    {
        manager.Push(eliminateToLeCanvas);
        //初始化画布
        InitCanvas();
        InitNewYearPic();
    }

    /// <summary>
    /// 初始化画布，将小方块填充整个画布
    /// </summary>
    void InitCanvas()
    {
        int initX;
        int initY = blockHeight;

        //blocks.count--画布行数，blocks[i].count--画布列数
        for (int row = 0; row < rowBlockNum; row++)
        {
            initX = blockWidth;
            blocks.Add(new List<Block>());
            for (int col = 0; col < colBlockNum; col++)
            {
                //实例化小方块
                Block blockPrefab = Resources.Load<Block>("Prefabs/Block");
                Block blockTmp = Instantiate(blockPrefab, eliminateToLeCanvas.transform);

                blockTmp.GetComponent<BlockScript>().ClearCrossArea = ClearCrossArea;

                blockTmp.transform.position = new Vector3(initX,initY, 0);
                blockTmp.GetComponent<BlockScript>().SetIndex(row, col);
                
                int color = UnityEngine.Random.Range(0, 10);
                blockTmp.GetComponent<Image>().color = blockScript.colors[color];

                blocks[row].Add(blockTmp);

                initX += blockWidth * 2;
            }
            initY += blockHeight * 2;
        }
    }

    /// <summary>
    /// 消除十字领域的相同色块
    /// </summary>
    /// <param name="rowIndex"></param>
    /// <param name="colIndex"></param>
    public void ClearCrossArea(int rowIndex,int colIndex)
    {
        List<Block> fourBlocks = new List<Block>() {
            blocks[0][0],
            blocks[0][0],
            blocks[0][0],
            blocks[0][0]
        };

        int searchIndex;
        Color searchColor;
        
        //向下找
        for (searchIndex = 0; rowIndex - searchIndex >= 0; searchIndex++)
        {
            searchColor = blocks[rowIndex - searchIndex][colIndex].GetComponent<Image>().color;
            if (searchColor != blockScript.colors[0])
            {
                fourBlocks[0] = blocks[rowIndex - searchIndex][colIndex];
                break;
            }
        }
        //向上找
        for (searchIndex = 0; rowIndex + searchIndex < rowBlockNum; searchIndex++)
        {
            searchColor = blocks[rowIndex + searchIndex][colIndex].GetComponent<Image>().color;
            if (searchColor != blockScript.colors[0])
            {
                fourBlocks[1] = blocks[rowIndex + searchIndex][colIndex];
                break;
            }
        }
        //向左找
        for (searchIndex = 0; colIndex - searchIndex >= 0; searchIndex++)
        {
            searchColor = blocks[rowIndex][colIndex - searchIndex].GetComponent<Image>().color;
            if (searchColor != blockScript.colors[0])
            {
                fourBlocks[2] = blocks[rowIndex][colIndex - searchIndex];
                break;
            }
        }
        //向右找
        for (searchIndex = 0; colIndex + searchIndex < colBlockNum; searchIndex++)
        {
            searchColor = blocks[rowIndex][colIndex + searchIndex].GetComponent<Image>().color;
            if (searchColor != blockScript.colors[0])
            {
                fourBlocks[3] = blocks[rowIndex][colIndex + searchIndex];
                break;
            }
        }

        List<int> colorStatistics = new List<int>();
        //HitInfo hitInfo = HitInfo.MISS;
        int i;
        int j;

        for (i = 0; i < blockScript.colors.Count; i++)
        {
            colorStatistics.Add(0);
        }
        for (i = 0; i < colorStatistics.Count; i++)
        {
            //找十字区域的四个色块是否有相同颜色的色块
            for (j = 0; j < fourBlocks.Count; j++)
            {
                if (blockScript.colors[i] == fourBlocks[j].GetComponent<Image>().color)
                {
                    colorStatistics[i]++;
                }
            }

            //如果存在相同颜色的色块则消除
            if (colorStatistics[i] >= 2)
            {
                //hitInfo = HitInfo.HIT;
                for (j = 0; j < fourBlocks.Count; j++)
                {
                    if (blockScript.colors[i] == fourBlocks[j].GetComponent<Image>().color)
                    {
                        BlockScript script = fourBlocks[j].GetComponent<BlockScript>();
                        blocks[script.rowIndex][script.colIndex].GetComponent<Image>().color = blockScript.colors[0];
                    }
                }
            }
        }
        hasClearArea = ShowNewYearPic();
        //每次消除十字区域的色块时检查一遍年画内的色块是否已消除完毕
        if (hasClearArea == true && countDownBar.value > countDownBar.minValue)
        {
            Instantiate(Resources.Load(ConstLib.VECTORYCANVASP_PATH), eliminateToLeCanvas.transform);
        }
    }

    /// <summary>
    /// 初始化年画图片
    /// </summary>
    void InitNewYearPic()
    {
        //随机加载年画图片
        newYearPic.sprite = manager.GetComponent<UIResource>().RandomNewYearPic();

        RectTransform rect = newYearPic.GetComponent<RectTransform>();
        //随机取年画加载位置所在的列索引
        int col = UnityEngine.Random.Range(newYearPicMixI, newYearPicMaxI);
        //加载年画加载位置的x坐标
        float x = blocks[newYearPicRow][col].transform.position.x;
        float y = rect.position.y;
        //设置年画位置
        rect.position = new Vector3(x,y,0);

        float halfWidth = rect.rect.width / 2;
        float halfHeight = rect.rect.height / 2;
        //获取年画左上角点的坐标
        Vector2 upLeft = new Vector2(rect.position.x - halfWidth, rect.position.y + halfHeight);
        //获取年画右下角点的坐标
        Vector2 downRight = new Vector2(rect.position.x + halfWidth, rect.position.y - halfHeight);
        //遍历所有色块，将位置在年画图片内的色块存储到inPicBlocks列表中
        foreach (List<Block> items in blocks)
        {
            foreach (Block item in items)
            {
                float itemx = item.transform.position.x;
                float itemy = item.transform.position.y;
                if (itemx > upLeft.x && itemx < downRight.x && itemy < upLeft.y && itemy > downRight.y)
                {
                    inPicBlocks.Add(item);
                }
            }
        }
    }

    /// <summary>
    /// 遍历在年画图片内的色块，如果没有色块在图片内了则返回true
    /// </summary>
    /// <returns></returns>
    bool ShowNewYearPic()
    {
        bool showAll = false;
        foreach (Block block in inPicBlocks)
        {
            //判断年画图片内的色块中是否存在又颜色的色块
            if (block.GetComponent<Image>().color != blockScript.colors[0])
            {
                showAll = false;
                break;
            }
            else
            {
                showAll = true;
            }
        }
        return showAll;
    }
}
