using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackP3 : Pattern
{
    [SerializeField] Transform _target;
    [SerializeField] float _jumpPos;
    Animator _anim;

    [Header("°Ë±â")]
    [SerializeField] GameObject _swordE;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    protected override IEnumerator Attack()
    {
        transform.localPosition = new Vector3(_target.position.x, _jumpPos, 0);
        _anim.SetBool("JDAttack", true);
        yield return new WaitForSeconds(1);

        Vector2 down = transform.localPosition;
        down.y = 0;
        transform.localPosition = down;

        GameObject sE1 = Instantiate(_swordE);
        sE1.transform.position = transform.position;
        Vector3 y1 = sE1.transform.position;
        y1.y = _target.position.y + 1;
        sE1.transform.position = y1;

        GameObject sE2 = Instantiate(_swordE);
        sE2.transform.localPosition = transform.position;
        Vector3 y2 = sE1.transform.position;
        y2.y = _target.position.y + 1;
        sE2.transform.position = y2;
        Vector3 reverse = sE2.transform.localScale;
        reverse.x = -1;
        sE2.transform.localScale = reverse;

        yield return new WaitForSeconds(1);
        _anim.SetBool("JDAttack", false);

        yield return null;
    }
}
