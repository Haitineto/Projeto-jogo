using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTimeSpell : MonoBehaviour, ISpell
{
    private Timer Timer;
    private int Time;
    
    public void Initialize(int time, Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        Timer = GameObject.FindWithTag("Timer").GetComponent<Timer>();
        Time = time;

    }

    public void Execute()
    {
        StartCoroutine(FreezeTimeCourotine());
    }

    public IEnumerator FreezeTimeCourotine()
    {
        Timer.Paused = true;
        yield return new WaitForSeconds(Time);
        Timer.Paused = false;

        Destroy(gameObject);
    }
}
