using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [Header("[ ½½·Ô UI ]")]
    [SerializeField] Text _name;
    [SerializeField] Slider _expS;
    [SerializeField] Button _button;

    [SerializeField] WeaponBase _weapon;

    [SerializeField] WEAPON_TYPE _slotType;

    public WEAPON_TYPE _SlotType => _slotType;

    private void OnEnable()
    {
        _expS.value = _weapon._EXP;
        if (_weapon._LV > 1)
            _name.text = _weapon._Name + "+" + _weapon._LV;
        if(_weapon._EXP < 100)
            _button.gameObject.SetActive(false);
    }

    public void Upgrade()
    {
        if(_weapon._EXP >= 100)
        {
            _weapon.Enforce();
            transform.GetComponentInParent<UIPanel_Enforce>().EnforceClose();
        }
    }
}
