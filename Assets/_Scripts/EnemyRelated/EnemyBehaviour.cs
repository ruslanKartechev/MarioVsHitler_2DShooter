using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyBehaviour : MonoBehaviour
{
    public Animator animator;
    public Enemy classHandle;
    public EnemyManager managerHandle;
    public Transform firePoint;
    public Rigidbody2D body;
    public Vector2 shootDir;
    public Vector3 distanceToPlayer;
    public float timeToShoot = 0;
    private bool facingRight = false;
    public StatDisplay healthDispl;
    public AudioSource audioSource;
    private void Awake()
    {
      
            animator = gameObject.GetComponent<Animator>();
            body = GetComponent<Rigidbody2D>();
            managerHandle = FindObjectOfType<EnemyManager>();

        if (healthDispl == null)
            healthDispl = GetComponentInChildren<StatDisplay>();

        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        for (int i=0; i < managerHandle._enemyTypes.Length; i++)
        {
            if(managerHandle._enemyTypes[i].PreFab != null)
            {
                if (gameObject.tag == managerHandle._enemyTypes[i].PreFab.tag)
                {
                    classHandle = new Enemy(managerHandle._enemyTypes[i]);
                    classHandle.currentHealth = classHandle.maxHealth;
                }
            }
          
        }
    }



    public void Update()
    {
        
        if(healthDispl != null)
        {
            float h = (float)classHandle.currentHealth / (float)classHandle.maxHealth;
            healthDispl.SetParameter(h);
        }

        if (classHandle.currentHealth <= 0 && classHandle.isDead == false)
        {
            Die();
        }

        //Turning to player Mechanics;
        distanceToPlayer = managerHandle.Player.transform.position - gameObject.transform.position;
        if (distanceToPlayer.x > 0 && !facingRight)
        {
            facingRight = true;
            transform.Rotate(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
        } else if (distanceToPlayer.x < 0 && facingRight)
        {
            facingRight = false;
            transform.Rotate(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
        }

        // Move mechanics
        if (Mathf.Abs(distanceToPlayer.x) > classHandle.stopDistance && Mathf.Abs(distanceToPlayer.x) <= classHandle.followDistance )
        {
            MoveToPlayer();
           
        } 
        else if(Mathf.Abs(distanceToPlayer.x) <= classHandle.stopDistance && Mathf.Abs(distanceToPlayer.x) >= classHandle.retreatDistance)
        {
            body.velocity = Vector2.zero;
            animator.SetBool("Run", false);

            if (Time.time > timeToShoot)
            {
                if(classHandle.fireRate <= 0)
                {
                    return;
                }
                if (classHandle.fireRate != 0)
                {
                    timeToShoot = Time.time + 1 / classHandle.fireRate;
                    Shoot();
                }
                if (animator.GetBool("Shoot") != false)
                    animator.SetBool("Shoot", false);
            }
            
        } else if( Mathf.Abs( distanceToPlayer.x) <= classHandle.retreatDistance)
        {
            Retreate();
        }
        // end of movement mechanics


    }
    public void TakeDamage(int damage)
    {
        classHandle.currentHealth -= damage;
        if( classHandle.bleedingPF != null)
        {
            GameObject ex = Instantiate(classHandle.bleedingPF, transform.position, Quaternion.identity, gameObject.transform);
            ex.GetComponent<ParticleSystem>().Play();
            Destroy(ex, 1f);
        }
        
    }
    public void Die()
    {
        classHandle.isDead = true;
        animator.SetBool("Dead", true);
        Destroy(gameObject, 0.5f);
    }

    public void Shoot()
    {

        animator.SetBool("Shoot", true);
        // get shooting dir
        shootDir = managerHandle.Player.transform.position - gameObject.transform.position;
        //instantiate bullet
        GameObject b = Instantiate(classHandle.bulletPF, firePoint.position, gameObject.transform.rotation);
        if(classHandle.shootingPF!=null)
            Instantiate(classHandle.shootingPF, firePoint.position, gameObject.transform.rotation);
        if (b == null) // if bullet not found
        {
            Debug.LogWarning("Enemy bullet not assigned");
            return;
        }
        // set bullet parameters
        EnemyProjectile bh = b.GetComponent<EnemyProjectile>();
        bh.GetDir(shootDir);
        bh.GetDam(classHandle.damage);
        bh.GetExplosionEffect(classHandle.projectileExplosionEffect);
        // visual and sound effects
        Vector3 rot = gameObject.transform.rotation.eulerAngles + new Vector3(0, -180, 0);
        GameObject efPF = (GameObject)Instantiate(classHandle.shootingPF, firePoint.transform.position, transform.rotation, null);
        Destroy(efPF);
        //Destroy(Instantiate(classHandle.shootingPF, firePoint.transform.position, transform.rotation, null), 0.3f );
        SoundManager.PlaySound(classHandle.firingSoundName, ref audioSource);

    }
    public void MoveToPlayer()
    {
        animator.SetBool("Run", true);
        if (distanceToPlayer.x > 0)
        {
            body.velocity = new Vector3(classHandle.moveSpeed, body.velocity.y);
        } else if(distanceToPlayer.x < 0)
        {
            body.velocity = new Vector3(-classHandle.moveSpeed, body.velocity.y);

        }

    }
    public void Retreate()
    {
        animator.SetBool("Run", true);
        if (distanceToPlayer.x > 0)
        {
            body.velocity = new Vector3(-classHandle.moveSpeed, body.velocity.y);
        }
        else if (distanceToPlayer.x < 0)
        {
            body.velocity = new Vector3(classHandle.moveSpeed, body.velocity.y);

        }

    }

}




