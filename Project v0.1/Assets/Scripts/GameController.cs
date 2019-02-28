using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject timer;
    private Timer timerScript;

    public GameObject retryPanel;

    public GameObject swipe;
    private SwipeDetection swipeScript;

    public TextMeshProUGUI orderText;
    private string[] orders = { "UP", "DOWN", "LEFT", "RIGHT", "TAP", "DOUBLE TAP", "HOLD" };
    public int randIndex;
    public string randOrder;

    // Start is called before the first frame update
    void Start()
    {
        timerScript = timer.GetComponent<Timer>();
        swipeScript = swipe.GetComponent<SwipeDetection>();

        timerScript.starting = true;
        timerScript.nextStage = false;
        timerScript.lose = false;
        orderText.GetComponent<TextMeshProUGUI>().text = "TAP TO START";
    }

    // Update is called once per frame
    void Update()
    {
        if(timerScript.starting)
        {
            if(swipeScript.Tap)
            {
                randOrder = RandomOrder();
                orderText.GetComponent<TextMeshProUGUI>().text = randOrder;
                timerScript.starting = false;
            }
        }

        if (!timerScript.lose)
        {
            switch(randOrder)
            {
                case "UP":
                    {
                        if (swipeScript.SwipeUp)
                        {
                            timerScript.nextStage = true;
                        }
                        else if (swipeScript.SwipeDown || swipeScript.SwipeLeft || swipeScript.SwipeRight)
                        {
                            timerScript.lose = true;
                        }
                        break;
                    }

                case "DOWN":
                    {
                        if (swipeScript.SwipeDown)
                        {
                            timerScript.nextStage = true;
                        }
                        else if (swipeScript.SwipeUp || swipeScript.SwipeLeft || swipeScript.SwipeRight)
                        {
                            timerScript.lose = true;
                        }
                        break;
                    }

                case "LEFT":
                    {
                        if (swipeScript.SwipeLeft)
                        {
                            timerScript.nextStage = true;
                        }
                        else if (swipeScript.SwipeUp || swipeScript.SwipeDown || swipeScript.SwipeRight)
                        {
                            timerScript.lose = true;
                        }
                        break;
                    }

                case "RIGHT":
                    {
                        if (swipeScript.SwipeRight)
                        {
                            timerScript.nextStage = true;
                        }
                        else if (swipeScript.SwipeUp || swipeScript.SwipeLeft || swipeScript.SwipeDown)
                        {
                            timerScript.lose = true;
                        }
                        break;
                    }

                case "TAP":
                    {
                        if (swipeScript.Tap)
                        {
                            timerScript.nextStage = true;  
                        }
                        else if (swipeScript.SwipeUp || swipeScript.SwipeLeft || swipeScript.SwipeDown || swipeScript.SwipeRight)
                        {
                            timerScript.lose = true;
                        }
                        break;
                    }

                case "DOUBLE TAP":
                    {
                        if (swipeScript.DoubleTap)
                        {
                            timerScript.nextStage = true;
                        }
                        else if (swipeScript.SwipeUp || swipeScript.SwipeLeft || swipeScript.SwipeDown || swipeScript.SwipeRight)
                        {
                            timerScript.lose = true;
                        }
                        break;
                    }

                case "HOLD":
                    {
                        if(swipeScript.Hold)
                        {
                            timerScript.nextStage = true;
                        }
                        else if (swipeScript.SwipeUp || swipeScript.SwipeLeft || swipeScript.SwipeDown || swipeScript.SwipeRight)
                        {
                            timerScript.lose = true;
                        }
                        break;
                    }
            }

            if (timerScript.nextStage)
            {
                //if (timerScript.stage > 20)
                randOrder = RandomOrder();
                orderText.GetComponent<TextMeshProUGUI>().text = randOrder;
            }
        }

            if (timerScript.lose)
        {
            orderText.GetComponent<TextMeshProUGUI>().text = "YOU LOSE!";
            Time.timeScale = 0;
            retryPanel.gameObject.SetActive(true);
        }
    }

    public string RandomOrder()
    {
        randIndex = Random.Range(0, orders.Length);
        return orders[randIndex];
    }

    public void Restart()
    {
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;
        retryPanel.gameObject.SetActive(false);
    }
}
