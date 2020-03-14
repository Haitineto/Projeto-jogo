using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootingStatus : MonoBehaviour, IStatus
{
    private int Time;
    private SpriteRenderer SpriteRender;

    public void Initialize(int time)
    {
        SpriteRender = GetComponent<SpriteRenderer>();
        SpriteRender.sprite = GetComponent<SpriteRenderer>().sprite = CustomResources.Load<Sprite>("Status/RootingStatus");        

        Time = time;
    }

    public void Execute(Character target)
    {
        transform.position = target.transform.position;
        SpriteRender.enabled = true;

        StartCoroutine(Apply(target));
    }

    public void DoDestroy()
    {
        Destroy(gameObject);
    }

    IEnumerator Apply(Character target)
    {
        target.Paralyzed = true;
        yield return new WaitForSeconds(Time);
        target.Paralyzed = false;

        DoDestroy();
    }
}
