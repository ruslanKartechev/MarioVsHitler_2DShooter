using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyProjectile : MonoBehaviour
{
    private Vector3 flyDirection;
    public float bulletSpeed = 30f;
    private Rigidbody2D rbBullet;
    public int damage;
    public GameObject ExplosionEffect;
    public GameObject trail;
    // Start is called before the first frame update
    void Start()
    {
        rbBullet = GetComponent<Rigidbody2D>();
        rbBullet.velocity = flyDirection.normalized * bulletSpeed;
        if(trail != null)
        {
            Instantiate(trail, transform.position, Quaternion.identity, gameObject.transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 500 || transform.position.x < -200)
        {
            Destroy(gameObject);
        }
        if (transform.position.y >200 || transform.position.y < -200)
        {
            Destroy(gameObject);
        }
    }
    public void GetDir(Vector3 dir)
    {
        flyDirection = dir;
    }

    public void GetDam(int dam)
    {
        damage = dam;
    }
    public void GetExplosionEffect(GameObject effect)
    {
        ExplosionEffect = effect;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.gameObject.tag.Contains("Enemy") || collision.collider.gameObject.tag.Contains("BulletPack"))
        {
            Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider2D>());
        } else
        {
            IDamageable temp = collision.collider.gameObject.GetComponent<IDamageable>();
            if (temp != null)
            {
                temp.TakeDamage(damage);
                if (ExplosionEffect != null) Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

}
