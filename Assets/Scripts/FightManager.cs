using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FightCountdown : MonoBehaviour
{
    public float FightTimeStart;

    public GameObject player1FightTutorial, player2FightTutorial, player1FishTutorial, player2FishTutorial, fightTitle, fishSpawnerL, fishSpawnerR;

    public FishingPlayer player1, player2;

    public DayCycle backgroundSky;
    public WaterColourer water;


	[SerializeField]
	private Vector2 m_offsetWhenTitle = new Vector2(0.5f, 0.5f);

	[SerializeField]
	private float m_titleAnimationTimeSeconds = 0.5f;

	private Coroutine m_showTitleRoutine = null;
	private float m_showTitleStartTime;

	public Vector2 m_basePosition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFightCountdown());
    }

	private double easeOutBounce(double x)
	{
		const double n1 = 7.5625f;
		const double d1 = 2.75f;

		if (x < 1 / d1)
		{
			return n1* x * x;
		}
		else if (x < 2 / d1)
		{
			return n1 * (x -= 1.5 / d1) * x + 0.75;
		}
		else if (x < 2.5 / d1)
		{
			return n1 * (x -= 2.25 / d1) * x + 0.9375;
		}
		else
		{
			return n1 * (x -= 2.625 / d1) * x + 0.984375;
		}
	}


	private IEnumerator ShowTitle()
	{
		m_showTitleStartTime = Time.time;

		float showTitleFinishTime = m_showTitleStartTime + m_titleAnimationTimeSeconds;

		while (Time.time < showTitleFinishTime)
		{
			// 0 to 1
			float progress = (Time.time - m_showTitleStartTime) / m_titleAnimationTimeSeconds;
			
			// 0 to 2
			progress *= 2;

			float progressEased = (float)easeOutBounce(progress);

			fightTitle.transform.localScale = Vector2.Lerp(m_basePosition, m_basePosition + m_offsetWhenTitle, progressEased);
			yield return null;
		}
        // hack
        while (Time.time < m_showTitleStartTime + m_titleAnimationTimeSeconds + m_titleAnimationTimeSeconds)
        {
            yield return null;
        }

		m_showTitleRoutine = null;
        fightTitle.SetActive(false);
	}

    private IEnumerator StartFightCountdown()
    {
        yield return new WaitUntil(() => Time.timeSinceLevelLoad >= FightTimeStart);

        // START FIGHT HERE!!
        backgroundSky.RedSky();
        water.RedWater();

        player1FishTutorial.SetActive(false);
        player2FishTutorial.SetActive(false);
        player1FightTutorial.SetActive(true);
        player2FightTutorial.SetActive(true);
        fishSpawnerL.GetComponent<SpawnFish>().SetFight();
        fishSpawnerR.GetComponent<SpawnFish>().SetFight();
        

        player1.CanFight = true;
        player2.CanFight = true;

		m_showTitleRoutine = StartCoroutine(ShowTitle());
    }
}
