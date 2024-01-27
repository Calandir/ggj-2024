using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D m_collider;

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "Wall")
		{
			Debug.LogError("Collided");
		}
	}
}
