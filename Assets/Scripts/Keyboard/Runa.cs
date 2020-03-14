using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class Runa : MonoBehaviour
{

    public EnumElement Element { get; set; }
    public bool Selected { get; set; }
    public bool PoweredUp { get; set; }
    public Vector2 SpotInKeyboard;
    public SkeletonAnimation SkeletonAnimation;
    public string CurrentAnimation;
    public AnimationReferenceAsset AnimIdle, AnimSelected, AnimPoweredUp, AnimSelectedPowerUp;    

    public void SetRunaElement(EnumElement element)
    {
        Element = element;        
        switch (element)
        {
            case EnumElement.Air:
                SkeletonAnimation.Skeleton.SetSkin("Runa de Ar");
                SkeletonAnimation.Skeleton.SetSlotsToSetupPose();
                break;
            case EnumElement.Earth:
                SkeletonAnimation.Skeleton.SetSkin("Runa de terra");
                SkeletonAnimation.Skeleton.SetSlotsToSetupPose();
                break;
            case EnumElement.Fire:
                SkeletonAnimation.Skeleton.SetSkin("Runas de fogo");
                SkeletonAnimation.Skeleton.SetSlotsToSetupPose();
                break;
            case EnumElement.Water:
                SkeletonAnimation.Skeleton.SetSkin("Runa de Agua");
                SkeletonAnimation.Skeleton.SetSlotsToSetupPose();
                break;
            case EnumElement.Light:
                SkeletonAnimation.Skeleton.SetSkin("Runa de Luz");
                SkeletonAnimation.Skeleton.SetSlotsToSetupPose();
                break;
        }
    }

    public void SetSelected(bool selected)
    {
        Selected = selected;        
    }

    public void SetPoweredUp(bool poweredUp)
    {
        PoweredUp = poweredUp;
    }

    public void Start()
    {        
        SetRunaState();        
    }

    public void Update()
    {
       SetRunaState();        
    }
    
    private void SetRunaState()
    {
        if (Selected)
        {
            if (PoweredUp)
                SetAnimation(AnimSelectedPowerUp, true, 1f);
            else
                SetAnimation(AnimSelected, true, 1f);
        }
        else if (PoweredUp)
        {
            SetAnimation(AnimPoweredUp, true, 1f);
        }
        else
        {
            SetAnimation(AnimIdle, true, 1f);
        }
    }

    public IEnumerator MoveToPostion(Vector3 newPosition)
    {
        while (transform.position.y >= newPosition.y)
        {            
            transform.position += new Vector3(0, -10)*Time.deltaTime;
            yield return null;
        }
        transform.position = newPosition;
    }    

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(CurrentAnimation))
            return;

        SkeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
        CurrentAnimation = animation.name;
    }
}
