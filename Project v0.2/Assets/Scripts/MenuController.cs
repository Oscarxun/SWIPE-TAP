using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI hiScore;
    public GameObject HiScorePanel;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
