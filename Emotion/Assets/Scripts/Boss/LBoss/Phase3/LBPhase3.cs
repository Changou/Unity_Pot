using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBPhase3 : LivingEntity
{
    [Header("행동시간"), SerializeField] float _actTime;
    [Header("패턴"), SerializeField] Pattern[] _patterns;
    [Header("즉사패턴"), SerializeField] Pattern _dPattern;
    [Header("플레이어 감지"), SerializeField] DetectionArea _detected;

    enum PHASE3
    {
        MOVE,
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
        _OnDeath += () => 
        {
            EndGame();
        };
        _rogic = StartCoroutine(Rogic());
    }

    IEnumerator Rogic()
    {
        while (!_IsDead)
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
        if(_Health <= _startingHealth / 2 && _dPattern != null)
        {
            StopCoroutine(_rogic);
            PatternAllStop();
            _dPattern.enabled = true;
        }
    }

    public void EndGame()
    {
        StopCoroutine(_rogic);
        UIManager._Inst.HideAll();
        AllClear();
        GameManager._Inst.Pause();
        PhaseManager._Inst.ProductionStart();
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

    public void AllClear()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Projectile");
        if (obj != null)
        {
            for (int i = 0; i < obj.Length; i++)
            {
                Destroy(obj[i]);
            }
        }

        foreach (Pattern pattern in _patterns)
        {
            pattern.StopAllCoroutines();
            pattern.PatternOn(false);
        }
    }

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
