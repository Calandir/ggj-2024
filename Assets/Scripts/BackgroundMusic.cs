using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
	[SerializeField]
	private AudioClip m_fishingSong;
	[SerializeField]
	private AudioClip m_combatSong;

	private void Awake()
	{
		AudioSource src = GetComponent<AudioSource>();
		src.PlayOneShot(m_fishingSong);

		// Play the combat song after the fishing song.
		// Hardcoding the delay is easier than trimming the mp3.
		src.clip = m_combatSong;
		src.PlayDelayed(38.0f);
	}
}
