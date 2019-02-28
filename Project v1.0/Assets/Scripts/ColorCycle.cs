using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCycle : MonoBehaviour
{
    private Renderer rend;
    //public GameObject timer;
    //private Timer timerScript;

    public Color[] TopColor;
    public Color[] BottomColor;
    public bool isChanging = true;
    private int currentIndex;
    private int maxIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        maxIndex = TopColor.Length;
        //timerScript = timer.GetComponent<Timer>();
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color1", TopColor[currentIndex]);
        rend.material.SetColor("_Color", BottomColor[currentIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        if (isChanging)
        {
            Color startTopColor = rend.material.GetColor("_Color1");
            Color startBottomColor = rend.material.GetColor("_Color");

            Color endTopColor = TopColor[0];
            Color endBottomColor = BottomColor[0];

            if (currentIndex + 1 < maxIndex)
            {
                endTopColor = TopColor[currentIndex + 1];
                endBottomColor = BottomColor[currentIndex + 1];
            }

            Color newTopColor = Color.Lerp(startTopColor, endTopColor, Time.deltaTime);
            Color newBottomColor = Color.Lerp(startBottomColor, endBottomColor, Time.deltaTime);

            rend.material.SetColor("_Color1", newTopColor);
            rend.material.SetColor("_Color", newBottomColor);

            if(newTopColor == endTopColor && newBottomColor == endBottomColor)
            {
                //isChanging = false;

                if (currentIndex + 1 < maxIndex)
                {
                    currentIndex++;
                }
                else
                {
                    currentIndex = 0;
                }
            }
        }
        

    }
}
