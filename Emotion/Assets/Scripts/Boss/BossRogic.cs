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

    protected virtual void Update()
    {
        Direction();
    }

    void Direction()
    {
        Vector3 scale = transform.localScale;
        if(scale.x != _target.localScale.x)
        {
            scale.x *= -1; 
        }
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
