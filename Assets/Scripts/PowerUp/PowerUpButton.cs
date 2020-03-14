using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpButton : MonoBehaviour
{
    private bool _keyIsAlreadyDown;

    public PowerUpType PowerUpType;

    public delegate void OnSelectPowerUpEvent(PowerUpType PowerUpType);

    public OnSelectPowerUpEvent OnSelectPowerUp;

    public int Quantity;

    private Vector3 getMouseAdjustedPosition()
    {
        Vector3 mouseCurrentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseAdjustedPos = new Vector3(mouseCurrentPos.x, mouseCurrentPos.y, -1);
        return mouseAdjustedPos;
    }

    public void CheckClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_keyIsAlreadyDown)
        {
            _keyIsAlreadyDown = true;
            Bounds powerUpButtonBounds = GetComponent<SpriteRenderer>().bounds;
            Bounds mousePointerBounds = new Bounds(getMouseAdjustedPosition(), new Vector3(0.01f, 0.01f, 0));
            if (powerUpButtonBounds.Intersects(mousePointerBounds) && OnSelectPowerUp != null)
            {
                OnSelectPowerUp(PowerUpType);
                Quantity -= 1;
                if (Quantity <= 0)
                    Destroy(gameObject);
            }                
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
            _keyIsAlreadyDown = false;
    }    		

    public void Initialize(PowerUpType powerUpType, int quantity, OnSelectPowerUpEvent onSelectPowerUp)
    {
        Quantity = quantity;
        PowerUpType = powerUpType;
        OnSelectPowerUp += onSelectPowerUp;

        GetComponent<SpriteRenderer>().sprite = CustomResources.Load<Sprite>("PowerUps/PowerUp" + powerUpType);
    }
}
