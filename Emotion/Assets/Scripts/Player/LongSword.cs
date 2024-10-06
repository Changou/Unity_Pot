using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSword : WeaponBase
{
    [Header("대검액션")]
    [SerializeField] Animator anim;
    [SerializeField] Transform trans;

    Collider2D _coll;

    public float _AttackDelay => _delay;

    private void Awake()
    {
        _coll = GetComponent<Collider2D>();
        _coll.enabled = false;
    }

    Coroutine _cor;

    public void Attack()
    {
        if (_cor != null) return;

        anim.SetTrigger("Attack");
        _cor = StartCoroutine(AttackDelay());
    }
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _coll.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _coll.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _cor = null;
    }

    public void Direction(float x)
    {
        if (x < 0) trans.rotation = Quaternion.Euler(0, 180f, 0);
        else if (x > 0) trans.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable target = collision.GetComponent<IDamageable>();

        if(target != null)
        {
            target.OnDamage(_ATK);
        }
    }
}
