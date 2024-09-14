using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LBPhase1 : LivingEntity
{
    [SerializeField] float _skillDelay = 1f;

    [Header("보스 패턴 정보"), SerializeField] protected Pattern[] _pattern;

    [SerializeField] Transform _target;

    protected bool isDelay = false;

    private void Awake()
    {
        foreach (Pattern pattern in _pattern)
        {
            pattern.PatternOn(false);
        }
    }

    private void Start()
    {
        StartCoroutine(Think());
    }

    /*
    protected virtual void Update()
    {
        Direction();
    }

    void Direction()
    {
        Vector3 dir = _target.position - transform.position;

        Vector3 scale = new Vector3(dir.x > 0 ? -transform.localScale.x : transform.localScale.x, transform.localScale.y, 1);
        transform.localScale = scale;
    }
    */
    protected virtual IEnumerator Think()
    {
        while (!IsDead)
        {
            if (!isDelay)
            {
                int ran;
                while (true) {
                    ran = Random.Range(0, _pattern.Length);
                    if (!_pattern[ran].isActiveAndEnabled)
                        break;
                    yield return null;
                }
                _pattern[ran].PatternOn(true);
                StartCoroutine(CoolTime());
            }
            yield return null;
        }
    }
    protected IEnumerator CoolTime()
    {
        isDelay = true;
        yield return new WaitForSeconds(_skillDelay);
        isDelay = false;
    }
}
