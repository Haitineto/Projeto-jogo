using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    [SerializeField]
    public float MaxAlpha = 1;

    [SerializeField]
    public float FadeInSpeed = 0.5f;

    [SerializeField]
    public float FadeOutSpeed = 0.1f;    

    public NotifyEvent OnFinishFadeOut;    

    public NotifyEvent OnFinishFadeIn;    

    private IEnumerator fadeIn(MaskableGraphic maskableGraphic)
    {        
        while (maskableGraphic.color.a <= MaxAlpha)
        {
            maskableGraphic.color = new Color(maskableGraphic.color.r, maskableGraphic.color.g, maskableGraphic.color.b, maskableGraphic.color.a + FadeInSpeed);
            yield return null;
        }

        maskableGraphic.color = new Color(maskableGraphic.color.r, maskableGraphic.color.g, maskableGraphic.color.b, MaxAlpha);

        if (OnFinishFadeIn != null)
            OnFinishFadeIn();
    }    

    private IEnumerator fadeOut(MaskableGraphic maskableGraphic, Canvas canvas)
    {        
        while (maskableGraphic.color.a >= 0)
        {
            maskableGraphic.color = new Color(maskableGraphic.color.r, maskableGraphic.color.g, maskableGraphic.color.b, maskableGraphic.color.a - FadeOutSpeed);
            yield return null;
        }

        /*if (canvas != null)
            canvas.enabled = false;*/

        if (OnFinishFadeOut != null)
            OnFinishFadeOut();
    }

    public void StartFadeIn(Canvas canvas)
    {
        var maskableGraphic = GetComponent<MaskableGraphic>();

        if (maskableGraphic != null)
        {
            maskableGraphic.color = new Color(maskableGraphic.color.r, maskableGraphic.color.g, maskableGraphic.color.b, 0);

            if (canvas != null)
                canvas.enabled = true;

            StartCoroutine(fadeIn(maskableGraphic));
        }        
    }

    public void StartFadeOut(Canvas canvas)
    {
        var maskableGraphic = GetComponent<MaskableGraphic>();

        if (maskableGraphic != null)
        {
            maskableGraphic.color = new Color(maskableGraphic.color.r, maskableGraphic.color.g, maskableGraphic.color.b, MaxAlpha);

            if (canvas != null)
                canvas.enabled = true;

            StartCoroutine(fadeOut(maskableGraphic, canvas));
        }        
    }
}
