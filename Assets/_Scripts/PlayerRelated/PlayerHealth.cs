using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealeable
{
    public  float MaxHealth = 100;
    private float currentHealth;
    private PlayerArmor armorHandle;
    private bool isArmored = false;
    public int maxRespawns = 4;
    private int respawnsLeft;
    public GameObject damageEffect;
    public float _currentHealth

    {
        get { return currentHealth; }
    }
    // Start is called before the first frame update
    void Awake()
    {
        armorHandle = GetComponent<PlayerArmor>();
        if(armorHandle != null) { isArmored = true; }
        respawnsLeft = maxRespawns;
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
        respawnsLeft -= 1;
        currentHealth = 0;
    }



}
