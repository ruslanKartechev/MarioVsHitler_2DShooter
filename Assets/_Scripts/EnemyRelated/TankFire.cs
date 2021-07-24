using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFire : MonoBehaviour
{
    private Vector3 flyDirection;
    public float projSpeed = 15f;
    private Rigidbody2D projRB;
    public ParticleSystem ExplosionEffect;
    public int damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        projRB = GetComponent<Rigidbody2D>();
        projRB.velocity = flyDirection * projSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 200 || transform.position.x < -20)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > 50 || transform.position.y < -50)
        {
            Destroy(gameObject);
        }
    }
    public void GetDir(Vector3 dir)
    {
        flyDirection = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      


        if (collision.collider.gameObject.tag.Contains("Enemy") || collision.collider.gameObject.layer == LayerMask.NameToLayer("Furniture") || collision.collider.gameObject.tag == "Ground" || collision.collider.gameObject.tag.Contains("Player") )
        {
            Destroy(gameObject);
        }
        if (collision.collider.gameObject.tag.Contains("Enemy") || collision.collider.gameObject.tag.Contains("BulletPack"))
        {
            Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider2D>());

        }
        if (collision.collider.gameObject.tag == "Player")
        {
            PlayerControl.TakeDamage(damage);
        }
    }

    private void OnDestroy()
    {
        if(ExplosionEffect != null)
             Instantiate(ExplosionEffect,  transform.position,Quaternion.identity);
        if (PlayerControl.soundmanagerLoaded == true)
        {
            SoundManager.PlaySound("tankFireExplosion", ref  AudioSources.enemySounds);
        }
    }




}
