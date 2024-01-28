using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


public class FightCountdown : MonoBehaviour
{
    public float FightTimeStart;

    public GameObject player1FightTutorial, player2FightTutorial;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFightCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartFightCountdown()
    {
        yield return new WaitUntil(() => Time.time >= FightTimeStart);

        // START FIGHT HERE!!
        
        player1FightTutorial.SetActive(true);
        player2FightTutorial.SetActive(true);
    }
}
