using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionSpell : MonoBehaviour, ISpell
{    
    private Vector3 Direction;
    private Rigidbody2D Rb;
    public Character Owner;
    public int Protection;
    public float Speed;
    public Health Health;

    public void Initialize(int protection, Sprite sprite, Character owner, Character target)
    {
        Health = gameObject.AddComponent<Health>();
        Rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = sprite;

        Speed = 2f;
        transform.position = owner.transform.position;
        Health.Initialize(protection);
        Direction = target.transform.position - owner.transform.position;
        Owner = owner;
        Rb.velocity = Direction.normalized * Speed;
    }

    public void Execute()
    {
        StartCoroutine(MoveCourotine());
    }

    public IEnumerator MoveCourotine()
    {        
        yield return new WaitForSeconds(0.5f);
        Rb.velocity = new Vector2(0,0);
    }

}
