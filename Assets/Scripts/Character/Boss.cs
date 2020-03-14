using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : Character
{
    public const int TimeToAttack = 10;

    public Timer Timer;    

    public int MaxPower;

    public void Start()
    {
        Timer = GetComponent<Timer>();
        Timer.Initialize(TimeToAttack);
        Timer.OnTimeIsOver += OnTimeIsOver;
    }

    public virtual void DoYourStuff()
    {
        if (Timer!=null)        
            Timer.Execute();
    }

    public virtual void OnTimeIsOver()
    {
        if (!Paralyzed)
            DoAttack();

        Timer.Initialize(TimeToAttack);
    }

    public void Initialize(int healthAmount, AttributePoints attributePoints, Character target, int maxPower)
    {
        base.Initialize(healthAmount, attributePoints, target);        
        this.MaxPower = maxPower;
    }

    public int CalcPower()
    {
        return Random.Range(2, MaxPower);
    }

    public abstract void DoAttack();
}
