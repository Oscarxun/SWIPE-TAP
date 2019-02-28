using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    public AudioSource bgm;

    private float bgmVolume;


    // Start is called before the first frame update
    void Start()
    {
        bgm = GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("bgmVolume"))
        {
            bgmVolume = PlayerPrefs.GetFloat("bgmVolume");
        }
        else
        {
            bgmVolume = 0.75f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bgm.volume = bgmVolume;
    }

    public void SetBgmVolume(float vol)
    {
        bgmVolume = vol;
        PlayerPrefs.SetFloat("bgmVolume", vol);
    }
}
