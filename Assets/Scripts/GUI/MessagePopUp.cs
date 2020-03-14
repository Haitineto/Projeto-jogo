using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePopUp : MonoBehaviour
{    
    public NotifyEvent AfterPopUpClose;

    private void disableCanvas()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public virtual void Show()
    {
        Canvas canvas = GetComponent<Canvas>();
        Fade[] fades = GetComponentsInChildren<Fade>();
        foreach (var item in fades)        
            item.StartFadeIn(canvas);
        if (fades.Length <= 0)
            canvas.enabled = true;
    }

    public virtual void Hide()
    {
        Fade[] fades = GetComponentsInChildren<Fade>();
        Canvas canvas = GetComponent<Canvas>();

        foreach (var item in fades)        
            item.StartFadeOut(canvas);

        if (fades.Length <= 0)
        {
            DoAfterPopUpClose();
            canvas.enabled = false;            
        }
        else
        {
            fades[fades.Length - 1].OnFinishFadeOut = AfterPopUpClose;
            fades[fades.Length - 1].OnFinishFadeOut += disableCanvas;
        }
    }

    private void DoAfterPopUpClose()
    {
        if (AfterPopUpClose != null)
            AfterPopUpClose();
    }
}