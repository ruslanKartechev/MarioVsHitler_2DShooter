using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold: MonoBehaviour, IBonus
{

    private float lLimit = 100f;
    private float uLimit = 500f;
    public void GiveBonus(PlayerStats player)
    {
        int amount = (int)Random.Range(lLimit, uLimit);
        player.GiveGold(amount);
    }

}
