using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : LivingEntity
{
    public enum TYPE
    {
        IDLE,
        ATTACK,
        DIE
    }

    [SerializeField] TYPE _type = TYPE.IDLE;

    [SerializeField] GameObject _prefab;

    [SerializeField] float _attackDelay = 2f;
    float _lastTime;

    [SerializeField] DetectionCollV2 _detection;

    [Header("Å¸°Ù")] 
    [SerializeField]Transform _target;

    [SerializeField] int _exp;

    protected override void Awake()
    {
        _anim = GetComponent<Animator>();
        _OnDeath += () =>
        {
            _target.GetComponentInChildren<PlayerInfo>().WeaponExpUp(_exp);
            Destroy(transform.parent.gameObject);
        };
    }

    private void Update()
    {
        if (_detection.Detection)
            _type = TYPE.ATTACK;
        else
            _type = TYPE.IDLE;

        if (_type == TYPE.IDLE || _type == TYPE.DIE) return;
        else if (_type == TYPE.ATTACK)
        {
            if (Time.time > _lastTime + _attackDelay)
            {
                Attack();
            }
        }
        
    }
    void Attack()
    {
        _anim.SetTrigger("Attack");
        Transform tmp = _detection.Target;
        Vector3 dir = tmp.position - transform.position;

        GameObject fire = Instantiate(_prefab, transform.parent);
        fire.GetComponent<FireBall>().Dir = dir.normalized;

        _lastTime = Time.time;
    }
}
