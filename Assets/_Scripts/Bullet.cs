using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IProjectile
{

    private Rigidbody2D rbBullet;
    public GameObject bulletTrail;
    private float damage;
    private Vector2 direction = new Vector2();
    private float Velocity;
    private PlayerShoot owner;

    void Awake()
    {
        rbBullet = GetComponent<Rigidbody2D>();
        if(bulletTrail != null)
        {
            GameObject trail = Instantiate(bulletTrail,transform.position,Quaternion.identity, gameObject.transform);
        }
    }

    void Update()
    {   
        if(transform.position.x > 400 || transform.position.x < -200)
        {
            Destroy(gameObject);
        }    
        if(transform.position.y > 200 || transform.position.y < -200)
        {
            Destroy(gameObject);
        }
    }
    public void StartProjectile()
    {
        rbBullet.velocity = direction * Velocity;
    }
    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }
    public void SetVelocity(float vel)
    {
        Velocity = vel;
    }
    public void SetDamage(float dam)
    {
        damage = dam;
    }
    public void SetOwner(PlayerShoot shootingHandle)
    {
        owner = shootingHandle;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    { 
        Vector3 v = Vector3.Reflect(transform.right, collision.contacts[0].normal);
        float rot = 90 - Mathf.Atan2(v.z, v.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, rot);


        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Furniture") || collision.collider.gameObject.tag == "Ground" || collision.collider.gameObject.tag.Contains("Bound"))
        {
            Destroy(gameObject);
        }
        if(collision.collider.gameObject.tag.Contains("Enemy"))
        {
       
            EnemyBehaviour ex = collision.collider.gameObject.GetComponent<EnemyBehaviour>();
            ex.TakeDamage((int)damage);
            Destroy(gameObject);

        } 
        if(collision.collider.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider);
        }
    }


}
