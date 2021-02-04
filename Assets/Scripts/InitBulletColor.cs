using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitBulletColor : MonoBehaviour
{
    public List<Color> colors;

    void Start()
    {
        //Image bubble = Resources.Load<Image>("Prefabs/Bubble");
        //BubbleAndBulletScript script = bubble.GetComponent<BubbleAndBulletScript>();
        //colors = script.colors;
    }

    public Color RandomColor()
    {
        return colors[Random.Range(0, colors.Count)];
    }
}
