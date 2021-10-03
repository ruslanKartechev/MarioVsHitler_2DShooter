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
        IDamageable temp = collision.collider.gameObject.GetComponent<IDamageable>();
        if (temp != null) temp.TakeDamage(damage);
    }

}
