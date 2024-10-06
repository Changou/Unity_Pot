using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBNormalAttack1 : Pattern
{
    [SerializeField] Animator _anim;
    [SerializeField] Collider2D _collider;
    [SerializeField] Transform _target;

    protected override void OnEnable()
    {
        _anim.SetTrigger("Attack");
        base.OnEnable();
    }

    protected override IEnumerator Attack()
    {
        Vector3 dir = _target.position - transform.position;

        Vector3 scale = transform.localScale;
        if(dir.x > 0) 
            scale.x = -1;
        else
            scale.x = 1;

        _collider.enabled = true;
        yield return new WaitForSeconds(_patternTime);
        _collider.enabled = false;
        PatternOn(false);
    }
}
