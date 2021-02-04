using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleAndBulletScript : MonoBehaviour
{
    public delegate void OnTriggerEnterCallBack(Image bullet,Image bubble);

    public OnTriggerEnterCallBack OnTrigger;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BulletMove>() == null)
        {
            return;
        }
        Sprite other = collision.gameObject.GetComponent<Image>().sprite;
        Sprite self = gameObject.GetComponent<Image>().sprite;
        Image bubble = gameObject.GetComponent<Image>();
        Image bullet = collision.gameObject.GetComponent<Image>();
        if (self.name == other.name)
        {
            OnTrigger(bullet,bubble);
        }
    }
}
