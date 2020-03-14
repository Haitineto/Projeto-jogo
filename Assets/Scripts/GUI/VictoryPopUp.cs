using UnityEngine;

public class VictoryPopUp : MessagePopUp
{
    public Animator MessagePanel;

    public Animator XpPanel;    

    public Animator CoinsPanel;

    public Animator Button;

    public Counter CoinText;

    public Counter XpText;

    public void OnContinueButtonClick()
    {
        MessagePanel.SetBool("Enabled", false);
        XpPanel.SetBool("Enabled", false);
        CoinsPanel.SetBool("Enabled", false);
        Button.SetBool("Enabled", false);        
        //SoundManager.instance.PlayButtonClick();
        Hide();        
    }

    public void Show(int reward, int xp)
    {        
        MessagePanel.SetBool("Enabled", true);
        XpPanel.SetBool("Enabled", true);
        CoinsPanel.SetBool("Enabled", true);
        Button.SetBool("Enabled", true);

        XpText.StartCount(xp, 5);
        CoinText.StartCount(reward, 5);

        base.Show();
    }    
}
