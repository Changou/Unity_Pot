using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : LivingEntity
{
    [SerializeField] Collider2D _bossColl;
    [SerializeField] protected Slider _shieldSlider;

    [Header("½¯µå ·®")]
    [SerializeField] protected int _maxShield;

    protected virtual void Start()
    {
        _shieldSlider.maxValue = _maxShield;
        _shieldSlider.minValue = 0;
        _shieldSlider.value = _startingHealth;
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        _shieldSlider.value = _Health;
    }

    public override void Die()
    {
        _bossColl.enabled = true;
        gameObject.SetActive(false);
    }
}
