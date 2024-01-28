using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
	[SerializeField]
    // How long does sun rise take in seconds.
    public float DayCycleTime;
	[SerializeField]
    public float MaxYValue;
	[SerializeField]
    public float MaxXValue;

    private GameObject bluesky;
    private GameObject sunset;
    private GameObject redsky;
    Color blueSkyCol;
    Color sunsetCol;
    Color redSkyCol;
    bool skyIsRed = false;

    void Start()
    {
        bluesky = GameObject.Find("blue_sky");
        sunset = GameObject.Find("sunset_sky");
        redsky = GameObject.Find("red_sky");
        redSkyCol = redsky.GetComponent<Renderer>().material.color;
        redSkyCol.a = 0.0f;
        redsky.GetComponent<Renderer>().material.color = redSkyCol;

    }
    void Update()
    {
        float deltaDistance = transform.position.y + (1.0f / DayCycleTime) * Time.deltaTime;
        float yPos = Mathf.Clamp(deltaDistance, 0.0f, MaxYValue);
        float xPos = Mathf.Clamp(deltaDistance, 0.0f, MaxXValue);
        if (yPos < 10 && !skyIsRed)
        {
            blueSkyCol = bluesky.GetComponent<Renderer>().material.color;
            blueSkyCol.a = yPos / 10;
            bluesky.GetComponent<Renderer>().material.color = blueSkyCol;

            sunsetCol = sunset.GetComponent<Renderer>().material.color;
            sunsetCol.a = 1 - (yPos / 10);
            sunset.GetComponent<Renderer>().material.color = sunsetCol;
            
        }

        transform.position = new Vector3(xPos, yPos, 0.0f);
    }

    void RedSky()
    {
        skyIsRed = true;
        redSkyCol = redsky.GetComponent<Renderer>().material.color;
        redSkyCol.a = 1.0f;
        redsky.GetComponent<Renderer>().material.color = redSkyCol;

        bluesky.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        sunset.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

    void BlueSky()
    {
        skyIsRed = false;
        blueSkyCol = bluesky.GetComponent<Renderer>().material.color;
        blueSkyCol.a = 1.0f;
        bluesky.GetComponent<Renderer>().material.color = blueSkyCol;

        sunset.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        redsky.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

}
