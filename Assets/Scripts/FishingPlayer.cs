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
	public FishingState m_currentState{
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

	[SerializeField]
	public int fishOnTheLine = 0;

	public SpriteRenderer fishPileRenderer;
	public Sprite
		fishBasket0,
		fishBasket1,
		fishBasket2,
		fishBasket3,
		fishBasket4,
		fishBasket5,
		fishBasket6,
		fishBasket7,
		fishBasket8,
		fishBasket9;

	private Sprite[] fishSprites;

	private int __fishTotal = 0;
	[SerializeField]
	public int fishTotal {
		get
		{
			return __fishTotal;
		}
		set
		{
			// When fish total is set, check new value to update fish pile sprite.
			this.__fishTotal = value;
			// Assign baskets at an ever increasing difference of fish given by fibonacci sequence.
			int a = 0, b = 1;
			int i = 0;
			for (i = 0; i < 10; i++) {
				int c = a + b; 
				if (this.__fishTotal < c) {
					break;
				}
				a = b;
				b = c;
			}
			fishPileRenderer.sprite = fishSprites[i];
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

		fishSprites = new Sprite[10]{fishBasket0, fishBasket1, fishBasket2, fishBasket3, fishBasket4, fishBasket5, fishBasket6, fishBasket7, fishBasket8, fishBasket9};

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
		else if (m_currentState == FishingState.ChargeCast)
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

			// Magic value so rod hook is not cast into space.
			float scaledPower = CastPower / 12.0f;
			float xDirection = m_fishhookDropLocation.x < 0 ? scaledPower : -scaledPower;
			Vector2 velocity = new Vector2(xDirection, scaledPower);

			bool sinkClockwise = m_playerNumber == 1 ? true : false;

			m_fishhook.DropAt(m_fishhookDropLocation, sinkClockwise, velocity);
			
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
				fishTotal += fishOnTheLine;
				fishOnTheLine = 0;
				Debug.Log(string.Format("player {0} fish total: {1}", m_playerNumber, fishTotal));

				m_fishhook.gameObject.SetActive(false);
				CastPower = 0;
				m_currentState = FishingState.Idle;
			}
		}
	}
}
