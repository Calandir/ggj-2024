using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    public GameObject fish;
    [SerializeField] public bool facingRight;
    Vector2 startPosition;

    void Start()
    {
        InvokeRepeating("Spawn", 0, 2);
        startPosition = transform.position;
    }

    void Spawn()
    {
        GameObject instantiatedFish = Instantiate(fish, transform.position, Quaternion.identity);
        instantiatedFish.GetComponent<FishMover>().facingRight = facingRight;
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.y = startPosition.y + Mathf.Sin(Time.time) * 2;
        transform.position = position;
    }
}
