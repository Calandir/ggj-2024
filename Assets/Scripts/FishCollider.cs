using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollider : MonoBehaviour
{
	public string team = "Unassigned";

    // Start is called before the first frame update
    void Start()
    {
        
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

				otherRB.AddForce(selfRB.velocity-otherRB.velocity, ForceMode2D.Impulse);

				//A feature for having extra-strong collisions
				if (Time.time - lastStrongCollision > 0.5 && selfRB.velocity.magnitude < otherRB.velocity.magnitude)
				{
					otherRB.AddForce((selfRB.velocity-otherRB.velocity).normalized*500, ForceMode2D.Impulse);
					lastStrongCollision = Time.time;
				}
			}
		}
	}
}
