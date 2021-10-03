using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public  float MaxHealth;
    private float currentHealth;
    private PlayerArmor armorHandle;
    private bool isArmored = false;
    public GameObject damageEffect;
    private PlayerStats mStats;
    public float _currentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    void Awake()
    {
        mStats = GetComponent<PlayerStats>();
        GameManager.Instance.eventsManager.StartLevel.AddListener(() => OnGameStart());
        GameManager.Instance.eventsManager.PlayerRespawn.AddListener(() => OnPlayerRespawn());
        armorHandle = GetComponent<PlayerArmor>();
        if(armorHandle != null) { isArmored = true; }
        currentHealth = MaxHealth;
    }

    public void OnGameStart()
    {
        currentHealth = MaxHealth;
    }
    public void OnPlayerRespawn()
    {
        currentHealth = MaxHealth;
    }
    public void TakeDamage(float damage)
    {
        if (mStats.m_isInvincible == false)
            return;

        if(isArmored == true){
            if(armorHandle._currentArmor > 0f)
            {
                armorHandle.TakeHit(damage);
            } else {
                currentHealth -= damage;
            }
        }
        else
        {
            currentHealth -= damage;
        }

        if(currentHealth <= 0f)
        {
            Die();
        }
    }
    public void AddHealth(float healAmount)
    {
        if (currentHealth + healAmount <= MaxHealth)
            currentHealth += healAmount;
        else
            currentHealth = MaxHealth;
    }


    public void ShowDamageEffect(GameObject effect)
    {
        effect.transform.position = gameObject.transform.position + new Vector3(0, 1.5f, 0);
        effect.transform.rotation = gameObject.transform.rotation;
        effect.GetComponent<ParticleSystem>().Play();
    }



    private void Die()
    {
        currentHealth = 0;
        GameManager.Instance.eventsManager.PlayerDie.Invoke();
    }



}
