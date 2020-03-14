using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class AttackSpell : MonoBehaviour, ISpell
{
    private Rigidbody2D Rb;
    private Vector3 Direction;
    private float Speed;
    private int Force;
    private bool Critical;
    private Character Owner;
    public IStatus Status;

    public void Initialize(int force, Character owner, Character target, bool critical, IStatus status, Sprite sprite)
    {
        Rb = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().sprite = sprite;

        Force = force;
        Critical = critical;
        transform.position = owner.transform.position;
        Direction = target.transform.position - owner.transform.position;
        Speed = 200f;
        Owner = owner;
        Status = status;
    }

    public void Execute()
    {
        CalculateForce();
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        Rb.velocity = Direction.normalized * Speed * Time.deltaTime;
        yield return null;
    }

    public int CalculateForce()
    {
        if (Critical)
            return (int)(Force * 1.5);
        else
            return Force;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        if (character != null && Owner != character)
        {
            character.Health.TakeDamage(Force);

            if (Status != null)
            {
                var status = character.AppliedStatus.Where(x => x.GetType() == Status.GetType()).ToList();
                if (status.Any())
                {
                    foreach (var item in status)
                    {
                        character.AppliedStatus.Remove(item);
                    }
                }
                character.AppliedStatus.Add(Status);
                Status.Execute(character);
            }

            Destroy(gameObject);
        }

        var protectionSpell = collision.GetComponent<ProtectionSpell>();
        if (protectionSpell != null && Owner != protectionSpell.Owner)
        {
            var newForce = Force - (int) protectionSpell.Health.Amount;
            protectionSpell.Health.TakeDamage(Force);
            Force = newForce;

            if (Force <= 0)
                Destroy(gameObject);

            if (protectionSpell.Health.IsDead)
                Destroy(protectionSpell.gameObject);
        }
    }

}
