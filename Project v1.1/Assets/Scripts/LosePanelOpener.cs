using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePanelOpener : MonoBehaviour
{
    Animator animator;
    public static bool isMoved;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isMoved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.losePanel)
        {
            animator.SetBool("Lose", true);
        }

        isMoved = false;
    }

    public void Moved()
    {
        isMoved = true;
    }
}
