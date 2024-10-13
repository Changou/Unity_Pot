using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LBPhase1 : LivingEntity
{
    [SerializeField] float _skillDelay = 1f;

    [Header("보스 패턴 정보"), SerializeField] protected Pattern[] _pattern;

    [SerializeField] Transform _target;

    [SerializeField] Shield _shield;

    protected bool isDelay = false;

    Animator _anim;

    private void Awake()
    {
        foreach (Pattern pattern in _pattern)
        {
            pattern.PatternOn(false);
        }
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        OnDeath += () => { 
            _anim.SetTrigger("Die");
            PhaseManager._Inst.Phase2Start();
        };
        StartCoroutine(Think());
        _shield.gameObject.SetActive(true);
    }

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
                _anim.SetTrigger("Attack");
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

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        _anim.SetTrigger("Hurt");
    }


    [Header("등장연출"), SerializeField] GameObject _firewall;
    public void NextPhase()
    {
        GameObject fire = Instantiate(_firewall);
        Destroy(fire, 3f);
        Invoke("Boss2Active",3f);
        _sprite.enabled = false;
    }
    void Boss2Active()
    {
        PhaseManager._Inst.PhaseEndAndNextPhase();
        PhaseManager._Inst.Phase2TitleOn();
        UIManager2._Inst.AllHide();
    }
}
