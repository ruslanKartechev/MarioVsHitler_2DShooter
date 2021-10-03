using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Enemy
{
    public string typeName;
    public string gameTag;
    public int maxHealth;
    public int currentHealth;
    public float fireRate;
    public GameObject bulletPF;
    public bool isDead;
    public float moveSpeed;
    public float jumpHeight;
    public int damage;
    public GameObject PreFab;
    public float detectionDistance;
    public float followDistance;
    public float retreatDistance;
    public float flyingHeight;
    public float stopDistance;
    public GameObject bleedingPF;
    public GameObject shootingPF;
    public Animator enemyAnimator;
    public GameObject projectileExplosionEffect;
    public string firingSoundName;
    public Enemy()
    {
      typeName = null;
      gameTag = null; 
      maxHealth = 1;
      currentHealth = maxHealth;
      fireRate = 1;
      bulletPF = null ;
      isDead = false;
      moveSpeed = 0f;
      jumpHeight = 0f;
      damage = 0;
        detectionDistance = 15f;
        followDistance = 12f;
        stopDistance = 8f;
        retreatDistance = 3f;
        flyingHeight = 0;
        projectileExplosionEffect = null;
        firingSoundName = null;

    }
    public Enemy( Enemy CopyFrom)
    {
        typeName = CopyFrom.typeName;
        gameTag = CopyFrom.gameTag;
        maxHealth = CopyFrom.maxHealth;
        currentHealth = CopyFrom.currentHealth;
        fireRate = CopyFrom.fireRate;
        bulletPF = CopyFrom.bulletPF;
        isDead = CopyFrom.isDead;
        moveSpeed = CopyFrom.moveSpeed;
        damage = CopyFrom.damage;
        detectionDistance = CopyFrom.detectionDistance;
        followDistance = CopyFrom.followDistance;
        stopDistance = CopyFrom.stopDistance;
        retreatDistance = CopyFrom.retreatDistance;
        flyingHeight = CopyFrom.flyingHeight;

        bleedingPF = CopyFrom.bleedingPF;
        shootingPF = CopyFrom.shootingPF;
        enemyAnimator = CopyFrom.enemyAnimator;
        projectileExplosionEffect = CopyFrom.projectileExplosionEffect;
        firingSoundName = CopyFrom.firingSoundName;



    }





}
