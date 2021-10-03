using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats: MonoBehaviour
{

    public float m_MaxHealth;
    public float m_MaxArmor;
    public float m_startHealth;
    public float m_startArmor;
    public int m_startingGold;
    public int m_maxRespawns;
    // starting weapons 
    // starting ammo
    public float m_defaultMoveSpeed;
    public float m_maxMoveSpeed;
    public float m_defaultJumpHeight;
    public float m_maxJumpHeight;

    public bool m_allowMove = true;
    public bool m_allowJump = true;
    public int m_maxJumpCount;
    public bool m_allowAttack = true;
    public bool m_allowDamage = true;
    public bool m_allowPickUp = true;
    public bool m_isInvincible = false;

    public float startingGold;
    public int goldCount;
    public int killCount;

    private PlayerHealth mHealth;
    private PlayerArmor mArmor;
    private PlayerMove mMove;
    private PlayerSpawn mSpawning;
    private PlayerShoot mShoot;
    private PlayerAim mAiming;
    private PlayerControl mControl;
    private void Awake()
    {
        mHealth = GetComponent<PlayerHealth>();
        mArmor = GetComponent<PlayerArmor>();
        mMove = GetComponent<PlayerMove>();
        mShoot = GetComponent<PlayerShoot>();
        mAiming = GetComponent<PlayerAim>();
        mControl = GetComponent<PlayerControl>();
        mSpawning = GetComponent<PlayerSpawn>();
        if (mHealth != null)
        {
            mHealth._currentHealth = m_startHealth;
            mHealth.MaxHealth = m_MaxHealth;
        }

        if (mArmor != null)
        {
            mArmor._currentArmor = m_startArmor;
            mArmor.maxArmor = m_MaxArmor;
        }
        if(mMove != null)
        {
            mMove.allowMoving = m_allowMove;
            mMove.allowJumping = m_allowJump;
            mMove.jumpLimit = m_maxJumpCount;
        }
        if (mSpawning != null)
        {
            mSpawning.maxRespawns = m_maxRespawns;
        }


    }

    public void GiveHealth(float amount) => mHealth.AddHealth(amount);

    public void GiveArmor(float amount) => mArmor.AddArmor(amount);

    public void GiveGold(int amount) => goldCount += amount;
    public void AddKill(int amount) => killCount += amount;
    public void GiveMoveSpeed(float amount)
    {

    }
    public void GiveJumpHeight(float amount)
    {


    }
    public void FreezPlayer(bool onOff)
    {
        m_allowMove = false;
        mMove.allowMoving = m_allowMove;
    }

    public void AddRespawns(int amount)
    {
        mSpawning.respawnsCount += amount;
    }
    public void MakeInvinsible(bool onOff, float duration = 0f)
    {
        m_isInvincible = onOff;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (!collision.collider.transform.tag.Contains("Ground") 
            && !collision.collider.transform.tag.Contains("Bullet")
            && !collision.collider.transform.tag.Contains("Enemy"))
        {
            Collider2D col = collision.collider;
            IBonus bonus = col.gameObject.GetComponent<IBonus>();
            if(bonus != null)
            {
                bonus.GiveBonus(this);
                Destroy(col.gameObject);
            }
        }
    }

}
