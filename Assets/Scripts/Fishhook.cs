using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishhook : MonoBehaviour
{
    public bool IsReeling => m_isReeling;

    public FishingPlayer FishingPlayer => m_fishingPlayer;

	private float WaterLevel;

	[SerializeField]
    private FishingPlayer m_fishingPlayer;

    [SerializeField]
    private Rigidbody2D m_rigidbody;

    [SerializeField]
    private Vector2 m_reelDestination;

	[SerializeField]
	private float m_fishhookReelSpeedPerSecond = 1.0f;

    private bool m_isReeling = false;
	private bool m_isAirBorn = true;

	public void DropAt(Vector2 position, Vector2 velocity = default(Vector2))
    {
        transform.position = position;

        m_rigidbody.velocity = velocity;

		// !! Assumption is made here that hook is dropped above water line
		m_isAirBorn = true;
    }

    public void LerpToReelDestination()
    {
		Vector2 distanceToTravel = (Vector2)transform.position - m_reelDestination;

        float totalTravelTime = distanceToTravel.magnitude / m_fishhookReelSpeedPerSecond;

        StartCoroutine(ReelRoutine(Time.time, totalTravelTime, transform.position, m_reelDestination));
    }

    private IEnumerator ReelRoutine(float startTime, float reelTime, Vector2 start, Vector2 finish)
    {
        float finishTime = startTime + reelTime;

        m_isReeling = true;
		m_rigidbody.isKinematic = true;

		while (Time.time < finishTime)
        {
            float progress = Easing((Time.time - startTime) / reelTime);

            transform.position = Vector2.Lerp(start, finish, progress);

            yield return null;
        }

        m_isReeling = false;
		m_rigidbody.isKinematic = false;
	}

    private float Easing(float number)
    {
		return Mathf.Sqrt(1 - Mathf.Pow(number - 1, 2));
	}

	private void Update()
	{
		if (m_isAirBorn && transform.position.y < WaterLevel) {
			// When hook hits water, stop it.
        	m_rigidbody.velocity = new Vector2(0.0f, 0.0f);
			m_isAirBorn = false;
		}
	}
}
