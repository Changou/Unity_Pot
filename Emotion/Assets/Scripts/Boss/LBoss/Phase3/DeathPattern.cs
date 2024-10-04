using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPattern : Pattern
{
    [Header("Á×À½ ¿µ¿ª"), SerializeField] GameObject _deathArea;

    [Header("ÆÄÈÑ½Ã°£")]
    [SerializeField] float _deathTime;

    [SerializeField] GameObject _barrier;

    Animator _anim;
    Collider2D _coll;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _coll = GetComponent<Collider2D>();
    }

    protected override IEnumerator Attack()
    {
        transform.localPosition = Vector3.zero;
        _barrier.SetActive(true);
        _coll.enabled = false;

        yield return new WaitForSeconds(_deathTime);
        _anim.SetTrigger("DAttackOn");
        yield return new WaitForSeconds(1);
        GameObject death = Instantiate(_deathArea);
        yield return new WaitForSeconds(1);
        Destroy(death);
    }

    private void Update()
    {
        if (!_barrier.activeSelf)
        {
            StopCoroutine(Attack());
            transform.GetComponent<LBPhase3>().Groggy();
            Destroy(this);
        }
    }
}
