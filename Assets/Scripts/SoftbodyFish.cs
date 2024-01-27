using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class SoftbodyFish : MonoBehaviour
{
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
			rb.gravityScale = 0;
			rb.drag = 0.1f;
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
		}
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

		Transform Bone = Bones[0];
		Bone.GetComponent<Rigidbody2D>().AddForce(aim*5);
	}
}
