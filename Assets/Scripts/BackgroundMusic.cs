using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
	[SerializeField]
	private AudioClip m_song;

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
		src.clip = m_song;
		src.Play();
	}
}
