using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public Transform timer;
    public float currentAmount;
    public float speed;
    public float speedMultiplier;
    public int stage;
    public TextMeshProUGUI stageText;

    public bool starting;
    public bool nextStage;
    public bool startCountdown;
    public bool nothing;
    public bool lose;
    

    // Start is called before the first frame update
    void Start()
    {
        stage = 1;
        stageText.GetComponent<TextMeshProUGUI>().text = stage.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!lose)
        {
            if (currentAmount > 0 && startCountdown && !starting)
            {
                currentAmount -= speed * Time.deltaTime;
                if (currentAmount <= 0 && !nothing)
                {
                    lose = true;
                }
            }
            else if (nextStage)
            {
                stage++;
                stageText.GetComponent<TextMeshProUGUI>().text = stage.ToString();
                currentAmount = 100;
                if(speed <= 100) //max speed = 70
                    speed *= speedMultiplier;
                nextStage = false;
            }

            timer.GetComponent<Image>().fillAmount = currentAmount / 100;
        }
    }

    
}
