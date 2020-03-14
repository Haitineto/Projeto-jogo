using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMystic : Boss
{
    public override void DoAttack()
    {
        var quantityOfRunas = Random.Range(2, MaxPower);

        var selectedRunas = new List<Runa>();

        for (int i = 0; i < quantityOfRunas; i++)
        {
            selectedRunas.Add(new Runa{ Element = EnumElement.Fire, Selected = true, PoweredUp = false });
        }

        ISpell spell = SpellFactory.Create(EnumElement.Fire, null, selectedRunas, this, Target, AttributePoints);
        spell.Execute();
    }
}
