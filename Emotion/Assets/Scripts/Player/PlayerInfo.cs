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
    [SerializeField] int atk;
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] float dashPower;
    [SerializeField] float dashTime;
    [SerializeField] float attackDelay;
    [SerializeField] WEAPON weaponState;

    public float _Speed => speed;
    public float _JumpPower => jumpPower;
    public float _DashTime => dashTime;
    public float _AttackDelay => attackDelay;
    public WEAPON _WeaponState => weaponState;

    float defalutSpeed;

    private void Awake()
    {
        defalutSpeed = speed;
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
        weaponState++;
        if(weaponState == WEAPON.MAX) weaponState = WEAPON.NORMAL;
    }
    public void WeaponDown()
    {
        if (weaponState == WEAPON.NORMAL) weaponState = WEAPON.MAX;
        weaponState--;
    }

}
