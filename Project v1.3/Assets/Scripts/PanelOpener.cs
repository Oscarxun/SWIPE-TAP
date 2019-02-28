using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    Animator animator;
    public static bool isOpen;
    public static bool isClose;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = false;
        isClose = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.pause)
        {
            animator.SetBool("Pause", true);
            isOpen = false;
        }

        if(!GameController.pause)
        {
            animator.SetBool("Pause", false);
            isClose = false;
        }
        
    }

    public void OpenPanel()
    {
        isOpen = true;
    }

    public void ClosePanel()
    {
        isClose = true;
    }
}
