using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackPowerUp : IPowerUp
{    
    public EnumElement Element { get; set; }
    public int Expiration { get; set; }    

    public void Apply(BattleController battleController)
    {
        for (int x = 0; x < Keyboard.KeyboardColumns; x++)
            for (int y = 0; y < Keyboard.KeyboardRows; y++)
            {
                if (battleController.Keyboard.Runas[x, y].Element == Element && !battleController.Keyboard.Runas[x, y].PoweredUp)
                    battleController.Keyboard.Runas[x, y].SetPoweredUp(true);
            }
        Expiration -= 1;
    }

    public void Revert(BattleController battleController)
    {
        for (int x = 0; x < Keyboard.KeyboardColumns; x++)
            for (int y = 0; y < Keyboard.KeyboardRows; y++)
            {
                if (battleController.Keyboard.Runas[x, y].Element == Element && battleController.Keyboard.Runas[x, y].PoweredUp)
                    battleController.Keyboard.Runas[x, y].SetPoweredUp(false);
            }        
    }

    public bool isValid()
    {
        return Expiration > 0;
    }
}
