using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResource : MonoBehaviour
{
    public List<Sprite> newYearPicList = new List<Sprite>();

    public List<Sprite> bubbleSprites = new List<Sprite>();

    public Dictionary<Sprite, Sprite> picDic = new Dictionary<Sprite, Sprite>();

    private void Awake()
    {
        for (int i = 0; i < newYearPicList.Count; i++)
        {
            picDic.Add(newYearPicList[i], bubbleSprites[i]);
        }
        
    }

    public Sprite RandomNewYearPic()
    {
        return newYearPicList[Random.Range(0, newYearPicList.Count)];
    }

    public Sprite RandomBubbleSprite()
    {
        return bubbleSprites[Random.Range(0, bubbleSprites.Count)];
    }
}
