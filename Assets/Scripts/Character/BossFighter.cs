using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossFighter : Boss
{
    private Coroutine _coroutine;

    public override void DoAttack()
    {
        StartMoveCoroutine(transform.position, Target.OriginPosition, 2f);
    }

    public IEnumerator MoveTo(Vector3 origin, Vector3 target, float speed)
    {
        float constant = (speed / (origin - target).magnitude) * Time.deltaTime;
        float t = 0;

        while (t <= 1.0f)
        {
            if (Paralyzed)
                yield break;

            t += constant;
            transform.position = Vector3.Lerp(origin, target, t);

            yield return null;
        }

        transform.position = target;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCharacterCollision(collision);
        CheckProtectionSpellCollision(collision);
    }

    private void CheckCharacterCollision(Collider2D collision)
    {
        var character = collision.GetComponent<Character>();
        if (character == null)
            return;

        character.Health.TakeDamage(Random.Range((float)Math.Truncate(CalcPower() * 0.10f), CalcPower()));

        StartMoveCoroutine(transform.position, OriginPosition, 2f);
    }

    private void CheckProtectionSpellCollision(Collider2D collision)
    {
        var protectionSpell = collision.GetComponent<ProtectionSpell>();
        if (protectionSpell == null)
            return;

        protectionSpell.Health.TakeDamage(5);
        if (protectionSpell.Health.IsDead)
            Destroy(protectionSpell.gameObject);

        StartMoveCoroutine(transform.position, OriginPosition, 2f);
    }

    private void StartMoveCoroutine(Vector3 origin, Vector3 target, float speed)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(MoveTo(origin, target, speed));
    }
}
