using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPowerUp : IPowerUp
{
    private bool _executed = false;    

    public void Apply(BattleController battleController)
    {
        battleController.Player.Health.SetAmount(battleController.Player.Health.MaxAmount);
        _executed = true;
    }

    public void Revert(BattleController battleController)
    {
    }

    public bool isValid()
    {
        return !_executed;
    }
}
