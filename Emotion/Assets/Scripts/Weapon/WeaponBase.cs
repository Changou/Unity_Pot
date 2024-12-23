using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WEAPON_TYPE
{
    NORMAL,
    SWORD,
    ARROW,
    WAND,

    MAX
}

public class WeaponBase : MonoBehaviour
{
    [Header("[ ¹«±â ]")]
    [SerializeField] string _name;
    [SerializeField] int _atk;
    [SerializeField] int _exp;
    [SerializeField] int _lv;
    [SerializeField] WEAPON_TYPE _wType;
    [SerializeField] public float _delay;
    [SerializeField] public bool _isGet = false;

    public string _Name => _name;
    public int _ATK => _atk;
    public int _EXP
    {
        get { return _exp; }
        set { _exp = value; }
    }
    public int _LV
    {
        get { return _lv; }
        set { _lv = value; }
    }
    public WEAPON_TYPE _WType => _wType;

    public void Enforce()
    {
        _lv++;
        _exp = 0;
    }

    public void ExpUp(int i)
    {
        _exp = (_exp + i) > 100 ? 100 : _exp + i;
    }
}
