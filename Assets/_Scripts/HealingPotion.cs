using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HealingPotion : MonoBehaviour, IBonus
{

    private float lLimit = 50f;
    private float uLimit = 70f;
    public void GiveBonus(PlayerStats player)
    {
        int amount = (int)Random.Range(lLimit, uLimit);
        player.GiveHealth(amount);
    }

}
