using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    Animator animator;
    public static bool moveUp = false;
    public static bool moveRight = false;
    public static bool moveDown = false;
    public static bool moveLeft = false;
    //public static bool tap = false;
    //public static bool notUp = false;
    //public static bool nothing = false;

    public GameObject cross;
    public ParticleSystem ripple;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (Timer.tutorial)
        {
            ripple.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveUp)
        {
            animator.SetBool("MoveUp", true);
            moveUp = false;
        }
        if (moveRight)
        {
            animator.SetBool("MoveRight", true);
            moveRight = false;
        }
        if (moveDown)
        {
            animator.SetBool("MoveDown", true);
            moveDown = false;
        }
        if (moveLeft)
        {
            animator.SetBool("MoveLeft", true);
            moveLeft = false;
        }

        if (!Timer.tutorial)
        {
            ripple.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

    }

    public void Deactive()
    {
        gameObject.SetActive(false);
        cross.SetActive(false);
    }
}
