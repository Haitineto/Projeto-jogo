using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp
{
    void Apply(BattleController battleController);
    void Revert(BattleController battleController);
    bool isValid();
}
