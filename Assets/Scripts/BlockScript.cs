﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// 色块类，用于存储色块的部分信息
/// </summary>
public class BlockScript : MonoBehaviour
{
    public List<Color> colors;
    public delegate void Clicked(int rowIndex,int colIndex);
    public Clicked ClearCrossArea;

    public int rowIndex;
    public int colIndex;

    public void SetIndex(int rowIndex,int colIndex)
    {
        this.rowIndex = rowIndex;
        this.colIndex = colIndex;
    }

    public void Click()
    {
        if (this.GetComponent<Image>().color == colors[0])
        {
            ClearCrossArea(rowIndex,colIndex);
        }
    }
}