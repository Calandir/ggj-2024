using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptInput : MonoBehaviour
{

    public string waitInputKey;

    public float rotateAngle;

    public float rotateSpeed;

    private Dictionary<string, KeyCode[]> inputKeyMap;

    void Start()
    {
        inputKeyMap = new Dictionary<string, KeyCode[]>{
            {"lshift", new KeyCode[]{KeyCode.LeftShift}},
            {"rshift", new KeyCode[]{KeyCode.RightShift}},
            {"wsadz", new KeyCode[]{KeyCode.Z}},
            {"arrow", new KeyCode[]{KeyCode.Slash}}
        };
    }


    void Update()
    {
        KeyCode[] keyList = inputKeyMap[waitInputKey];

        // Once player has pressed input, stop tutorial.
        for (int i = 0; i < keyList.Length; i++) {
            if (Input.GetKey(keyList[i])) {
                gameObject.SetActive(false);
                break;
            }
        }

        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            Mathf.Sin(Time.time * rotateSpeed) * rotateAngle
        );

    }
}
