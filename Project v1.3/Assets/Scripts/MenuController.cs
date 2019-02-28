﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI hiScore;
    public GameObject HiScorePanel;
    public GameObject SettingPanel;

    public Toggle tutorial;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Index"))
        {
            if (PlayerPrefs.GetInt("Index") >= 5)
            {
                tutorial.isOn = false;
                Timer.tutorial = false;
            }
        }
        else
        {
            tutorial.isOn = true;
            Timer.tutorial = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void QuitGame()
    {
        //Debug.Log("Quited!");
        Application.Quit();
    }

    public void StartGame()
    {
        //Load Gameplay Scene
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void ShowHiScore()
    {
        hiScore.text = PlayerPrefs.GetInt("High Score", 0).ToString();
        HiScorePanel.SetActive(true);
    }

    public void OpenSetting()
    {
        //SettingPanel.SetActive(true);
        SettingOpener.isOpen = true;
    }

    public void CloseSetting()
    {
        //SettingPanel.SetActive(false);
        SettingOpener.isClose = true;
    }

    public void EnableTutorial()
    {
        if(tutorial.isOn)
        {
            PlayerPrefs.SetInt("Index", 0);
            Debug.Log("HH");
        }
        if(!tutorial.isOn)
        {
            PlayerPrefs.SetInt("Index", 5);
            Debug.Log("ks");
        }
    }
}
