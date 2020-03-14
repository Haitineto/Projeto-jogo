using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfo
{
    public int Id;

    public int BattleTime;

    public BossInfo BossInfo;    

    public BattleReward Reward;

    public AudioClip BattleMusic;

    public int CoinsToContinue()
    {
        return Reward.CoinsToEarn * 4;
    }
}
