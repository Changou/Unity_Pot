using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagicDamage : MonoBehaviour
{
    [SerializeField] float _damage;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable target = collision.GetComponent<IDamageable>();

        if(target != null )
        {
            target.OnDamage(_damage);
        }
    }
}
