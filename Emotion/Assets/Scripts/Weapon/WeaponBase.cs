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
    [SerializeField] protected float _delay;
    [SerializeField] bool _isGet = false;

    public string _Name => _name;
    public int _ATK => _atk;
    public int _EXP => _exp;
    public int _LV => _lv;
    public WEAPON_TYPE _WType => _wType;
    public bool _IsGet => _isGet;

    public void Enforce()
    {
        _lv++;
    }
}
