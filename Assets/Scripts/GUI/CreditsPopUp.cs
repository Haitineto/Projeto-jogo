using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPopUp : MessagePopUp
{
    public Animator SetupCanvas;

    public override void Show()
    {
        SetupCanvas.SetBool("Enabled", true);
        base.Show();
    }

    public override void Hide()
    {
        SetupCanvas.SetBool("Enabled", false);
        base.Hide();
    }
}
