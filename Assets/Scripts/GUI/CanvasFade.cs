using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFade : MonoBehaviour
{
    private void DisableCanvas()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public void StartFadeIn(NotifyEvent onFinishFadeIn)
    {        
        Canvas canvas = GetComponent<Canvas>();

        Fade[] fades = GetComponentsInChildren<Fade>();

        foreach (var item in fades)
            item.StartFadeIn(canvas);

        if (fades.Length > 0)
            fades[fades.Length - 1].OnFinishFadeIn = onFinishFadeIn;
    }

    public void StartFadeOut(NotifyEvent onFinishFadeOut)
    {
        Canvas canvas = GetComponent<Canvas>();

        Fade[] fades = GetComponentsInChildren<Fade>();

        foreach (var item in fades)
            item.StartFadeOut(canvas);

        if (fades.Length > 0)
        {
            fades[fades.Length - 1].OnFinishFadeOut = onFinishFadeOut;
            fades[fades.Length - 1].OnFinishFadeOut += DisableCanvas;
        }            
    }
}
