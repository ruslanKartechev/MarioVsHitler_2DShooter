using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    private float currentGoldCount;

    public float _currentGoldCount
    {
        get { return currentGoldCount; }
        set { currentGoldCount = value; }
    }
    private void Awake() {
    
    }
    public void AddGold(float amount)
    {
        currentGoldCount += amount;
    }

}
