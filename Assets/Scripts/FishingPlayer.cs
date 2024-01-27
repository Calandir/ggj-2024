using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Text;

public class FishingPlayer : MonoBehaviour
{
	public FishingState CurrentState => m_currentState;

	[SerializeField]
	private int m_playerNumber = 1;

	[SerializeField]
	private Fishhook m_fishhook;

	[SerializeField]
	private Vector2 m_fishhookDropLocation;

	[SerializeField]
	private SpriteRenderer spriteRenderer;

	// Cast power between 0 - 100.
	public float CastPower = 0;
	// How fast it takes to charge max power in seconds.
	public float CastChargeSpeed;

	public enum FishingState
	{
		Idle,
		ChargeCast,
		Cast,
		Sinking,
		Reel,
	}


	[SerializeField]
	private Sprite blueFisherman, blueFishermanCast;

	private Dictionary<FishingState, Sprite> spriteMap;

	private FishingState __m_currentState = FishingState.Idle;
	private FishingState m_currentState{
		get
		{
			return __m_currentState;
		}
		set
		{
			// When state is set, update sprite to reflect new state.
			this.__m_currentState = value;
			spriteRenderer.sprite = spriteMap[this.__m_currentState];
		}
	}

	private KeyCode m_inputKeyCode;

	private void Start()
	{
		m_fishhook.gameObject.SetActive(false);

		spriteMap = new Dictionary<FishingState, Sprite>(){
			{FishingState.Idle, blueFisherman},
			{FishingState.ChargeCast, blueFishermanCast},
			{FishingState.Cast, blueFisherman},
			{FishingState.Sinking, blueFisherman},
			{FishingState.Reel, blueFisherman}
		};
		spriteRenderer.sprite = spriteMap[m_currentState];

		m_inputKeyCode = m_playerNumber == 1 ? KeyCode.LeftShift : KeyCode.RightShift;
	}

	private void Update()
	{
		if (m_currentState == FishingState.Idle)
		{
			if (Input.GetKeyDown(m_inputKeyCode)) {
				m_currentState = FishingState.ChargeCast;
			}
		}
		if (m_currentState == FishingState.ChargeCast)
		{
			if (Input.GetKeyUp(m_inputKeyCode)) {
				// When key is released, cast rod
				m_currentState = FishingState.Cast;
			}

			if (Input.GetKey(m_inputKeyCode)) {
				// While key is held down, increase cast power.
				CastPower += 1.0f / (CastChargeSpeed / 100.0f) * Time.deltaTime;
				CastPower = Mathf.Clamp(CastPower, 0.0f, 100.0f);
			}
		}
		else if (m_currentState == FishingState.Cast)
		{
			m_fishhook.gameObject.SetActive(true);
			m_fishhook.DropAt(m_fishhookDropLocation);
			
			m_currentState = FishingState.Sinking;

			return;
		}
		else if (m_currentState == FishingState.Sinking)
		{
			if (Input.GetKeyDown(m_inputKeyCode))
			{
				m_fishhook.LerpToReelDestination();

				m_currentState = FishingState.Reel;
			}
		}
		else if (m_currentState == FishingState.Reel)
		{
			if (!m_fishhook.IsReeling)
			{
				// Finished reeling
				m_fishhook.gameObject.SetActive(false);

				m_currentState = FishingState.Idle;
			}
		}
	}
}