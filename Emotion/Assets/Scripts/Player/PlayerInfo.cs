using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum WEAPON
{
    NORMAL,
    SWORD,
    ARROW,
    WAND,

    MAX
}
public class PlayerInfo : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] float dashPower;
    [SerializeField] float dashTime;
    [SerializeField] WEAPON weaponState;
    [SerializeField] WeaponBase[] _weapon;

    public float _Speed => speed;
    public float _JumpPower => jumpPower;
    public float _DashTime => dashTime;
    public WEAPON _WeaponState => weaponState;

    float defalutSpeed;

    public int WeaponExp(WEAPON w)
    {
        return _weapon[(int)w]._EXP;
    }

    public int WeaponLevel(WEAPON w)
    {
        return _weapon[(int)w]._LV;
    }

    public void WeaponExpUp(int exp)
    {
        _weapon[(int)weaponState].ExpUp(exp);
    }

    public float WeaponDelay()
    {
        return _weapon[(int)weaponState]._delay;
    }

    public bool IsWeaponGet(int w)
    {
        return _weapon[w]._isGet;
    }

    public bool WeaponAllGet()
    {
        foreach(var weapon in _weapon)
        {
            if (!weapon._isGet)
            {
                return false;
            }
        }
        return true;
    }

    private void Awake()
    {
        defalutSpeed = speed;

        LoadGameInfo();
    }

    void LoadGameInfo()
    {
        _weapon[(int)WEAPON.NORMAL]._EXP = PlayerPrefs.GetInt("NormalEXP");
        _weapon[(int)WEAPON.SWORD]._EXP = PlayerPrefs.GetInt("SwordEXP");
        _weapon[(int)WEAPON.ARROW]._EXP = PlayerPrefs.GetInt("BowEXP");
        _weapon[(int)WEAPON.WAND]._EXP = PlayerPrefs.GetInt("WandEXP");

        _weapon[(int)WEAPON.NORMAL]._LV = PlayerPrefs.GetInt("NormalLevel");
        _weapon[(int)WEAPON.SWORD]._LV = PlayerPrefs.GetInt("SwordLevel");
        _weapon[(int)WEAPON.ARROW]._LV = PlayerPrefs.GetInt("BowLevel");
        _weapon[(int)WEAPON.WAND]._LV = PlayerPrefs.GetInt("WandLevel");

        _weapon[1]._isGet = Convert.ToBoolean(PlayerPrefs.GetInt("Sword"));
        _weapon[2]._isGet = Convert.ToBoolean(PlayerPrefs.GetInt("Bow"));
        _weapon[3]._isGet = Convert.ToBoolean(PlayerPrefs.GetInt("Wand"));
    }

    public void OnDash()
    {
        speed = dashPower;
    }
    public void OffDash()
    {
        speed = defalutSpeed;
    }

    public void WeaponUp()
    {
        weaponState = ++weaponState < WEAPON.MAX ? weaponState : WEAPON.NORMAL;
        if (!_weapon[(int)weaponState]._isGet)
        {
            WeaponUp();
        }
    }
    public void WeaponDown()
    {
        weaponState = --weaponState >= WEAPON.NORMAL ? weaponState : WEAPON.MAX - 1;
        if (!_weapon[(int)weaponState]._isGet) 
        {
            WeaponDown();
        }
    }

    public void StartGameInfo()
    {
        _weapon[(int)WEAPON.NORMAL]._EXP = 0;
        _weapon[(int)WEAPON.SWORD]._EXP = 0;
        _weapon[(int)WEAPON.ARROW]._EXP = 0;
        _weapon[(int)WEAPON.WAND]._EXP = 0;

        _weapon[(int)WEAPON.NORMAL]._LV = 1;
        _weapon[(int)WEAPON.SWORD]._LV = 1;
        _weapon[(int)WEAPON.ARROW]._LV = 1;
        _weapon[(int)WEAPON.WAND]._LV = 1;

        _weapon[1]._isGet = false;
        _weapon[2]._isGet = false;
        _weapon[3]._isGet = false;
    }
}
