using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BladeDamage : MonoBehaviour
{
    public float damage = 100f;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag.Contains("Player"))
        {
            PlayerControl.TakeDamage(damage);
            SoundManager.PlaySound("Stabbing_1", ref AudioSources.enemySounds);
        }
    }

}
