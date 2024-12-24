using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIClear : UIBase
{
    [Header("������")]
    [SerializeField] float _delay;
    [SerializeField] BOSS_TYPE _bt;

    [Header("���� �̹���")]
    [SerializeField] Sprite[] _weaponImg;
    [SerializeField] GameObject _getWeapon;

    [SerializeField] PlayerInfo _playerInfo;

    protected virtual void OnEnable()
    {
        if (_playerInfo.IsWeaponGet(((int)_bt) + 1))
            _getWeapon.SetActive(false);
        else
        {
            _getWeapon.SetActive(true);
            SettingGetMessage();
        }

        StartCoroutine("ClearP");
    }

    void SettingGetMessage()
    {
        _getWeapon.GetComponentInChildren<Image>().sprite = _weaponImg[(int)_bt];
        string s = "";
        switch ((int)_bt)
        {
            case 0:
                s = "���";
                _playerInfo.WeaponGet(WEAPON.SWORD);
                break;
            case 1:
                s = "Ȱ";
                _playerInfo.WeaponGet(WEAPON.ARROW);
                break;
            case 2:
                s = "�ϵ�";
                _playerInfo.WeaponGet(WEAPON.WAND);
                break;
        }
        s += "��(��) ȹ���Ͽ����ϴ�.";
        _getWeapon.GetComponentInChildren<Text>().text = s;
        
    }

    protected IEnumerator ClearP()
    {
        yield return new WaitForSeconds(_delay);
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
