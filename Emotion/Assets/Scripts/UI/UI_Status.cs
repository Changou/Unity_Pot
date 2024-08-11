using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Status : UIBase
{
    [Header("플레이어 정보")]
    [SerializeField] LivingEntity _playerHp;
    [SerializeField] PlayerInfo _playerInfo;
    [SerializeField] Slider hp;

    [Header("[ 무기 이미지 ]")]
    [SerializeField] Sprite[] _weaponImgs;
    [SerializeField] Image _weaponIcon;

    private void Awake()
    {
        hp.maxValue = _playerHp._startingHealth;
        hp.value = _playerHp.Health;
    }

    private void Update()
    {
        hp.value = _playerHp.Health;
        Setting();
    }
    void Setting()
    {
        _weaponIcon.sprite = _weaponImgs[(int)_playerInfo._WeaponState];
    }
}
