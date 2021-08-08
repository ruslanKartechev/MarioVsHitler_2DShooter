using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rbPlayer;
    private int jumpCount = 0;
    public int jumpLimit = 2;
    private float speed = 0f;
    [Header("Set in Inspector")]
    public float defaultSpeed = 5f;
    public int maxSpeedUps = 1;
    public float runSpeedmodifier = 1.5f;
    private int jumpHeight = 10;
    private float moveInput;

    public static bool FacingRight = true;
    public static PlayerMove S;

    // Start is called before the first frame update
    void Awake()
    {
        if(rbPlayer == null)
            rbPlayer = GetComponent<Rigidbody2D>();

        S = this;
        FacingRight = true;
        SetDefaultSpeed();
    }

    public void Stop()
    {
        speed = 0f;
    }

    public void SetDefaultSpeed()
    {
        speed = defaultSpeed*runSpeedmodifier;
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

    public void moveLeftRight(float input)
    {
        moveInput = input;
        if (input > 0 && !FacingRight)
        {
            FacingRight = true;
            Flip();
        }

        if (input < 0 && FacingRight)
        {
            Flip();
            FacingRight = false;
        }
            
    }

    public void MoveDown()
    {
        rbPlayer.velocity  = new Vector2(rbPlayer.velocity.x, -jumpHeight);   
    }
    public void Jump()
    {
        if (jumpCount < 2)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, jumpHeight);
            jumpCount += 1;
        }

    }

    void Flip()
    {
        rbPlayer.transform.Rotate(0, 180, 0);
    }
    private void FixedUpdate()
    {
        if(moveInput == 0f)
        {
            rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
        }
        else if(moveInput > 0)
        {
            rbPlayer.velocity = new Vector2(speed, rbPlayer.velocity.y);
        } else if(moveInput < 0)
        {
            rbPlayer.velocity = new Vector2(-speed, rbPlayer.velocity.y);
        }
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

}
