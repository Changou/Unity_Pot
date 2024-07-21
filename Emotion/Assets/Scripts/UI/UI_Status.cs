using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Status : UIBase
{
    [Header("�÷��̾� ����")]
    [SerializeField] PlayerInfo _playerInfo;
    [SerializeField] Slider hp;

    [Header("[ ���� �̹��� ]")]
    [SerializeField] Sprite[] _weaponImgs;
    [SerializeField] Image _weaponIcon;

    private void FixedUpdate()
    {
        hp.value = _playerInfo._HP;
        Setting();
    }
    void Setting()
    {
        _weaponIcon.sprite = _weaponImgs[(int)_playerInfo._WeaponState];
    }
}
