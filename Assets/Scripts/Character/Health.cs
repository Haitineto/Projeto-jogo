using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health: MonoBehaviour
{    
    public float MaxAmount { get; private set; }

    public float Amount { get; private set; }

    public bool IsDead { get; private set; }

    public delegate void OnDieEvent();

    public OnDieEvent OnDie;

    public void Initialize(int amount)
    {
        Amount = MaxAmount = amount;
        IsDead = false;        
    }

    public void SetAmount(float amount)
    {
        Amount = Mathf.Clamp(amount, 0, MaxAmount);

        if (Amount <= 0)
        {
            if (OnDie!=null) OnDie();                        
            IsDead = true;
        }        
    }

    public void TakeDamage(float amount)
    {
        SetAmount(Amount-amount);
    }

    public void IncreasesLife(float amount)
    {
        SetAmount(Amount + amount);
    }
}
