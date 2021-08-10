using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealeable
{
    public  float MaxHealth = 100;
    private float currentHealth;
    private PlayerArmor armorHandle;
    private bool isArmored = false;
    public GameObject damageEffect;
    public EventsManager eventsHandle;
    public float _currentHealth

    {
        get { return currentHealth; }
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (eventsHandle == null) eventsHandle = FindObjectOfType<EventsManager>();
        eventsHandle.StartGame.AddListener(() => OnGameStart());
        eventsHandle.PlayerRespawn.AddListener(() => OnPlayerRespawn());
        armorHandle = GetComponent<PlayerArmor>();
        if(armorHandle != null) { isArmored = true; }
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0 )
        {
            Die();
        }
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

        if(currentHealth<= 0f)
        {
            Die();
        }
    }
    public void TakeHeal(float healAmount)
    {
        currentHealth += healAmount;
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
        eventsHandle.PlayerDie.Invoke();
    }



}
