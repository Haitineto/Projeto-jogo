using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpell : MonoBehaviour , ISpell
{    
    public Timer Timer;
    public int Time;

    public void Initialize(int time, Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;

        Timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        Time = time;
    }

    public void Execute()
    {
        if (Timer.Running)
            Timer.ChangeTime(Time);

        Destroy(gameObject);
    }
}
