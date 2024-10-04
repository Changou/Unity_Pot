using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldP3 : Shield
{


    protected override void Start()
    {
        _shieldSlider.maxValue = _maxShield;
        _shieldSlider.minValue = 0;
        _shieldSlider.value = 0;
        gameObject.SetActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _shieldSlider.value = Health;
    }

    public override void Die()
    {
        base.Die();
    }
}
