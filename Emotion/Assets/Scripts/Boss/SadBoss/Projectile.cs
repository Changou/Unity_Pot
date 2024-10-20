using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MagicDamage
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
