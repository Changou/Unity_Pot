using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBoss : UIBase
{
    [SerializeField] Slider _hpSlider;
    [SerializeField] Text _bossName;
    [SerializeField] LivingEntity _boss;

    private void Start()
    {
        _hpSlider.maxValue = _boss._startingHealth;
        _hpSlider.value = _hpSlider.maxValue;
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        _hpSlider.value = _boss.Health;
    }

}
