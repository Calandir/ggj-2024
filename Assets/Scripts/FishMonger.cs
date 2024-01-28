using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMonger : MonoBehaviour
{
	public Camera activeCamera;
	public GameObject FishTemplateA;
	public GameObject FishTemplateB;

	[SerializeField]
	private bool m_throwFishOnStart = true;

	Transform RedFish, BlueFish;

    void Start()
	{
		RedFish = transform.Find("RedFish");
		BlueFish = transform.Find("BlueFish");

		if (m_throwFishOnStart)
		{
			launchBlueFish();
			launchRedFish();
		}
	}

	void Update()
	{
		foreach (Transform Fish in RedFish.transform)
		{
			removeOutOfBounds(Fish);
		}
		foreach (Transform Fish in BlueFish.transform)
		{
			removeOutOfBounds(Fish);
		}
	}
	
	public void launchBlueFish()
	{
		launchFish(BlueFish, -30, 50, false, SoftbodyFish.ControlScheme.WASD);
	}

	public void launchRedFish()
	{
		launchFish(RedFish, -150, 50, true, SoftbodyFish.ControlScheme.Arrows);
	}

	void launchFish(Transform parent, float angle, float speed, bool flipped, SoftbodyFish.ControlScheme controls)
	{
		GameObject Fish;

		if (flipped)
		{
			Fish = Instantiate(FishTemplateB, parent);
		}
        else
        {
			Fish = Instantiate(FishTemplateA, parent);
		}

        SoftbodyFish FishScript = Fish.GetComponent<SoftbodyFish>();

		FishScript.controls = controls;
		FishScript.startPosition = parent.position;
		FishScript.startRotation = Quaternion.AngleAxis(angle, Vector3.forward);
		FishScript.startVelocity = vecFromAngle(angle)*speed;
	}

	void removeOutOfBounds(Transform Fish)
	{
		Vector2 position = Fish.GetComponent<SoftbodyFish>().GetPosition();
		position = activeCamera.WorldToViewportPoint(position);

		float buffer = 0.2f;
		if(position.x < -buffer || position.x > 1+buffer || position.y < 0 -buffer || position.y > 1 + buffer)
		{
			Destroy(Fish.gameObject);
		}
	}

	Vector2 vecFromAngle(float deg)
	{
		float angle = Mathf.Deg2Rad*deg;
		return new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle));
	}
}
