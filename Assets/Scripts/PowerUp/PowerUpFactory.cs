using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFactory
{
    public static IPowerUp Create(PowerUpType powerUpType, BattleController battleController)
    {
        if (powerUpType == PowerUpType.Potion)
            return new PotionPowerUp();
        else if (powerUpType == PowerUpType.Air)
            return new AtackPowerUp() { Element = EnumElement.Air, Expiration = 3 };
        else if (powerUpType == PowerUpType.Earth)
            return new AtackPowerUp() { Element = EnumElement.Earth, Expiration = 3 };
        else if (powerUpType == PowerUpType.Fire)
            return new AtackPowerUp() { Element = EnumElement.Fire, Expiration = 3 };
        else if (powerUpType == PowerUpType.Light)
            return new AtackPowerUp() { Element = EnumElement.Light, Expiration = 3 };
        else if (powerUpType == PowerUpType.Water)
            return new AtackPowerUp() { Element = EnumElement.Water, Expiration = 3 };
        else
            return null;
    }
}
