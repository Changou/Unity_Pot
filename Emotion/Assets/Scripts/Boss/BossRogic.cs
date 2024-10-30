using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BOSS_TYPE
{
    ANGER,
    SAD,
    MADNESS,

    MAX
}

public class BossRogic : LivingEntity
{
    [SerializeField] protected BOSS_TYPE _bossType;

    [SerializeField] float _skillDelay = 2f;

    [Header("보스 패턴 정보"), SerializeField] protected Pattern[] _pattern;

    [SerializeField] float _damageDelay;
    float _lastDamage;

    [SerializeField] Transform _target;

    protected bool isDelay = false;

    Collider2D _coll;
    Rigidbody2D _rb;

    protected override void Awake()
    {
        _coll = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();

        base.Awake();

        foreach(Pattern pattern in _pattern)
        {
            pattern.PatternOn(false);
        }
    }

    private void Start()
    {
        _OnDeath += () => 
        {
            _coll.enabled = false;
            _rb.gravityScale = 0;
            StopAllCoroutines();
        };
        StartCoroutine(Think());
    }

    protected virtual void Update()
    {
        Direction();
    }

    void Direction()
    {
        Vector3 dir = _target.position - transform.position;
        if(dir.normalized.x < 0 && transform.localScale.x < 0)
        {
            transform.localScale = Vector3.one;
        }
        else if(dir.normalized.x > 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected virtual IEnumerator Think()
    {
        while (!_IsDead)
        {
            if (!isDelay)
            {
                int ran;
                while (true)
                {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable player = collision.GetComponent<IDamageable>();

        if (player != null && _bossType != BOSS_TYPE.SAD)
        {
            player.OnDamage(10f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IDamageable player = collision.GetComponent<IDamageable>();

        if (player != null && Time.time >= _lastDamage + _damageDelay && _bossType != BOSS_TYPE.SAD)
        {
            player.OnDamage(10f);
            _lastDamage = Time.time;
        }
    }


}
