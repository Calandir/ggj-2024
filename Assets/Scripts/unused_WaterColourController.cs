using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterColourController : MonoBehaviour
{
    Sprite redWater;
    void RedWater()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/red_water");
    }

    void BlueWater()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/water");
    }
}
