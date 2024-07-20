using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHp : UIBase
{
    [Header("�÷��̾� ����")]
    [SerializeField] PlayerInfo _playerInfo;

    Slider hp;

    private void Awake()
    {
        hp = GetComponent<Slider>();
    }

    private void FixedUpdate()
    {
        hp.value = _playerInfo._HP;
    }
}
