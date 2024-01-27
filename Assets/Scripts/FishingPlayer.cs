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

	// Cast power between 0 - 100.
	public float CastPower = 0;
	// How fast it takes to charge max power in seconds.
	public float CastChargeSpeed;
	// Store space bar state.
	private bool spaceBarDown = false;

	public enum FishingState
	{
		ChargeCast,
		Cast,
		Sinking,
		Reel,
	}

	private FishingState m_currentState = FishingState.ChargeCast;

	private void Update()
	{
		if (m_currentState == FishingState.ChargeCast)
		{
			if (Input.GetKeyDown(KeyCode.Space)) {
				spaceBarDown = true;
			} else if (Input.GetKeyUp(KeyCode.Space)) {
				// When space bar is released, cast rod
				spaceBarDown = false;
				m_currentState = FishingState.Cast;
			}

			if (spaceBarDown) {
				// While space bar is held down, increase cast power.
				CastPower += 1.0f / (CastChargeSpeed / 100.0f) * Time.deltaTime;
				CastPower = Mathf.Clamp(CastPower, 0.0f, 100.0f);

			}
		}
		else if (m_currentState == FishingState.Cast)
		{

		}
		else if (m_currentState == FishingState.Sinking)
		{

		}
		else if (m_currentState == FishingState.Reel)
		{

		}
	}
}
