using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMove : MonoBehaviour
{
    public Animator animator;    
    Rigidbody2D rbPlayer;
   // private float InputX;
    private int jumpCount = 0;
    public int jumpLimit = 2;
    private float speed = 0f;
    [Header("Set in Inspector")]
    public float defaultSpeed = 5f;
    public int maxSpeedUps = 1;
    public float runSpeedmodifier = 1.5f;
    private int jumpHeight = 10;


    public static bool FacingRight = true;
    public static PlayerMove S;

    // Start is called before the first frame update
    void Awake()
    {
        if(rbPlayer == null)
            rbPlayer = GetComponent<Rigidbody2D>();

        S = this;
    }
    private void Start()
    {
        FacingRight = true;
    }

    public void Stop()
    {
        speed = 0f;
    }

    public void SetDefaultSpeed(int mod)
    {
        speed = defaultSpeed*mod* runSpeedmodifier;
    }

    public bool SetRunningSpeed()
    {
        if(Mathf.Abs(speed) < Mathf.Abs(defaultSpeed * Mathf.Pow(runSpeedmodifier, maxSpeedUps)))
        {
            speed *= runSpeedmodifier;
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void moveLeftRight()
    {
   
            if (speed> 0 && !FacingRight)
            {
                Flip();
                FacingRight = true;
            }
        
       
            if (speed<0 && FacingRight)
            {
                Flip();
                FacingRight = false;
            }
           rbPlayer.velocity = new Vector2(speed, rbPlayer.velocity.y);

        
    }

    public void MoveDown()
    {
        rbPlayer.velocity  = new Vector2(rbPlayer.velocity.x, -jumpHeight);
        
    }






    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.gameObject.tag.Contains("Ground") || other.collider.gameObject.tag.Contains("Furniture"))
        {
            jumpCount = 0;
        }
        if (other.collider.gameObject.tag.Contains("Bound"))
        {
            rbPlayer.velocity = -rbPlayer.velocity;
        }

    }


    public void Jump() 
    {
        if(jumpCount < 2)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, jumpHeight);
            jumpCount += 1;
        }
      
    }

    void Flip()
    {
        rbPlayer.transform.Rotate(0,180,0);
    }
}
