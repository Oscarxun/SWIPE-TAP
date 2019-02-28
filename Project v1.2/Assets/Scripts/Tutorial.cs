using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    Animator animator;
    public static bool moveUp = false;
    public static bool moveRight = false;
    public static bool tap = false;
    public static bool notUp = false;
    public static bool nothing = false;

    public GameObject cross;
    public ParticleSystem ripple;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (Timer.tutorial)
        {
            cross.SetActive(false);
            ripple.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveUp || notUp)
        {
            if (notUp)
            {
                cross.SetActive(true);
                notUp = false;
            }
            animator.SetBool("MoveUp", true);
            moveUp = false;
        }
        if (moveRight)
        {
            animator.SetBool("MoveRight", true);
            moveRight = false;
        }
        if(tap)
        {
            ripple.gameObject.SetActive(true);
            tap = false;
        }
        if(nothing)
        {
            ripple.gameObject.SetActive(false);
            cross.SetActive(true);
            nothing = false;
        }
        
        if(!Timer.tutorial)
        {
            cross.SetActive(false);
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
