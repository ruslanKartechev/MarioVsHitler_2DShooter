using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HealingPotion : MonoBehaviour
{
    private float ceeling;
    private float floor;
    public float offset = 0;
    private bool goUp = true;
    public float bouncingSpeed = 0.5f;
    private float phase = 0;
    private Vector3 unitY = new Vector3(0, 1, 0);

    void Awake()
    {
        if (GetComponent<CircleCollider2D>() != null)
        {
            offset = (float)GetComponent<CircleCollider2D>().radius;
        }
        ceeling = transform.position.y + offset;
        floor = transform.position.y - offset;
        phase = UnityEngine.Random.Range(-offset, offset);
        transform.position += unitY * phase;
    }

    // Update is called once per frame
    void Update()
    {
        if (goUp)
        {
            transform.position += unitY * bouncingSpeed * Time.deltaTime;

        }
        else if (!goUp)
        {
            transform.position -= unitY * bouncingSpeed * Time.deltaTime;
        }

        if (transform.position.y >= ceeling)
        {
            goUp = false;
        }
        else if (transform.position.y <= floor)
        {
            goUp = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag.Contains("Player"))
        {
            int health = (int)UnityEngine.Random.Range(40, 60);
            PlayerControl.Heal(health);
            Destroy(gameObject);
        }
    }

}
