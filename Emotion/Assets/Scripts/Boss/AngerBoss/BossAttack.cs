using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : Pattern
{
    [SerializeField] float _damage = 10f;
    [SerializeField] float _attackSpeed = 5f;
    [SerializeField] Transform _target;
    [SerializeField] GameObject _arrow;
    [SerializeField] float _omenDelay = 1f;

    Vector3 tmp;

    bool _patternStart = false;

    Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    protected override void OnEnable()
    {
        _patternStart = false;

        tmp = _target.position;
        tmp.y = transform.position.y;

        Vector3 dir = _target.position - transform.position;
        Vector3 scale = _arrow.transform.localScale;

        if (transform.localScale.x > 0)
        { scale.x = dir.x > 0 ? -0.15f : 0.15f; }
        else
        { scale.x = dir.x > 0 ? 0.15f : -0.15f; }
        
        _arrow.transform.localScale = scale;
        _arrow.SetActive(true);

        StartCoroutine(Omen());
    }

    private void Update()
    {
        if (!_patternStart) return;

        Attacker();
    }

    IEnumerator Omen()
    {
        yield return new WaitForSeconds(_omenDelay);
        _arrow.SetActive(false);
        _patternStart = true;
        _anim.SetTrigger("Attack");
    }

    void Attacker()
    {
        if(transform.position == tmp)
        {
            PatternOn(false);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, tmp, _attackSpeed * Time.deltaTime);
    }
}
