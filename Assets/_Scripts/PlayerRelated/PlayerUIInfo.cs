using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerUIInfo : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI killText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI livesLeft;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI armorText;
    public StatDisplay armorBar;
    public StatDisplay healthBar;


    public PlayerGold playerGoldHandle;
    public PlayerHealth playerHealthHandle;
    public PlayerArmor playerArmorHandle;
    

    void Awake()
    {
        if (playerGoldHandle == null) playerGoldHandle = GetComponent<PlayerGold>();
        if (playerHealthHandle == null) playerHealthHandle = GetComponent<PlayerHealth>();
        if (playerArmorHandle == null) playerArmorHandle = GetComponent<PlayerArmor>();
    }

    
    void FixedUpdate()
    {
        healthText.text = "HP: " + playerHealthHandle._currentHealth.ToString();
        healthBar.SetParameter(playerHealthHandle._currentHealth/playerHealthHandle.MaxHealth);
        armorText.text = "Armor: " + playerArmorHandle._currentArmor.ToString();
        armorBar.SetParameter(playerArmorHandle._currentArmor/playerArmorHandle.maxArmor);
        goldText.text = "Gold: " + playerGoldHandle._currentGoldCount.ToString();

        ammoText.text = "Current Weapon: " + WeaponInfo.CurrentWeapon.name + "\nAmmo: " + WeaponInfo.CurrentWeapon.currentAmmo;


    }
}
