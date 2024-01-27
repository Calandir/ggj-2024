using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // You can put fields in GameManager for data transfer between scenes
    // (like numFishesCaught or something)

    [SerializeField]
    private string m_transitionSceneName = "TransitionScene";

    [SerializeField]
    private float m_transitionDelayTime = 1.0f;

    [SerializeField]
    private Canvas m_canvas;

    private string m_previousScene = null;

	// Start is called before the first frame update
	void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            // Only one GameManager allowed
            Destroy(this);
        }
    }

    public void LoadSceneWithTransition(string sceneName)
    {
        m_canvas.gameObject.SetActive(false);

        StartCoroutine(TransitionRoutine(sceneName));
    }

    private IEnumerator TransitionRoutine(string sceneToLoad)
    {
        // Show transition
        yield return SceneManager.LoadSceneAsync(m_transitionSceneName, LoadSceneMode.Additive);

        float timeAtTransitionStart = Time.time;

        // Swap out scenes underneath the transition
		if (!string.IsNullOrEmpty(m_previousScene))
		{
			yield return SceneManager.UnloadSceneAsync(m_previousScene);
		}
		yield return SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        yield return WaitUntilTime(timeAtTransitionStart + m_transitionDelayTime);

        // Remove transition
		yield return SceneManager.UnloadSceneAsync(m_transitionSceneName);

		m_previousScene = sceneToLoad;
	}

    private IEnumerator WaitUntilTime(float endTime)
    {
		yield return new WaitUntil(() => Time.time >= endTime);
	}
}
