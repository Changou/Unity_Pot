using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPoint : MonoBehaviour
{
    [SerializeField] WeaponBase _weapon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damage = collision.GetComponent<IDamageable>();

        if(damage != null)
        {
            damage.OnDamage(_weapon._ATK);
        }
    }
}
