using System;
using UnityEngine;
using UnityEngine.UI;


public class TimerImage: MonoBehaviour
{    
    public Image ClockImage;

    public Timer Timer;

    public void Start()
    {
        ClockImage = GetComponent<Image>();
        Timer = GetComponent<Timer>();
    }        

    public void Update()
    {        
        UpdateImage();
    }    

    private void UpdateImage()
    {
        if (ClockImage != null)
            ClockImage.fillAmount = Timer.RemainingTime / Timer.TotalTime;
    }    
}
