using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.XR;

public class SoftbodyFish : MonoBehaviour
{
	public Vector2 startVelocity, startPosition = Vector2.zero;
	public Quaternion startRotation = Quaternion.identity;

	public enum ControlScheme
	{
		WASD,
		TFGH,
		IJKL,
		Arrows
	}
	public ControlScheme controls = ControlScheme.Arrows;
	KeyCode up, down, left, right;

	Transform[] Bones;

	public Vector2 GetPosition()
	{
		if (initialized)
		{
			return Bones[0].position;
		}
		else return Vector2.zero;
	}

	private bool initialized = false;
	void Start()
	{
		/*
		 * Translates the ControlScheme enum to actual controls.
		 */
		switch (controls)
		{
			case (ControlScheme.WASD): {
				up = KeyCode.W; down = KeyCode.S; left = KeyCode.A; right = KeyCode.D; break;
			}
			case (ControlScheme.TFGH): {
				up = KeyCode.T; down = KeyCode.G; left = KeyCode.F; right = KeyCode.H; break;
			}
			case (ControlScheme.IJKL): {
				up = KeyCode.I; down = KeyCode.K; left = KeyCode.J; right = KeyCode.L; break;
				}
			default: {
				up = KeyCode.UpArrow; down = KeyCode.DownArrow; left = KeyCode.LeftArrow; right = KeyCode.RightArrow; break;
			}
		}

		/*
		 * Softbody physics made by creating a physics object for each bone in the fish sprite, that also deforms the sprite as it moves.
		 * This *should* copy a prefab but it doesn't because bones objects can't be assigned during runtime so it relies on pre-existing bones.
		 */
		Bones = transform.GetComponent<SpriteSkin>().boneTransforms;
		for(int i = 0; i < Bones.Length; i++)
		{
			Transform Bone = Bones[i];
			Rigidbody2D rb = Bone.AddComponent<Rigidbody2D>();
			rb.gravityScale = 0.5f;
			rb.drag = 0.1f;
			rb.mass = Mathf.Lerp(1, 0.3f, i / Bones.Length); //Fish gets lighter towards the tail
			if (i > 0)
			{
				HingeJoint2D hinge = Bone.AddComponent<HingeJoint2D>();
				hinge.connectedBody = Bones[i - 1].GetComponent<Rigidbody2D>();
				JointAngleLimits2D limits = new()
				{
					min = -60,
					max = 60
				};
				hinge.limits = limits;
				hinge.useLimits = true;
			}
			
			CircleCollider2D collider = Bone.AddComponent<CircleCollider2D>();
			collider.radius = 0.5f;
			collider.isTrigger = true;

			Bone.AddComponent<FishCollider>().team = transform.parent.name;
		}

		Bones[0].GetComponent<Rigidbody2D>().velocity = startVelocity;
		Bones[0].position = startPosition;
		Bones[0].rotation = startRotation;

		initialized = true;
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 aim = new();
		if (Input.GetKey(up))
		{
			aim += new Vector2(0, 1);
		}
		if (Input.GetKey(down))
		{
			aim += new Vector2(0, -1);
		}
		if (Input.GetKey(left))
		{
			aim += new Vector2(-1, 0);
		}
		if (Input.GetKey(right))
		{
			aim += new Vector2(1, 0);
		}
		aim.Normalize();

		Rigidbody2D headRB = Bones[0].GetComponent<Rigidbody2D>();
		float thrustFactor = 20;
		if ((headRB.velocity + aim).magnitude < headRB.velocity.magnitude)
		{
			thrustFactor *= 3;
		}
		if (headRB.velocity.magnitude < 10)
		{
			thrustFactor *= 3;
		}
		for (int i = 0; i < Bones.Length; i++)
		{
			Rigidbody2D boneRB = Bones[i].GetComponent<Rigidbody2D>();
			//boneRB.AddForce(aim * thrustFactor * boneRB.mass * Mathf.Lerp(1, -0.5f, i / Bones.Length));
		}

		headRB.AddForce(aim*thrustFactor);
	}
}
