using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPattern : Pattern
{
    [Header("���� ����"), SerializeField] GameObject _deathArea;

    [Header("���ѽð�")]
    [SerializeField] float _deathTime;

    [SerializeField] GameObject _barrier;

    Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    protected override IEnumerator Attack()
    {
        transform.localPosition = Vector3.zero;
        _barrier.SetActive(true);

        yield return new WaitForSeconds(_deathTime);
        _anim.SetTrigger("DAttackOn");
        yield return new WaitForSeconds(1);
        GameObject death = Instantiate(_deathArea);
        yield return new WaitForSeconds(1);
        Destroy(death);
    }
}
