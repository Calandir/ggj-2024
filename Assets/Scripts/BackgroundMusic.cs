using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
	[SerializeField]
	private AudioClip m_fishingSong;
	[SerializeField]
	private AudioClip m_combatSong;

	private static BackgroundMusic s_instance = null;

	private void Awake()
	{
		if (s_instance == null)
		{
			s_instance = this;
		}
		else if (s_instance != this)
		{
			Destroy(this);
			return;
		}

		DontDestroyOnLoad(this);

		AudioSource src = GetComponent<AudioSource>();
		src.PlayOneShot(m_fishingSong);

		// Play the combat song after the fishing song.
		// Hardcoding the delay is easier than trimming the mp3.
		src.clip = m_combatSong;
		src.PlayDelayed(38.0f);
	}
}
