using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LBPhase2 : LivingEntity
{
    [Header("행동시간"), SerializeField] float _actTime;
    [Header("패턴"), SerializeField] Pattern[] _patterns;
    [Header("플레이어 감지"), SerializeField] DetectionArea _detected;

    enum PHASE2
    {
        MOVE,
        NATTACK1,
        NATTACK2,
        SATTACK1,
        SATTACK2,

        MAX
    }

    // Start is called before the first frame update
    void Start()
    {
        OnDeath += () => PhaseManager._Inst.PhaseEndAndNextPhase();
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
                    if ((num == (int)PHASE2.NATTACK1 && !_detected._isDetectPlayer)) continue;
                    break;
                }
                yield return null;
            }
            _patterns[num].PatternOn(true);
            yield return null;
        }
    }
}
