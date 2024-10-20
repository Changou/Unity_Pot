using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float _startingHealth = 100f;

    public float Health {  get; protected set; }
    public bool IsDead {  get; protected set; }
    public event Action OnDeath;

    [SerializeField] protected SpriteRenderer _sprite;

    Coroutine _cor;

    protected virtual void OnEnable()
    {
        IsDead = false;

        Health = _startingHealth;
    }

    public virtual void Die()
    {
        if(OnDeath != null) OnDeath();

        IsDead = true;
    }

    public virtual void OnDamage(float damage)
    {
        if (IsDead) return;

        if (_cor == null)
        {
            Health -= damage;
            if(_sprite != null)
                _cor = StartCoroutine(DamageColor());
        }

        if (Health <= 0f)
            Die();
    }

    [Header("�ǰ� ������ Ƚ��"), SerializeField] int _damageBlink; 
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
