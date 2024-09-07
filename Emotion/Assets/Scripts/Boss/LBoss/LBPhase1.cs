using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LBPhase1 : LivingEntity
{
    [SerializeField] float _skillDelay = 2f;

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

    protected virtual IEnumerator Think()
    {
        while (!IsDead)
        {
            if (!isDelay)
            {
                int ran = Random.Range(0, 6);
                switch (ran)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        _pattern[0].PatternOn(true);
                        break;
                    case 4:
                    case 5:
                        _pattern[1].PatternOn(true);
                        break;
                }
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
