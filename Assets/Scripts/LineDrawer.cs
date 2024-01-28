using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer m_lineRenderer;

	[SerializeField]
	private Transform m_castFromPosition;

	[SerializeField]
	private Transform m_fishhook;

	private Vector3[] m_positions = new Vector3[2];

	private void Update()
	{
		// Hide if the fishook is hidden
		if (!m_fishhook.gameObject.activeInHierarchy)
		{
			m_positions[0] = Vector3.zero;
			m_positions[1] = Vector3.zero;
		}
		else
		{
			m_positions[0] = m_castFromPosition.localPosition;
			m_positions[1] = m_fishhook.localPosition;
		}

		m_lineRenderer.SetPositions(m_positions);
	}
}
