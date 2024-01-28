using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_creditsParent;

	private void Start()
	{
		ToggleCredits(show: false);
	}

	public void ToggleCredits(bool show)
    {
        m_creditsParent.SetActive(show);
    }
}
