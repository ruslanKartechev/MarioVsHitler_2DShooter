using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 flyDirection;
    public float bulletSpeed = 50f;
    private Rigidbody2D rbBullet;
    public GameObject bulletTrail;
    public int  damage;
    // Start is called before the first frame update



    void Start()
    {
        rbBullet = GetComponent<Rigidbody2D>();
        rbBullet.velocity = flyDirection.normalized * bulletSpeed;
        if(bulletTrail != null)
        {
            GameObject trail = Instantiate(bulletTrail,transform.position,Quaternion.identity, gameObject.transform);
            
        }
    }

    // Update is called once per frame
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
    public void GetDir(Vector3 dir)
    {
        flyDirection = dir;
    }

    public void GetDam(int dam)
    {
        damage = dam;
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
            ex.TakeDamage(damage);
            Destroy(gameObject);

        } 
        if(collision.collider.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider);
        }
    }


}
