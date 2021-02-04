using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BulletMove : MonoBehaviour
{
    public float speed = 2f;

    public Vector2 direction = Vector2.up;
    //public delegate void OnCollisionCallBak(Image bubble,Color bulletColor);
    //public OnCollisionCallBak OnCollision;
    Vector2 pos;

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed, Space.Self);
        BeyondScreen();
    }

    void BeyondScreen()
    {
        pos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (pos.x < 0 || pos.x > 1920 || pos.y < 0 || pos.y > 1080)
        {
            Destroy(gameObject);
        }
    }
}
