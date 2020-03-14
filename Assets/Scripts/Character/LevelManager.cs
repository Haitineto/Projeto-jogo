using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{
    public static void AddXp(int xp)
    {
        PlayerInfo.XP = PlayerInfo.XP + xp;        

        if (reachedNewLevel())
            UpLevel();
    }

    public static void UpLevel()
    {
        PlayerInfo.Level += 1;
        PlayerInfo.AvaliableAttributePoints += 1;
        PlayerInfo.NewLevelReached="S";
    }

    private static bool reachedNewLevel()
    {
        return PlayerInfo.XP > CalcXpToLevel(PlayerInfo.Level);
    }

    public static int CalcXpToLevel(int currentLevel)
    {
        return (currentLevel * currentLevel) * 100;
    }
}
