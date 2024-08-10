using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : Pattern
{
    [SerializeField] float _damage = 10f;
    [SerializeField] float _attackSpeed = 5f;
    [SerializeField] Transform _target;

    Vector3 tmp;

    private void OnEnable()
    {
        tmp = _target.position;
        tmp.y = transform.position.y;
    }

    private void Update()
    {
        Attack();
    }

    protected virtual void Attack()
    {
        if(transform.position == tmp)
        {
            PatternOn(false);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, tmp, _attackSpeed * Time.deltaTime);
    }
}
