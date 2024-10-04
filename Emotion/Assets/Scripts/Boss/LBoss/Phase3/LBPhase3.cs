using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBPhase3 : LivingEntity
{
    [Header("행동시간"), SerializeField] float _actTime;
    [Header("패턴"), SerializeField] Pattern[] _patterns;
    [Header("즉사패턴"), SerializeField] Pattern _dPattern;
    [Header("플레이어 감지"), SerializeField] DetectionArea _detected;

    bool _isRogicOn = true;

    enum PHASE3
    {
        N1ATTACK1,
        N1ATTACK2,
        N2ATTACK1,
        N2ATTACK2,
        N3ATTACK5,
        SPATTACK,

        MAX
    }

    Coroutine _rogic;

    void Start()
    {
        _rogic = StartCoroutine(Rogic());
    }

    IEnumerator Rogic()
    {
        while (!IsDead)
        {
            yield return new WaitForSeconds(_actTime);
            int num;
            while (true)
            {
                num = Random.Range(0, _patterns.Length);
                if (!_patterns[num].enabled)
                {
                    if ((num == (int)PHASE3.N2ATTACK1 && !_detected._isDetectPlayer)) continue;
                    break;
                }
                yield return null;
            }
           _patterns[num].PatternOn(true);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= _startingHealth / 2 && _dPattern != null)
        {
            StopCoroutine(_rogic);
            PatternAllStop();
            _dPattern.enabled = true;
        }
    }

    void PatternAllStop()
    {
        foreach(Pattern pattern in _patterns)
        {
            pattern.enabled = false;
        }
    }

    [Header("그로기 타임"), SerializeField] float _groggyTime;
    [Header("그로기 마테리얼")]
    [SerializeField] Material _groggyMat;

    public void Groggy()
    {
        StartCoroutine(GroggyOn());
    }
    IEnumerator GroggyOn()
    {
        Material defaultMat = _sprite.material;
        _sprite.material = _groggyMat;
        yield return new WaitForSeconds(_groggyTime);
        _sprite.material = defaultMat;
        _rogic = StartCoroutine(Rogic());
    }
}
