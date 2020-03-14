using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AttributePointsPanel : MonoBehaviour
{    
    public AttributeType AttributeType;

    public Text CurrentAttributePoints;

    public delegate void OnChangeAttributePointsEvent(AttributeType AttributeType);

    public OnChangeAttributePointsEvent OnIncAttributePoints;

    public OnChangeAttributePointsEvent OnDecAttributePoints;

    public void UpdateText(int currentAttributePoints)
    {
        CurrentAttributePoints.text = currentAttributePoints.ToString();
    }

    public void PlusButtonClick()
    {
        if (OnIncAttributePoints!=null)
        {
            OnIncAttributePoints(AttributeType);
        }
    }    

    public void MinusButtonClick()
    {
        if (OnDecAttributePoints != null)
        {
            OnDecAttributePoints(AttributeType);
        }
    }  
}
