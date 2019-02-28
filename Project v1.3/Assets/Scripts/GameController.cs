using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject timer;
    private Timer timerScript;

    public GameObject retryPanel;
    public GameObject pausePanel;
    public Button cross;
    public TextMeshProUGUI score;
    public TextMeshProUGUI hiScoreText;
    public TextMeshProUGUI score1;
    public TextMeshProUGUI hiScoreText2;
    public Text bestText;
    public TextMeshProUGUI CountDownText;

    public GameObject swipe;
    private SwipeDetection swipeScript;

    public TextMeshProUGUI orderText;
    private List<string> orders = new List<string>{ "UP", "DOWN", "LEFT", "RIGHT"};
    //private List<string> tutorialOrders = new List<string> { "UP", "RIGHT", "TAP", "NOT UP", "NOTHING" /*"BONUS"*/};
    private int index;
    //private string[] orders = { "UP", "DOWN", "LEFT", "RIGHT", "TAP", "DOUBLE TAP", "HOLD" };
    public int randIndex;
    public string randOrder;

    public bool tapToStart;
    public bool taped;
    public bool next;
    public static bool pause;

    public GameObject gesture;

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
        Timer.tutorial = true;

        tapToStart = false;
        taped = false;
        next = false;
        pause = false;

        if (PlayerPrefs.HasKey("Index"))
        {
            index = PlayerPrefs.GetInt("Index");
        }
        else
        {
            index = 0;
        }

        orderText.GetComponent<TextMeshProUGUI>().text = "TAP TO START";
        //orderText.canvasRenderer.SetAlpha(0.1f);

        hiScoreText.text = "High Score: " + PlayerPrefs.GetInt("High Score", 0).ToString();
        hiScoreText2.text = hiScoreText.text;
        bestText.text = PlayerPrefs.GetInt("High Score", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (index >= 5)
        {
            Timer.tutorial = false;
        }

        if (timerScript.starting)
        {
            if (!tapToStart)
            {
                orderText.canvasRenderer.SetAlpha(0.0f);
                orderText.CrossFadeAlpha(1.0f, 1.0f, false);
                //Debug.Log(orderText.canvasRenderer.GetAlpha());
                tapToStart = true;
                if(Timer.tutorial)
                {
                    timerScript.startCountdown = false;
                }
            }

            if (swipeScript.Tap && !taped && !pause)
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

        if (!timerScript.lose && orderText.canvasRenderer.GetAlpha() == 1.0f && !pause)
        {
            switch(randOrder)
            {
                case "UP":
                    {
                        if (Timer.tutorial)
                        {
                            gesture.SetActive(true);
                            Tutorial.moveUp = true;
                        }
                        if (swipeScript.SwipeUp)
                        {
                            ParticleMovement.moveUp = true;
                            NextFadeOut();
                            next = true;
                            if(Timer.tutorial)
                                index++;
                        }
                        else if (swipeScript.SwipeDown || swipeScript.SwipeLeft || swipeScript.SwipeRight)
                        {
                            timerScript.lose = true;
                        }
                        break;
                    }

                case "DOWN":
                    {
                        if (Timer.tutorial)
                        {
                            gesture.SetActive(true);
                            Tutorial.moveDown = true;
                        }
                        if (swipeScript.SwipeDown)
                        {
                            ParticleMovement.moveDown = true;
                            NextFadeOut();
                            next = true;
                            if (Timer.tutorial)
                                index++;
                        }
                        else if (swipeScript.SwipeUp || swipeScript.SwipeLeft || swipeScript.SwipeRight)
                        {
                            timerScript.lose = true;
                        }
                        break;
                    }

                case "LEFT":
                    {
                        if (Timer.tutorial)
                        {
                            gesture.SetActive(true);
                            Tutorial.moveLeft = true;
                        }
                        if (swipeScript.SwipeLeft)
                        {
                            ParticleMovement.moveLeft = true;
                            NextFadeOut();
                            next = true;
                            if (Timer.tutorial)
                                index++;
                        }
                        else if (swipeScript.SwipeUp || swipeScript.SwipeDown || swipeScript.SwipeRight)
                        {
                            timerScript.lose = true;
                        }
                        break;
                    }

                case "RIGHT":
                    {
                        if (Timer.tutorial)
                        {
                            gesture.SetActive(true);
                            Tutorial.moveRight = true;
                        }
                        if (swipeScript.SwipeRight)
                        {
                            ParticleMovement.moveRight = true;
                            NextFadeOut();
                            next = true;
                            if (Timer.tutorial)
                                index++;
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
                                timerScript.nothing = false;
                                NextFadeOut();
                                next = true;
                            }
                        }
                        break;
                    }

                case "NOT NOTHING":
                    {
                        if(swipeScript.SwipeRight || swipeScript.SwipeUp || swipeScript.SwipeLeft || swipeScript.SwipeDown || swipeScript.Tap || swipeScript.DoubleTap || swipeScript.Hold)
                        {
                            NextFadeOut();
                            next = true;
                        }
                        break;
                    }

                case "BONUS":
                    {
                        if(swipeScript.Tap)
                        {
                            timerScript.stage++;
                            timerScript.stageText.GetComponent<TextMeshProUGUI>().text = timerScript.stage.ToString();
                        }
                        timerScript.nothing = true;
                        if (timerScript.currentAmount <= 0)
                        {
                            timerScript.nothing = false;
                            NextFadeOut();
                            next = true;
                        }
                        break;
                    }
            }
        }

        if(next && !pause)
        {
            NextStage();
        }

        UpdateScore();

        if (timerScript.lose)
        {
            orderText.GetComponent<TextMeshProUGUI>().text = "YOU LOSE!";
            
            retryPanel.gameObject.SetActive(true);

            PlayerPrefs.SetInt("Index", index);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public string RandomOrder()
    {
        randIndex = Random.Range(0, orders.Count);
        return orders[randIndex];    
    }

    public void NextFadeOut()
    {
        ParticleMovement.notMove = true;
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
                orders.Add("TAP");
                orders.Add("DOUBLE TAP");
                //orders.Add("HOLD");
            }
            if(timerScript.stage == 24)
            {
                orders.Add("NOTHING");
            }
            if (timerScript.stage == 31)
            {
                orders.Add("NOT UP");
                orders.Add("NOT DOWN");
                orders.Add("NOT LEFT");
                orders.Add("NOT RIGHT");
            }
            if(timerScript.stage == 40)
            {
                orders.Add("NOT NOTHING");
            }
            randOrder = RandomOrder();
            orderText.GetComponent<TextMeshProUGUI>().text = randOrder;
            //Debug.Log(orderText.GetComponent<TextMeshProUGUI>().text);
            orderText.CrossFadeAlpha(1.0f, 0.5f, false);
        }

        if (orderText.canvasRenderer.GetAlpha() == 1.0f && !Timer.tutorial)
            timerScript.startCountdown = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Gameplay");
        timerScript.lose = false;
        //Time.timeScale = 1;
        retryPanel.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        if (!pause && !timerScript.lose)
        {
            pause = true;
            cross.interactable = true ;
            score1.text = "SCORE: " + (timerScript.stage).ToString();
            hiScoreText2.text = "High Score: " + bestText.text;
            pausePanel.gameObject.SetActive(true);
        }
    }

    public void Continue()
    {
        cross.interactable = false;
        
        if (PanelOpener.isClose)
            pausePanel.gameObject.SetActive(false);
        if (taped)
            StartCoroutine(CountDown(3));
        else
            pause = false;
    }

    public IEnumerator CountDown(int seconds)
    {
        int count = seconds;
        timerScript.startCountdown = false;
        CountDownText.gameObject.SetActive(true);

        while (count > 0)
        {
            CountDownText.GetComponent<TextMeshProUGUI>().text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }

        CountDownText.gameObject.SetActive(false);
        timerScript.startCountdown = true;
        pause = false;
    }

    public void UpdateScore()
    {
        score.text = "SCORE: " + (timerScript.stage).ToString();
        if (timerScript.stage > PlayerPrefs.GetInt("High Score"))
        {
            PlayerPrefs.SetInt("High Score", timerScript.stage);
            hiScoreText.text = "High Score: " + (timerScript.stage);
            bestText.text = timerScript.stage.ToString();
        }
    }
}
