using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBarDisplay : MonoBehaviour
{
    public GameObject ChargeBarAmount;
    public FishingPlayer FishPlayer;

    void Update()
    {
        float normalizedChargePower = FishPlayer.CastPower / 100.0f;
        ChargeBarAmount.transform.localScale = new Vector3(1.0f, normalizedChargePower, 1.0f);
    }
}
