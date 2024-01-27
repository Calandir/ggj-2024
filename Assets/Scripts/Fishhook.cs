using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishhook : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_rigidbody;

    public void DropAt(Vector2 position)
    {
        transform.position = position;

        m_rigidbody.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
