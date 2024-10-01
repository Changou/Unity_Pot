using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBNormalAttack2 : Pattern
{
    [SerializeField] Transform _target;
    [SerializeField] Collider2D _coll;
    [SerializeField] Animator _anim;

    protected override IEnumerator Attack()
    {
        Vector3 pos = transform.localPosition;
        Vector3 scale = transform.localScale;
        if (_target.localScale.x > 0)
        {
            pos.x = _target.position.x - 1.5f;
            scale.x = -1;
        }
        else if(_target.localScale.x < 0)
        {
            pos.x = _target.position.x + 1.5f;
            scale.x = 1;
        }
        transform.localScale = scale;
        transform.localPosition = pos;
        yield return new WaitForSeconds(0.8f);

        _anim.SetTrigger("Attack");
        _coll.enabled = true;
        yield return new WaitForSeconds(0.5f);

        _coll.enabled = false;
        yield return new WaitForSeconds(_patternTime);
        PatternOn(false);
    }
}
