using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour, ITakeGold
{
    private float currentGoldCount = 0f;

    public float _currentGoldCount
    {
        get { return currentGoldCount; }
    }
    private void Awake() {
    }
    public void TakeGold(float amount)
    {
        currentGoldCount += amount;
    }

}
