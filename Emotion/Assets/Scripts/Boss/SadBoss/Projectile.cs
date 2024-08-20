using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            IDamageable damage = collision.GetComponent<IDamageable>();

            if (damage != null)
            {
                damage.OnDamage(10f);
            }
            Destroy(gameObject);
        }
    }
}
