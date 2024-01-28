using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitmarker : MonoBehaviour
{
	float lifeTime = 0.3f;
	float creationTime;
    void Start()
    {
		creationTime = Time.time;
		transform.rotation = Quaternion.AngleAxis(Random.value * 360, Vector3.forward);
    }

    void Update()
    {
		transform.rotation = Quaternion.AngleAxis(Random.value * 360, Vector3.forward);

		if (Time.time-creationTime > lifeTime)
		{
			Destroy(gameObject);
		}
	}
}
