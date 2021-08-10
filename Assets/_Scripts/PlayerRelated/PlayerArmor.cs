using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmor : MonoBehaviour, IArmorable
{
    public float maxArmor = 100f;
    private float currentArmor;
    public float _currentArmor
    {
        get { return currentArmor; }
    }
    private void Awake()
    {
        currentArmor = maxArmor;
    }

    public void TakeHit(float damage)
    {
        currentArmor -= damage;
        if(currentArmor <= 0)
        {
            currentArmor = 0;
        }
    }
    public void EquipArmor(float amount)
    {
        currentArmor += amount;
    }
}
