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
    public TextMeshProUGUI score;
    public TextMeshProUGUI hiScoreText;

    public GameObject swipe;
    private SwipeDetection swipeScript;

    public TextMeshProUGUI orderText;
    private List<string> orders = new List<string>{ "UP", "DOWN", "LEFT", "RIGHT"};
    //private string[] orders = { "UP", "DOWN", "LEFT", "RIGHT", "TAP", "DOUBLE TAP", "HOLD" };
    public int randIndex;
    public string randOrder;

    public bool tapToStart;
    public bool taped;
    public bool next;

    // Start is called before the first frame update
    void Start()
    {
        timerScript = timer.GetComponent<Timer>();
        swipeScript = swipe.GetComponent<SwipeDetection>();

        timerScript.starting = true;
        timerScript.nextStage = false;
        timerScript.startCountdown = true;
        timerScript.nothing = false;
        timerScript.lose = false;

        tapToStart = false;
        taped = false;
        next = false;

        orderText.GetComponent<TextMeshProUGUI>().text = "TAP TO START";
        //orderText.canvasRenderer.SetAlpha(0.1f);

        hiScoreText.text = "High Score: " + PlayerPrefs.GetInt("High Score", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(timerScript.starting)
        {

            if (!tapToStart)
            {
                orderText.canvasRenderer.SetAlpha(0.0f);
                orderText.CrossFadeAlpha(1.0f, 1.0f, false);
                //Debug.Log(orderText.canvasRenderer.GetAlpha());
                tapToStart = true;
            }

            if (swipeScript.Tap)
            {
                //Debug.Log(orderText.canvasRenderer.GetAlpha());
                orderText.CrossFadeAlpha(0.0f, 1.0f, false);
                taped = true;
            }
            else if(orderText.canvasRenderer.GetAlpha() == 0.0f)
            {
                randOrder = RandomOrder();
                orderText.GetComponent<TextMeshProUGUI>().text = randOrder;

                if (orderText.canvasRenderer.GetAlpha() < 1.0f)
                {
                    orderText.CrossFadeAlpha(1.0f, 0.5f, false);
                }
            }
            else if(orderText.canvasRenderer.GetAlpha() == 1.0f && taped)
                timerScript.starting = false;
        }

        if (!timerScript.lose && orderText.canvasRenderer.GetAlpha() == 1.0f)
        {
            switch(randOrder)
            {
                case "UP":
                    {
                        if (swipeScript.SwipeUp)
                        {
                            NextFadeOut();
                            next = true;
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
                            NextFadeOut();
                            next = true;
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
                            NextFadeOut();
                            next = true;
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
                            NextFadeOut();
                            next = true;
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
                            NextFadeOut();
                            next = true;
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
                            NextFadeOut();
                            next = true;
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
                            NextFadeOut();
                            next = true;
                        }
                        else if (swipeScript.SwipeUp || swipeScript.SwipeLeft || swipeScript.SwipeDown || swipeScript.SwipeRight)
                        {
                            timerScript.lose = true;
                        }
                        break;
                    }

                case "NOT UP":
                    {
                        if (swipeScript.SwipeUp)
                        {
                            timerScript.lose = true;
                        }
                        else if (swipeScript.SwipeDown || swipeScript.SwipeLeft || swipeScript.SwipeRight)
                        {
                            NextFadeOut();
                            next = true;
                        }
                        break;
                    }

                case "NOT DOWN":
                    {
                        if (swipeScript.SwipeDown)
                        {
                            timerScript.lose = true;
                        }
                        else if (swipeScript.SwipeUp || swipeScript.SwipeLeft || swipeScript.SwipeRight)
                        {
                            NextFadeOut();
                            next = true;
                        }
                        break;
                    }

                case "NOT LEFT":
                    {
                        if (swipeScript.SwipeLeft)
                        {
                            timerScript.lose = true;
                        }
                        else if (swipeScript.SwipeUp || swipeScript.SwipeDown || swipeScript.SwipeRight)
                        {
                            NextFadeOut();
                            next = true;
                        }
                        break;
                    }

                case "NOT RIGHT":
                    {
                        if (swipeScript.SwipeRight)
                        {
                            timerScript.lose = true;
                        }
                        else if (swipeScript.SwipeUp || swipeScript.SwipeLeft || swipeScript.SwipeDown)
                        {
                            NextFadeOut();
                            next = true;
                        }
                        break;
                    }

                case "NOTHING":
                    {
                        if(swipeScript.SwipeRight || swipeScript.SwipeUp || swipeScript.SwipeLeft || swipeScript.SwipeDown || swipeScript.Tap || swipeScript.DoubleTap || swipeScript.Hold)
                        {
                            timerScript.lose = true;
                        }
                        else
                        {
                            timerScript.nothing = true;
                            if(timerScript.currentAmount <= 0)
                            {
                                NextFadeOut();
                                next = true;
                            }
                        }
                        break;
                    }
            }
        }

        if(next)
        {
            NextStage();
        }

        if (timerScript.lose)
        {
            orderText.GetComponent<TextMeshProUGUI>().text = "YOU LOSE!";
            Time.timeScale = 0;
            if (timerScript.stage > PlayerPrefs.GetInt("High Score"))
            {
                PlayerPrefs.SetInt("High Score", timerScript.stage);
                hiScoreText.text = "High Score: " + timerScript.stage;
            }
            score.text = "SCORE: " + (timerScript.stage - 1).ToString();
            retryPanel.gameObject.SetActive(true);
        }
    }

    public string RandomOrder()
    {
        randIndex = Random.Range(0, orders.Count);
        //Debug.Log(randIndex);
        return orders[randIndex];
    }

    public void NextFadeOut()
    {
        timerScript.startCountdown = false;
        orderText.CrossFadeAlpha(0.0f, 0.5f, false);
    }

    public void NextStage()
    {
        if (orderText.canvasRenderer.GetAlpha() == 0.0f)
            timerScript.nextStage = true;

        if (timerScript.nextStage)
        {
            swipeScript.tapCount = 0;
            if (timerScript.stage == 16)
            {
                orders.Add("NOTHING");
                orders.Add("TAP");
                orders.Add("DOUBLE TAP");
                //orders.Add("HOLD");
            }
            if (timerScript.stage == 31)
            {
                orders.Add("NOT UP");
                orders.Add("NOT DOWN");
                orders.Add("NOT LEFT");
                orders.Add("NOT RIGHT");
            }
            randOrder = RandomOrder();
            orderText.GetComponent<TextMeshProUGUI>().text = randOrder;
            //Debug.Log(orderText.GetComponent<TextMeshProUGUI>().text);
            orderText.CrossFadeAlpha(1.0f, 0.5f, false);
        }

        if (orderText.canvasRenderer.GetAlpha() == 1.0f)
            timerScript.startCountdown = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Gameplay");
        timerScript.lose = false;
        Time.timeScale = 1;
        retryPanel.gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
