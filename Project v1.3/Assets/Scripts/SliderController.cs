using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public AudioSource bgm;
    //public AudioSource sfx;

    public Slider bgmSlider;
    //public Slider sfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("bgmVolume"))
        {
            bgmSlider.value = PlayerPrefs.GetFloat("bgmVolume");
        }
        else
        {
            bgmSlider.value = 0.75f;
        }

        /*if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        }
        else
        {
            sfxSlider.value = 0.75f;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
