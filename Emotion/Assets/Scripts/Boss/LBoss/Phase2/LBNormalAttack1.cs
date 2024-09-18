using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBNormalAttack1 : Pattern
{
    [SerializeField] Animator _anim;
    [SerializeField] Collider2D _collider;

    private void OnEnable()
    {
        _anim.SetTrigger("Attack");
        StartCoroutine(PatternOff());
    }

    IEnumerator PatternOff()
    {
        _collider.enabled = true;
        yield return new WaitForSeconds(_patternTime);
        _collider.enabled = false;
        PatternOn(false);
    }
}
