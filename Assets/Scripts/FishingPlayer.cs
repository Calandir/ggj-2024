using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPlayer : MonoBehaviour
{
	[SerializeField]
	private int m_playerNumber = 1;

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
			// TODO Antony's bar work :D
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
