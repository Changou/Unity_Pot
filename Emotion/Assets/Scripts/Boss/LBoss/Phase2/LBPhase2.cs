using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBPhase2 : LivingEntity
{
    public enum STATE
    {
        CHASE,
        ATTACK,
        SKILL,
        DIE,

        MAX
    }

    [Header("행동시간"), SerializeField] float _actTime;
    [Header("패턴"), SerializeField] Pattern[] _patterns;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rogic());
    }

    IEnumerator Rogic()
    {
        while (true)
        {
            yield return new WaitForSeconds(_actTime);
            int num;
            while (true)
            {
                num = Random.Range(0, _patterns.Length);
                if (!_patterns[num].enabled)
                {
                    break;
                }
            }
            _patterns[num].PatternOn(true);
            yield return null;
        }
    }
}
