using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public Image timer;
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
    public static bool losePanel;
    public static bool tutorial;

    // Start is called before the first frame update
    void Start()
    {
        stage = 0;
        stageText.GetComponent<TextMeshProUGUI>().text = stage.ToString();
        timer = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        losePanel = lose;

        if (!lose)
        {
            if (currentAmount > 0 && startCountdown && !starting && !GameController.pause)
            {
                //Countdown timer
                currentAmount -= speed * Time.deltaTime;

                //Change timer color
                if (currentAmount <= 35 && currentAmount > 20)
                {
                    timer.color = new Color(1.0f, 0.64f, 0.0f);
                }
                else if(currentAmount <= 20)
                {
                    timer.color = Color.red;
                }

                //lose condition - time's up
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
                timer.color = Color.white;
                if (speed <= 150 && !tutorial) //max speed = 150
                    speed *= speedMultiplier;
                nextStage = false;
            }

            timer.fillAmount = currentAmount / 100;
        }
    }
    
}
