using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitmarker : MonoBehaviour
{
	float lifeTime = 0.2f;
	float creationTime;
    void Start()
    {
		creationTime = Time.time;
		transform.rotation = Quaternion.AngleAxis(Random.value * 360, Vector3.forward);
    }

    void Update()
    {
		if (Random.value < 0.1)
		{
			transform.rotation = Quaternion.AngleAxis(Random.value * 360, Vector3.forward);
		}
		if (Time.time-creationTime > lifeTime)
		{
			Destroy(gameObject);
		}
	}
}
