using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParticleMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 movement;
    private Vector2 startPosition;

    Rigidbody2D rb;

    public static bool moveUp;
    public static bool moveDown;
    public static bool moveLeft;
    public static bool moveRight;

    public static bool notMove;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        if(moveUp)
        {
            movement = Vector2.up;
        }
        else if(moveDown)
        {
            movement = Vector2.down;
        }
        else if(moveLeft)
        {
            movement = Vector2.left;
        }
        else if(moveRight)
        {
            movement = Vector2.right;
        }
        else if(notMove)
        {
            movement = startPosition;
        }

        rb.AddForce(movement * speed);
        ResetBool();
    }

    void FixedUpdate()
    {
       
    }

    public void ResetBool()
    {
        moveUp = false;
        moveDown = false;
        moveLeft = false;
        moveRight = false;
        notMove = false;
    }
}
