using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject ExplosionEffect;
    public Rigidbody2D rbGrenade;
    public Vector3 direction;
    public float explosionForce = 500f;
    public float Rad = 2f;
    public Collider2D coll;
    public bool thrown = false;
    public float countdown;
    public float timeBeforeExplode = 2f;
    public float damage = 50f;
    private void Awake()
    {
         coll = gameObject.GetComponent<CircleCollider2D>();
        rbGrenade = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        coll.isTrigger = true;
        rbGrenade.isKinematic = true;
        

    }


    public void throwG(Vector3 direction, float force){
        countdown = timeBeforeExplode;
        gameObject.transform.parent = null;
        thrown = true;
        coll.isTrigger = false;
        rbGrenade.isKinematic = false;
        rbGrenade.AddForce(direction.normalized * force, ForceMode2D.Impulse) ;

    }




    public void PickedUp()
    {
        coll.isTrigger = false;
        rbGrenade.isKinematic = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(thrown == true)
        {
            countdown -= Time.deltaTime;
            if(countdown <= 0)
            {
                explode();
            }

        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(rbGrenade.isKinematic == false)
        {
            if (collision.collider.gameObject.tag.Contains("Player"))
            {
                Physics2D.IgnoreCollision(coll, collision.collider);
            }
        }
    }




    private void explode()
    {
        if (ExplosionEffect != null)
        {
            GameObject expl =  Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
            Destroy(expl, 5f);
        }
   
            
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Rad);
        foreach(Collider2D col in colliders)
        {
            Rigidbody2D colRB = col.gameObject.GetComponent<Rigidbody2D>();
            if (colRB != null)
            {
                  Vector3 direction = (col.transform.position - transform.position).normalized;
             //   if(!colRB.gameObject.tag.Contains("Player"))
             //       colRB.constraints = rbGrenade.constraints;
                 colRB.GetComponent<Rigidbody2D>().AddRelativeForce(direction * explosionForce);
                if (colRB.gameObject.tag.Contains("Enemy"))
                {
                    colRB.gameObject.GetComponent<EnemyBehaviour>().TakeDamage((int)damage);
                }
            }
        }


        Destroy(gameObject);

    }

    private void OnDestroy()
    {
        if(thrown && PlayerControl.soundmanagerLoaded)
           SoundManager.PlaySound("Explosion", ref AudioSources.explosion);
    }





}
/*
public static class Rigidbody2DExt
{

    public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force)
    {
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        // Normalize without computing magnitude again
        if (upwardsModifier == 0)
            explosionDir /= explosionDistance;
        else
        {
            // From Rigidbody.AddExplosionForce doc:
            // If you pass a non-zero value for the upwardsModifier parameter, the direction
            // will be modified by subtracting that value from the Y component of the centre point.
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }

        rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir, mode);
    }
}
*/