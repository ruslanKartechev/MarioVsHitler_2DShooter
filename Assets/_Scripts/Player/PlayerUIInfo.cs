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

    private PlayerHealth healthH;
    private PlayerArmor armorH;
    private PlayerStats statsH;

    void Awake()
    {
        healthH = GetComponent<PlayerHealth>();
        armorH = GetComponent<PlayerArmor>();
        statsH = GetComponent<PlayerStats>();
    }
    private void Start()
    {
        StartCoroutine(UpdatePlayerStats());
    }

    private IEnumerator UpdatePlayerStats()
    {
        while (true)
        {
            healthText.text = "HP: " + healthH._currentHealth.ToString();
            healthBar.SetParameter(healthH._currentHealth / healthH.MaxHealth);
            armorText.text = "Armor: " + armorH._currentArmor.ToString();
            armorBar.SetParameter(armorH._currentArmor / armorH.maxArmor);
            goldText.text = "Gold: " + statsH.goldCount.ToString();
            killText.text = "Kills: " + statsH.killCount.ToString();
            if(WeaponInfo.CurrentWeapon != null)
                ammoText.text = "Current Weapon: " + WeaponInfo.CurrentWeapon.name + "\nAmmo: " + WeaponInfo.CurrentWeapon.currentAmmo;
            yield return null;
        }
    }
}
