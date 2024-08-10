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

        Health -= damage;

        if (Health <= 0f)
            Die();
    }
}
