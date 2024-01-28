using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterColourer : MonoBehaviour
{
    private GameObject bluewater;
    private GameObject redwater;
    Color blueWaterCol;
    Color redWaterCol;

    void Start()
    {
        bluewater = GameObject.Find("water");
        redwater = GameObject.Find("red_water");
        redWaterCol = redwater.GetComponent<Renderer>().material.color;
        redWaterCol.a = 0.0f;
        redwater.GetComponent<Renderer>().material.color = redWaterCol;

    }

    public void RedWater()
    {
        redWaterCol = redwater.GetComponent<Renderer>().material.color;
        redWaterCol.a = 1.0f;
        redwater.GetComponent<Renderer>().material.color = redWaterCol;

        blueWaterCol = bluewater.GetComponent<Renderer>().material.color;
        blueWaterCol.a = 0.0f;
        bluewater.GetComponent<Renderer>().material.color = blueWaterCol;
    }
}
