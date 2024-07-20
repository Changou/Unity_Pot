using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : UIBase
{
    [Header("[ 무기 이미지 ]")]
    [SerializeField] Sprite[] _weaponImgs;
    [SerializeField] PlayerInfo _playerInfo;
    [SerializeField] Image _weaponIcon;

    private void Awake()
    {
        _weaponIcon = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        Setting();
    }

    void Setting()
    {
        _weaponIcon.sprite = _weaponImgs[(int)_playerInfo._WeaponState];
    }
}
