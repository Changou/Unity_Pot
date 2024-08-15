using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpAttack : Pattern
{
    [SerializeField] float _jumpPower;
    [SerializeField] Transform _target;
    [SerializeField] float _rushPower;
    [SerializeField] GameObject _arrow;
    [SerializeField] float _omenDelay = 1f;

    Rigidbody2D _rb;

    Vector3 dir;

    bool isAttack = false;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        isAttack = false;
        StartCoroutine(Omen());
    }

    private void Update()
    {
        if(_rb.velocity.y < 0 && !isAttack)
        {
            Attack();
        }
    }

    IEnumerator Omen()
    {
        _arrow.SetActive(true);
        yield return new WaitForSeconds(_omenDelay);
        _arrow.SetActive(false);
        JumpAttack();
    }

    void Attack()
    {
        dir = _target.position - transform.position;
        isAttack = true;
        _rb.AddForce(dir * _rushPower, ForceMode2D.Impulse);
    }

    void JumpAttack()
    {
        _rb.AddForce(Vector3.up * _jumpPower, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _rb.velocity = Vector3.zero;
            PatternOn(false);
        }
    }
}
