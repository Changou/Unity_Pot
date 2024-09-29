using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : LivingEntity
{
    [SerializeField] Collider2D _bossColl;
    [SerializeField] Slider _shieldSlider;

    [Header("½¯µå ·®")]
    [SerializeField] int _maxShield;
    [SerializeField] int _shieldNum;

    private void Start()
    {
        _shieldSlider.maxValue = _maxShield;
        _shieldSlider.minValue = 0;
        _shieldSlider.value = _shieldNum;
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        _shieldSlider.value = Health;
    }

    public override void Die()
    {
        _bossColl.enabled = true;
        gameObject.SetActive(false);
    }

    public void OnShield()
    {

    }
}
