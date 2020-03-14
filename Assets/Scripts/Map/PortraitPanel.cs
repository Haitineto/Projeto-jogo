using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitPanel : MonoBehaviour
{
    public Text LevelText;

    public Image XpForegroundPanel;

    void Start ()
    {
        LevelText.text = PlayerInfo.Level.ToString();
        float currentXp = 0;
        float targetXp = 0;
        if (PlayerInfo.Level == 1)
        {
            currentXp = PlayerInfo.XP;
            targetXp = LevelManager.CalcXpToLevel(PlayerInfo.Level);
        }
        else
        {
            currentXp = PlayerInfo.XP - LevelManager.CalcXpToLevel(PlayerInfo.Level - 1);
            targetXp = LevelManager.CalcXpToLevel(PlayerInfo.Level)- LevelManager.CalcXpToLevel(PlayerInfo.Level-1);
        }
        XpForegroundPanel.fillAmount = currentXp / targetXp;
    }    
}
