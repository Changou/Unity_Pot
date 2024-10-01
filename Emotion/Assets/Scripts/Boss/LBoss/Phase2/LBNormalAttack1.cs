using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBNormalAttack1 : Pattern
{
    [SerializeField] Animator _anim;
    [SerializeField] Collider2D _collider;

    protected override void OnEnable()
    {
        _anim.SetTrigger("Attack");
        base.OnEnable();
    }

    protected override IEnumerator Attack()
    {
        _collider.enabled = true;
        yield return new WaitForSeconds(_patternTime);
        _collider.enabled = false;
        PatternOn(false);
    }
}
