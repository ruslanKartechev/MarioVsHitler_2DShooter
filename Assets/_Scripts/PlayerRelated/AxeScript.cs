using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class AxeScript : MonoBehaviour
{
    public int damage = 50;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag.Contains("Enemy"))
        {
            collision.collider.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(damage);
            SoundManager.PlaySound("Stabbing", ref AudioSources.shoot);
        }
    }



}
