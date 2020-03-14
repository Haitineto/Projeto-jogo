using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnStatus : MonoBehaviour, IStatus
{
    private int Damage;
    private int Time;
    private SpriteRenderer SpriteRender;

    public void Initialize(int damage, int time)
    {
        SpriteRender = GetComponent<SpriteRenderer>();
        SpriteRender.sprite = CustomResources.Load<Sprite>("Status/BurnStatus");

        Damage = damage;
        Time = time;
    }

    public void Execute(Character target)
    {
        transform.position = target.transform.position;

        StartCoroutine(Apply(target));
    }

    public void DoDestroy()
    {
        Destroy(gameObject);
    }

    IEnumerator Apply(Character target)
    {
        while (Time > 0)
        {
            StartCoroutine(ShowHide());
            target.Health.TakeDamage(Damage);
            --Time;
            yield return new WaitForSeconds(3);
        }

        DoDestroy();
    }

    IEnumerator ShowHide()
    {
        SpriteRender.enabled = true;
        yield return new WaitForSeconds(0.5f);
        SpriteRender.enabled = false;
    }
}
