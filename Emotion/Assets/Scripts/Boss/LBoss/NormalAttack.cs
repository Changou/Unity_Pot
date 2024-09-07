using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : Pattern
{
    [SerializeField] Transform _target;
    [SerializeField] GameObject _prefab;

    // Start is called before the first frame update
    void Start()
    {
        Attack();
    }

    void Attack()
    {
        Transform tmp = _target;
        Vector3 dir = tmp.position - transform.position;

        GameObject fire = Instantiate(_prefab, transform.parent);
        fire.GetComponent<FireBall>().Dir = dir.normalized;
    }
}
