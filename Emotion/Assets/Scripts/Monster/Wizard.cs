using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public enum TYPE
    {
        IDLE,
        ATTACK,
        DIE
    }

    Animator _anim;

    [SerializeField] TYPE _type = TYPE.IDLE;

    [SerializeField] GameObject _prefab;

    [SerializeField] float _attackDelay = 2f;
    float _lastTime;

    [SerializeField] DetectionCollV2 _detection;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
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
