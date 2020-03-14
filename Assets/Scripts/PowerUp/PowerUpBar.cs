using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBar : MonoBehaviour
{
    public PowerUpButton[] Butons = new PowerUpButton[4];

    private void setButtonsPosition()
    {
        for (int x=0; x<Butons.Length; x++)
            if (Butons[x]!=null)            
                Butons[x].transform.position = calculatePowerUpPosition(Butons[x].gameObject, x);
    }

    private Vector3 calculatePowerUpPosition(GameObject powerUp, int column)
    {        
        Bounds powerUpBarBounds = GetComponent<SpriteRenderer>().bounds;

        Bounds powerUpBounds = powerUp.GetComponent<SpriteRenderer>().bounds;

        float widthOfEachPowerUp = powerUpBarBounds.size.x / Butons.Length;

        float heightOfEachPowerUp = powerUpBarBounds.size.y / 1;

        float firstColumnPosition = powerUpBarBounds.min.x + (widthOfEachPowerUp/2);

        float firstRowPosition = powerUpBarBounds.max.y - (heightOfEachPowerUp/2);        

        return new Vector3(firstColumnPosition + (column * widthOfEachPowerUp), firstRowPosition, -1);
    }

    public void Initialize(PowerUpButton[] butons)
    {
        foreach (var item in Butons)        
            Destroy(item);        
        Butons = butons;
        setButtonsPosition();
    }

    public void CheckClick()
    {
        foreach (var item in Butons)
            if (item!=null)
                item.CheckClick();
    }
}
