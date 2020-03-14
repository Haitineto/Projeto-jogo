using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpell : MonoBehaviour, ISpell
{

    private int Health;
    private int Time;
    private Character Target;

    public void Initialize(Sprite sprite, int health, int time, Character target)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;

        Health = health;
        Time = time;
        Target = target;
    }

    public void Execute()
    {
        StartCoroutine(Apply(Target));
    }

    IEnumerator Apply(Character target)
    {
        while (Time > 0)
        {
            target.Health.IncreasesLife(Health);
            --Time;
            yield return new WaitForSeconds(3);
        }

        Destroy(gameObject);
    }

}
