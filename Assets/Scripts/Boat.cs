using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
	[SerializeField]
	private string m_collisionParentName = "FishesFromLeft";

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.parent.parent.name.ToLower() == m_collisionParentName.ToLower())
		{
			// TODO
		}
	}

}