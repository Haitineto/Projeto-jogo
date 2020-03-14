using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearStatusSpell : MonoBehaviour, ISpell
{
    private Character Target;

    public void Initialize(Sprite sprite, Character target)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;

        Target = target;
    }

    public void Execute()
    {
        Target.ClearStatus();

        Destroy(gameObject);
    }
}
