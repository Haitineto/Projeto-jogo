using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatPopUp : MessagePopUp
{
    public delegate void OnUseCoinsToContinueEvent(int coinsToContinue);

    public OnUseCoinsToContinueEvent OnUseCoinsToContinue;

    public Animator MessagePanel;

    public Animator VideoPanel;

    public Animator CoinsPanel;

    public Animator AvaliableCoinsPanel;

    public Animator Button;    

    public Text CoinText;

    public Text AvaliableCoinText;

    public int CoinsToContinue;

    public int AvaliableCoins;

    public bool CoinsUsed = false;

    public override void Hide()
    {
        MessagePanel.SetBool("Enabled", false);
        VideoPanel.SetBool("Enabled", false);
        CoinsPanel.SetBool("Enabled", false);
        AvaliableCoinsPanel.SetBool("Enabled", false);
        Button.SetBool("Enabled", false);        
        SoundManager.instance.PlayButtonClick();

        base.Hide();
    }

    public void OnAcceptDefeatButtonClick()
    {        
        Hide();
    }

    public void OnUseCoinsButtonClick()
    {
        if (AvaliableCoins < CoinsToContinue) return;

        if (OnUseCoinsToContinue != null)
            OnUseCoinsToContinue(CoinsToContinue);

        CoinsUsed = true;

        Hide();
    }

    public void Show(int coinsToContinue, int avaliableCoins)
    {        
        MessagePanel.SetBool("Enabled", true);
        VideoPanel.SetBool("Enabled", true);
        CoinsPanel.SetBool("Enabled", true);
        AvaliableCoinsPanel.SetBool("Enabled", true);
        Button.SetBool("Enabled", true);

        CoinsToContinue = coinsToContinue;
        AvaliableCoins = avaliableCoins;

        if (AvaliableCoins < CoinsToContinue)
            CoinsPanel.GetComponent<Button>().interactable = false;
        else
            CoinsPanel.GetComponent<Button>().interactable = true;

        AvaliableCoinText.text = avaliableCoins.ToString();
        CoinText.text = "Usar " + CoinsToContinue.ToString() + " moedas";

        CoinsUsed = false;

        base.Show();
    }
}
