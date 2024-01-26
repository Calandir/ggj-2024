using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitching : MonoBehaviour
{
    public void GoToScene(string name)
    {
        GameManager.Instance.LoadSceneWithTransition(name);
    }
}
