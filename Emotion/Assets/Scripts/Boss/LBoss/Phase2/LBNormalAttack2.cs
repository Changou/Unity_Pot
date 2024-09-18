using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBNormalAttack2 : Pattern
{
    [SerializeField] Transform _target;
    [SerializeField] Transform _myPos;
    [SerializeField] Collider2D _coll;
    [SerializeField] Animator _anim;

    private void OnEnable()
    {
        StartCoroutine(PatternOff());
    }

    IEnumerator PatternOff()
    {
        Vector3 pos = _myPos.position;
        Vector3 scale = _myPos.localScale;
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
        _myPos.localScale = scale;
        _myPos.position = pos;
        yield return new WaitForSeconds(0.8f);

        _anim.SetTrigger("Attack");
        _coll.enabled = true;
        yield return new WaitForSeconds(0.5f);

        _coll.enabled = false;
        yield return new WaitForSeconds(_patternTime);
        PatternOn(false);
    }
}
