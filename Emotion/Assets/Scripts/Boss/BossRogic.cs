using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRogic : LivingEntity
{
    [SerializeField] float _skillDelay = 2f;

    [Header("보스패턴"), SerializeField] Pattern[] _pattern;

    bool isDelay = false;

    private void Awake()
    {
        foreach(Pattern pattern in _pattern)
        {
            pattern.PatternOn(false);
        }
    }

    private void Start()
    {
        StartCoroutine(Think());
    }

    IEnumerator Think()
    {
        while (!IsDead)
        {
            if (!isDelay)
            {
                int ran = Random.Range(0, 6);
                Debug.Log(ran);
                switch (ran)
                {
                    case 0:
                    case 1:
                    case 2:
                        _pattern[0].PatternOn(true);
                        break;
                    case 3:
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
    IEnumerator CoolTime()
    {
        isDelay = true;
        yield return new WaitForSeconds(_skillDelay);
        isDelay = false;
    }
}
