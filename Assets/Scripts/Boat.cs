using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
	[SerializeField]
	private FishingPlayer m_player;

	[SerializeField]
	private string m_collisionParentName = "FishesFromLeft";

	[SerializeField]
	private Sprite[] m_damageSprites;

	[SerializeField]
	private SpriteRenderer m_spriteRenderer;

	[SerializeField]
	private int m_health = 12;

	[SerializeField]
	private Vector2 m_offsetWhenDamaged = new Vector2(1.0f, -0.5f);

	[SerializeField]
	private float m_damageAnimationTimeSeconds = 0.5f;

	private Coroutine m_showDamageRoutine = null;
	private float m_lastShowDamageStartTime;

	private Vector2 m_basePosition;

	private float m_yBob;

	private void Start()
	{
		m_basePosition = transform.position;
	}

	private void Update()
	{
		m_yBob = Mathf.Sin(Time.time / 1.5f) * 0.175f;
		
		if (m_showDamageRoutine == null)
		{
			transform.position = m_basePosition + new Vector2(0.0f, m_yBob);
		}
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.parent.parent.name.ToLower() == m_collisionParentName.ToLower())
		{
			Destroy(collision.transform.parent.gameObject);

			m_player.ShowDamageSprite();

			if (m_showDamageRoutine == null)
			{
				m_health -= 1;
				m_health = Mathf.Clamp(m_health, 0, int.MaxValue);

				int spriteIndex = Mathf.RoundToInt(m_health / m_damageSprites.Length);

				m_spriteRenderer.sprite = m_damageSprites[spriteIndex];

				m_showDamageRoutine = StartCoroutine(ShowDamage());
			}
		}
	}

	private IEnumerator ShowDamage()
	{
		m_lastShowDamageStartTime = Time.time;

		float showDamageFinishTime = m_lastShowDamageStartTime + m_damageAnimationTimeSeconds;

		while (Time.time < showDamageFinishTime)
		{
			// 0 to 1
			float progress = (Time.time - m_lastShowDamageStartTime) / m_damageAnimationTimeSeconds;
			
			// 0 to 2
			progress *= 2;

			// 0 to 1 to 0, so the lerp can undo
			if (progress > 1.0f)
			{
				progress = 2 - progress;
			}

			float progressEased = (float)easeOutBounce(progress);

			transform.position = new Vector2(0.0f, m_yBob) + Vector2.Lerp(m_basePosition, m_basePosition + m_offsetWhenDamaged, progressEased);

			yield return null;
		}

		m_showDamageRoutine = null;
	}

	private float easeInOutBounce(float number)
	{
		return 0.0f;
		//return x < 0.5
		//  ? (1 - easeOutBounce(1 - 2 * x)) / 2
		//  : (1 + easeOutBounce(2 * x - 1)) / 2;
	}

	private double easeOutBounce(double x)
	{
		const double n1 = 7.5625f;
		const double d1 = 2.75f;

		if (x < 1 / d1)
		{
			return n1* x * x;
		}
		else if (x < 2 / d1)
		{
			return n1 * (x -= 1.5 / d1) * x + 0.75;
		}
		else if (x < 2.5 / d1)
		{
			return n1 * (x -= 2.25 / d1) * x + 0.9375;
		}
		else
		{
			return n1 * (x -= 2.625 / d1) * x + 0.984375;
		}
	}
}