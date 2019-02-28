using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingOpener : MonoBehaviour
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
        if(isOpen)
        {
            animator.SetBool("OpenSetting", true);
            isOpen = false;
        }
            
        if(isClose)
        {
            animator.SetBool("OpenSetting", false);
            isClose = false;
        }
    }
}
