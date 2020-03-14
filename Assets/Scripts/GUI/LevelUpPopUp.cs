using UnityEngine;
using UnityEngine.UI;

public class LevelUpPopUp : MessagePopUp
{
    public Animator NewLevelPanel;

    public Animator AttributePointsPanel;

    public Animator UnlockPanel;

    public Animator Button;

    public Text NewLevelText;

    public Text AttributePointsText;

    public void OnContinueButtonClick()
    {
        NewLevelPanel.SetBool("Enabled", false);
        AttributePointsPanel.SetBool("Enabled", false);
        UnlockPanel.SetBool("Enabled", false);
        Button.SetBool("Enabled", false);
        //SoundManager.instance.PlayButtonClick();
        Hide();
    }

    public void Show(int newLevel, int attributePoints)
    {
        NewLevelPanel.SetBool("Enabled", true);
        AttributePointsPanel.SetBool("Enabled", true);
        UnlockPanel.SetBool("Enabled", true);
        Button.SetBool("Enabled", true);

        NewLevelText.text = newLevel.ToString();
        AttributePointsText.text = attributePoints.ToString();        

        base.Show();
    }
}
