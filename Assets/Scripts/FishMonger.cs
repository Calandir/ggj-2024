using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMonger : MonoBehaviour
{
	public Camera activeCamera;
	public GameObject FishTemplate;

	Transform RedFish, BlueFish;

    void Start()
	{
		RedFish = transform.Find("RedFish");
		BlueFish = transform.Find("BlueFish");

		launchBlueFish();
		launchRedFish();
	}

	void Update()
	{

	}
	void launchBlueFish()
	{
		launchFish(BlueFish, -30, 50, false, SoftbodyFish.ControlScheme.WASD);
	}

	void launchRedFish()
	{
		launchFish(RedFish, -150, 50, false, SoftbodyFish.ControlScheme.Arrows);
	}

	void launchFish(Transform parent, float angle, float speed, bool flipped, SoftbodyFish.ControlScheme controls)
	{
		GameObject Fish = Instantiate(FishTemplate, parent);
		SoftbodyFish FishScript = Fish.GetComponent<SoftbodyFish>();

		Fish.GetComponent<SpriteRenderer>().flipY = flipped;

		FishScript.controls = controls;
		FishScript.startPosition = parent.position;
		FishScript.startRotation = Quaternion.AngleAxis(angle, Vector3.forward);
		FishScript.startVelocity = vecFromAngle(angle)*speed;
	}

	void removeOutOfBounds(GameObject Fish)
	{
		Vector2 position = Fish.GetComponent<SoftbodyFish>().GetPosition();
		position = activeCamera.WorldToScreenPoint(position);

		float buffer = 0.2f;
		if(position.x < -buffer || position.x > 1+buffer || position.y < buffer || position.y > 1 + buffer)
		{
			Destroy(Fish);
		}
	}

	Vector2 vecFromAngle(float deg)
	{
		float angle = Mathf.Deg2Rad*deg;
		return new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle));
	}
}
