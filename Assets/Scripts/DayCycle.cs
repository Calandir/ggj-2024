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

    void Update()
    {
        float deltaDistance = transform.position.y + (1.0f / DayCycleTime) * Time.deltaTime;
        float yPos = Mathf.Clamp(deltaDistance, 0.0f, MaxYValue);
        float xPos = Mathf.Clamp(deltaDistance, 0.0f, MaxXValue);

        transform.position = new Vector3(xPos, yPos, 0.0f);
    }
}
