using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollider : MonoBehaviour
{
	public string team = "Unassigned";
	public Transform Head;
	GameObject HitMarkerTemplate;

    // Start is called before the first frame update
    void Start()
    {
		HitMarkerTemplate = Resources.Load("HitMarker") as GameObject;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	float lastStrongCollision = -1;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Transform otherTransform = collision.GetComponentInParent <Transform>();
		FishCollider CollideScript = otherTransform.GetComponent<FishCollider>();
		if (CollideScript)
		{
			if (CollideScript.team != team)
			{
				Rigidbody2D selfRB = transform.GetComponent<Rigidbody2D>();
				Rigidbody2D otherRB = otherTransform.GetComponent<Rigidbody2D>();

				//A feature for having extra-strong collisions
				if (Time.time - lastStrongCollision > 0.5 && selfRB.velocity.magnitude < otherRB.velocity.magnitude)
				{
					otherRB.AddForce((CollideScript.Head.position-Head.position).normalized*100, ForceMode2D.Impulse);
					Instantiate(HitMarkerTemplate, transform.position, Quaternion.identity);
					lastStrongCollision = Time.time;
				}
				else
				{
					otherRB.AddForce((selfRB.velocity - otherRB.velocity)*0.3f, ForceMode2D.Impulse);
				}
			}
		}
	}
}
