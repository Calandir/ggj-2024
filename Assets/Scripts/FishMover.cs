using System.Numerics;
using UnityEngine;

public class FishMover : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] public bool facingRight;
    [SerializeField] public float speed;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        if (facingRight == false)
            {
                transform.localScale = new UnityEngine.Vector2(-1, 1);
            }
        else
            {
                transform.localScale = new UnityEngine.Vector2(1, 1);
            }
    }
    
    private void FixedUpdate()
    {
        if (facingRight)
        {
            body.velocity = new UnityEngine.Vector2(UnityEngine.Vector2.right.x * speed, body.velocity.y);
        }
        else
        {
            body.velocity = new UnityEngine.Vector2(UnityEngine.Vector2.left.x * speed, body.velocity.y);
        }     
    }
}
