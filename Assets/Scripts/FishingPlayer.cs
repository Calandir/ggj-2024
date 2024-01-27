using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Text;

public class FishingPlayer : MonoBehaviour
{
	[SerializeField]
	private int m_playerNumber = 1;

	[SerializeField]
	private Fishhook m_fishhook;

	[SerializeField]
	private Vector2 m_fishhookDropLocation;

	// Cast power between 0 - 100.
	public float CastPower = 0;
	// How fast it takes to charge max power in seconds.
	public float CastChargeSpeed;

	public enum FishingState
	{
		ChargeCast,
		Cast,
		Sinking,
		Reel,
	}

	private FishingState m_currentState = FishingState.ChargeCast;

	private void Start()
	{
		m_fishhook.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (m_currentState == FishingState.ChargeCast)
		{
			if (Input.GetKeyUp(KeyCode.Space)) {
				// When space bar is released, cast rod
				m_currentState = FishingState.Cast;
			}

			if (Input.GetKey(KeyCode.Space)) {
				// While space bar is held down, increase cast power.
				CastPower += 1.0f / (CastChargeSpeed / 100.0f) * Time.deltaTime;
				CastPower = Mathf.Clamp(CastPower, 0.0f, 100.0f);
			}
		}
		else if (m_currentState == FishingState.Cast)
		{
			m_fishhook.gameObject.SetActive(true);
			m_fishhook.DropAt(m_fishhookDropLocation);
			
			m_currentState = FishingState.Sinking;
		}
		else if (m_currentState == FishingState.Sinking)
		{
			// TODO wait for reel input

			m_currentState = FishingState.Reel;
		}
		else if (m_currentState == FishingState.Reel)
		{
			// TODO wait for reel animation

			m_currentState = FishingState.ChargeCast;
		}
	}
}
