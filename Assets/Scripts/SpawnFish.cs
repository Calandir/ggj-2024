using System;
using UnityEngine;

public class SpawnFish : MonoBehaviour
{
    public GameObject fish;
    public GameObject longfish;
    public GameObject bluefish;
    public GameObject pinkfish;
    public GameObject orangefish;
    public GameObject catfish;
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
        GameObject fishToSpawn = null;
        float randFloat = (int)Math.Ceiling(UnityEngine.Random.Range(0.0f, 1.0f) * 10);
        switch(randFloat)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                fishToSpawn = fish;
                break;
            case 5:
                fishToSpawn = longfish;
                break;
            case 6:
            case 7:
                fishToSpawn = bluefish;
                break;
            case 8:
                fishToSpawn = orangefish;
                break;

            case 9:
                if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.3f && transform.position.y < -3) {fishToSpawn = catfish;}
                else {fishToSpawn = fish;}
                break;
            case 10:
                if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.95f) {fishToSpawn = pinkfish;}
                else {fishToSpawn = fish;}
                break;
            default:
                fishToSpawn = fish;
                break;
        }
        
        GameObject instantiatedFish = Instantiate(fishToSpawn, transform.position, Quaternion.identity);
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
