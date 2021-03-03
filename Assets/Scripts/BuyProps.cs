using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuyProps : MonoBehaviour
{
    Button btnBuyBoom;
    Button btnBuyTineAdd;
    Canvas shopCanvas;
    CanvasManager manager;

    const string txt = "积分";
    public int buyBoomIntegral = 50;
    public int buyTimeAddIntegral = 60;
    // Start is called before the first frame update
    void Start()
    {
        shopCanvas = GameObject.Find(ConstLib.SHOPCANVAS).GetComponent<Canvas>();
        manager = GameObject.Find(ConstLib.CANVASMANAGER).GetComponent<CanvasManager>();
        try
        {
            btnBuyBoom = shopCanvas.transform.Find(ConstLib.BTNBUYBOOM).GetComponent<Button>();
            btnBuyTineAdd = shopCanvas.transform.Find(ConstLib.BTNBUYTIMEADD).GetComponent<Button>();
        }
        catch (ArgumentNullException e)
        {
            Debug.LogError(e);
            throw;
        }

        SetConsumptionIntegral();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (gameObject.name == ConstLib.BTNSHOP)
        {
            manager.Push(shopCanvas);
        }
        else if (gameObject.name == ConstLib.BTNBUYBOOM)
        {
            if (ConstLib.Integral >= buyBoomIntegral)
            {
                ConstLib.Integral -= buyBoomIntegral;
                ConstLib.WriteIntegral();
                PlayerState.prop.BoomNum++;
                PlayerState.SetBoomNum();
                //Text txt = Instantiate(Resources.Load<Text>(ConstLib.PREFAB_TIG), shopCanvas.transform);
                //txt.text = Convert.ToString("购买成功");
                ToolLib.Tig(shopCanvas.transform, "购买成功");
            }            
        }
        else if (gameObject.name == ConstLib.BTNBUYTIMEADD)
        {
            if (ConstLib.Integral >= buyTimeAddIntegral)
            {
                ConstLib.Integral -= buyTimeAddIntegral;
                ConstLib.WriteIntegral();
                PlayerState.prop.TimeAddNum++;
                PlayerState.SetTimeAddNum();
                //Text txt = Instantiate(Resources.Load<Text>(ConstLib.PREFAB_TIG), shopCanvas.transform);
                //txt.text = Convert.ToString("购买成功");

                ToolLib.Tig(shopCanvas.transform, "购买成功");
            }          
        }
    }

    void SetConsumptionIntegral()
    {
        btnBuyBoom.transform.Find("Image/ConsumptionIntegral").GetComponent<Text>().text = buyBoomIntegral + txt;
        btnBuyTineAdd.transform.Find("Image/ConsumptionIntegral").GetComponent<Text>().text = buyTimeAddIntegral + txt;
    }
}
