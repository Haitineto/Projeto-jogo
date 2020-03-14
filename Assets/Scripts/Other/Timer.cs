using System;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public float RemainingTime;

    public float TotalTime;

    public bool Running;

    public bool Paused;

    public delegate void OnTimeIsOverEvent();

    public OnTimeIsOverEvent OnTimeIsOver;

    public void Initialize(float totalTime)
    {
        TotalTime = totalTime;
        RemainingTime = TotalTime;
        Running = true;
    }

    public void Execute()
    {
        CountDown();
    }

    private void CountDown()
    {
        if (!Running) return;

        RemainingTime -= Time.deltaTime;

        if (RemainingTime <= 0)
            Running = false;

        if (RemainingTime <= 0 && OnTimeIsOver != null)
            OnTimeIsOver();
    }

    public void ChangeTime(int time)
    {
        RemainingTime += time;

        if (RemainingTime > TotalTime)
            RemainingTime = TotalTime;
    }
}
