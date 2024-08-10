using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] float _damage = 10f;
    [SerializeField] float _attackSpeed = 5f;
    [SerializeField] Transform _target;


    private void Update()
    {
        Attack();
    }

    protected virtual void Attack()
    {
        Vector3 tmp = _target.position;
        tmp.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, tmp, _attackSpeed * Time.deltaTime);
    }
}
