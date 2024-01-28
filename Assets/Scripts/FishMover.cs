using System.Collections;
using System.Numerics;
using UnityEngine;

public class FishMover : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] public bool facingRight;
    [SerializeField] public float speed;

    private float wiggleSpeed = 0.4f;
    private float wiggleAmount = 0.1f;

    [SerializeField]
    private BoxCollider2D m_boxCollider;

    private bool m_isHooked = false;

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Fishhook")
        {
            m_isHooked = true;

            transform.SetParent(other.transform);

            Destroy(body);

            m_boxCollider.enabled = false;

            Fishhook fishhook = other.GetComponent<Fishhook>();

            fishhook.FishingPlayer.fishOnTheLine += 1;  // WOOHOO! FISH ON THE LINE, REEL HER IN!
            SFXSingleton.Instance.PlayLineStretchSFX();

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
        InvokeRepeating("Wiggle", 0, wiggleSpeed);
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
    private void Wiggle()
    {
        if (m_isHooked) {
            return;
        }
        float randX = UnityEngine.Random.Range(wiggleAmount * -1, wiggleAmount);
        float randY = UnityEngine.Random.Range(wiggleAmount * -1, wiggleAmount);
        randX += transform.position.x;
        if (transform.position.y > -0.5) {randY = -0.1f;}
        else if (transform.position.y < -7.5) {randY = 0.1f;}
        randY += transform.position.y;
        
        transform.position = new UnityEngine.Vector2(randX, randY);
    }
    private void FixedUpdate()
    {
        if (m_isHooked)
        {
            return;
        }

        if (facingRight)
        {
            body.velocity = new UnityEngine.Vector2(UnityEngine.Vector2.right.x * speed, body.velocity.y);
        }
        else
        {
            body.velocity = new UnityEngine.Vector2(UnityEngine.Vector2.left.x * speed, body.velocity.y);
        }     
        if (transform.position.x > 18 || transform.position.x < -18)
        {
            Destroy(gameObject);
        }
    }

    public void SetFight()
    {
        CancelInvoke();
        wiggleSpeed = 0.05f;
        wiggleAmount = 0.1f;
        InvokeRepeating("Wiggle", 0, wiggleSpeed);
    }
}
