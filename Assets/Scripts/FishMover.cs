using System.Collections;
using System.Numerics;
using UnityEngine;

public class FishMover : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] public bool facingRight;
    [SerializeField] public float speed;

    [SerializeField]
    private BoxCollider2D m_boxCollider;

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Fishhook")
        {
            transform.SetParent(other.transform);

            body.isKinematic = true;
            m_boxCollider.enabled = false;

            Fishhook fishhook = other.GetComponent<Fishhook>();

            fishhook.FishingPlayer.fishOnTheLine += 1;  // WOOHOO! FISH ON THE LINE, REEL HER IN!

            // Start on other object so the routine isn't interruptible if this is turned off
            fishhook.FishingPlayer.StartCoroutine(WaitToBeCollected(fishhook.FishingPlayer));
        }
	}

    private IEnumerator WaitToBeCollected(FishingPlayer player)
    {
        yield return new WaitUntil(() => player.CurrentState == FishingPlayer.FishingState.Idle);

        Destroy(gameObject);
    }

	private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        SetDirection(facingRight);
    }
    
    public void SetDirection(bool directionIsRight)
    {if (directionIsRight)
            {
                facingRight = true;
                transform.localScale = new UnityEngine.Vector2(0.7f, 0.7f);
            }
        else
            {
                facingRight = false;
                transform.localScale = new UnityEngine.Vector2(-0.7f, 0.7f);
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
        if (transform.position.x > 15 || transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
}
