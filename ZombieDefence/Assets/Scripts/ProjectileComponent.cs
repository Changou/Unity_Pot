using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    [SerializeField] float speed = 10f;     //�Ѿ� �ӵ�
    [SerializeField] int dmg = 2;           //���ݷ�

    Rigidbody2D rb;


    public void Move()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(speed, 0); ;
        Invoke("DestroyBullet", 3);
    }

    void DestroyBullet()
    {
        BulletPoolManager.i.ReturnBullet(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyComponent>().TakeDamage(dmg);
        }

        CancelInvoke("DestroyBullet");

        DestroyBullet();
    }


}
