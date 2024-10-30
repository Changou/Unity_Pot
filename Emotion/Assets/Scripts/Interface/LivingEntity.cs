using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float _startingHealth = 100f;

    public float _Health {  get; protected set; }
    public bool _IsDead {  get; protected set; }
    public event Action _OnDeath;

    [SerializeField] protected SpriteRenderer _sprite;

    Coroutine _cor;

    protected Animator _anim;

    protected virtual void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    protected virtual void OnEnable()
    {
        _IsDead = false;

        _Health = _startingHealth;
    }

    public virtual void Die()
    {
        _anim.SetTrigger("Death");
        if(_OnDeath != null) _OnDeath();

        _IsDead = true;
    }

    public virtual void OnDamage(float damage)
    {
        if (_IsDead) return;

        if (_cor == null)
        {
            _Health -= damage;
            if(_sprite != null)
                _cor = StartCoroutine(DamageColor());
        }

        if (_Health <= 0f)
        {
            Die();
            return;
        }
        _anim.SetTrigger("Damage");
    }

    [Header("ÇÇ°Ý ±ôºýÀÓ È½¼ö"), SerializeField] int _damageBlink; 
    IEnumerator DamageColor()
    {
        int blinkCnt = 0;
        while (blinkCnt < _damageBlink)
        {
            Color tmpcolor = _sprite.color;
            _sprite.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            _sprite.color = tmpcolor;
            yield return new WaitForSeconds(0.05f);
            blinkCnt++;
            yield return null;
        }
        _cor = null;
    }
}
