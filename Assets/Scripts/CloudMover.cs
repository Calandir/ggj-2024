using UnityEngine;

public class CloudMover : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] public float speed;

	private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        body.velocity = new UnityEngine.Vector2(UnityEngine.Vector2.right.x * speed, body.velocity.y);
        if (transform.position.x > 18)
        {
            float xPos = transform.position.x;
            xPos -= 102;
            Vector2 newPos = new Vector2(xPos, transform.position.y);
            transform.position = newPos;
        }
    }
}
