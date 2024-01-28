using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_creditsParent;

	[SerializeField]
	private GameObject m_eventSystemObj;

	private static GameObject s_eventSystemObj = null;

	private void Start()
	{
		if (s_eventSystemObj == null)
		{
			DontDestroyOnLoad(m_eventSystemObj);

			s_eventSystemObj = m_eventSystemObj;
		}
		else
		{
			Destroy(m_eventSystemObj);
		}

		ToggleCredits(show: false);
	}

	public void ToggleCredits(bool show)
    {
        m_creditsParent.SetActive(show);
    }
}
