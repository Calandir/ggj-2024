using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishhook : MonoBehaviour
{
    public bool IsReeling => m_isReeling;

    public FishingPlayer FishingPlayer => m_fishingPlayer;

	public float WaterLevel;

	[SerializeField]
    private FishingPlayer m_fishingPlayer;

    [SerializeField]
    private Rigidbody2D m_rigidbody;

    [SerializeField]
    private Transform m_reelDestination;

	[SerializeField]
	private float m_fishhookReelSpeedPerSecond = 1.0f;

    private bool m_isReeling = false;
	private bool m_isAirBorn = true;

    private float m_currentAngleRadians;
    private float m_hypotenuse;
    private bool m_sinkClockwise;

    private float m_timeSinceLastSink;

	public void DropAt(Vector2 position, bool sinkClockWise, Vector2 velocity = default(Vector2))
    {
        transform.position = position;

        m_sinkClockwise = sinkClockWise;

        m_rigidbody.isKinematic = false;
        m_rigidbody.velocity = velocity;

		// !! Assumption is made here that hook is dropped above water line
		m_isAirBorn = true;
    }

    public void LerpToReelDestination()
    {
		Vector2 distanceToTravel = (Vector2)transform.position - (Vector2)m_reelDestination.position;

        float totalTravelTime = distanceToTravel.magnitude / m_fishhookReelSpeedPerSecond;

        StartCoroutine(ReelRoutine(Time.time, totalTravelTime, transform.position, m_reelDestination.position));
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

        SFXSingleton.Instance.PlaySplashSFX();
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
            m_rigidbody.isKinematic = true;

			m_isAirBorn = false;

            // Store starting angle and distance from rod
			float yOffset = transform.position.y - m_reelDestination.position.y;
			float xOffset = transform.position.x - m_reelDestination.position.x;

			m_currentAngleRadians = Mathf.Atan2(yOffset, xOffset);
            m_hypotenuse = new Vector2(xOffset, yOffset).magnitude;

            m_timeSinceLastSink = 0.0f;
		}

        bool isSinking = !m_isAirBorn && !m_isReeling;
        if (!isSinking)
        {
            return;
        }

        bool continueRotating =
            (m_sinkClockwise && transform.position.x > m_reelDestination.position.x)
            || (!m_sinkClockwise && transform.position.x < m_reelDestination.position.x);
        if (continueRotating)
        {
			float direction = m_sinkClockwise ? -1 : 1;
			m_currentAngleRadians += 0.5f * direction * Time.deltaTime;
		}

        float newYOffset = Mathf.Sin(m_currentAngleRadians) * m_hypotenuse;
        float newXOffset = Mathf.Cos(m_currentAngleRadians) * m_hypotenuse;

        m_timeSinceLastSink += Time.deltaTime;
        m_timeSinceLastSink = Mathf.Clamp(m_timeSinceLastSink, 0.0f, 5.0f);

        float extraYOffset = -3.0f * (m_timeSinceLastSink / 5);

        transform.position = (Vector2)m_reelDestination.position + new Vector2(newXOffset, newYOffset + extraYOffset);
	}
}
