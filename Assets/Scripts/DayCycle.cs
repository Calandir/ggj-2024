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

    void Update()
    {
        float yPos = transform.position.y + (1.0f / DayCycleTime) * Time.deltaTime;
        yPos = Mathf.Clamp(yPos, 0.0f, MaxYValue);

        transform.position = new Vector3(0.0f, yPos, 0.0f);
    }
}
