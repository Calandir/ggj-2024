using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    public GameObject fish;
    [SerializeField] public bool facingRight;
    Vector2 startPosition;

    void Start()
    {
        if (facingRight) {InvokeRepeating("Spawn", 0, 2);}
        else             {InvokeRepeating("Spawn", 1, 2);}
        startPosition = transform.position;
    }

    void Spawn()
    {
        GameObject instantiatedFish = Instantiate(fish, transform.position, Quaternion.identity);
        instantiatedFish.GetComponent<FishMover>().SetDirection(facingRight);
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        if (facingRight) {position.y = startPosition.y + Mathf.Sin(Time.time) * 2;}
        else             {position.y = startPosition.y + Mathf.Cos(Time.time) * 2;}
        
        transform.position = position;
    }
}
