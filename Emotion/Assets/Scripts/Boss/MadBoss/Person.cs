using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Person : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _speed;
    [SerializeField] float _chaseSpeed;
    [SerializeField] float _life;
    [SerializeField] float _damage;

    public bool _isAttack;

    Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = FindObjectOfType<Player>().transform.parent;
    }

    private void Start()
    {
       Destroy(transform.parent.gameObject, 5f);
    }

    private void Update()
    {
        if (_isAttack)
        {
            //Direction();
            ChaseAttack();
        }
        else
        {
            FlyAttack();
        }
        CheckAlive();
    }

    void CheckAlive()
    {
        Vector3 worldPos = transform.position;

        worldPos.z = 10f;

        Vector2 pos = Camera.main.WorldToScreenPoint(worldPos);

        if ((pos.y < -30) ||
            (_rb.velocity.x < 0 && pos.x < -30) ||
            (_rb.velocity.x > 0 && pos.x > Screen.width + 30))
            Destroy(transform.parent.gameObject);
    }

    void ChaseAttack()
    {
        Vector3 dir = _target.position - transform.parent.position;
        dir.y = 0; dir.z = 0;
        if ((dir.x > 0 && transform.parent.localScale.x < 0) ||
            (dir.x < 0 && transform.parent.localScale.x > 0))
        {
            Vector3 tmp = transform.parent.localScale;
            tmp.x *= -1;
            transform.parent.localScale = tmp;
        }
        transform.parent.position += dir.normalized * _chaseSpeed * Time.deltaTime;
        //_rb.AddRelativeForce(Vector2.right * Time.deltaTime * _chaseSpeed, ForceMode2D.Impulse);
    }

    void FlyAttack()
    {
        _rb.AddRelativeForce(Vector2.up * _speed , ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable target = collision.GetComponent<IDamageable>();
        if (target != null)
        {
            target.OnDamage(_damage);
        }
    }

    private void OnDisable()
    {
        BossMad mad = FindObjectOfType<BossMad>();
        if (mad != null)
        {
            mad.OnDamage(_life);
        }
    }
}
