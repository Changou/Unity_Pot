using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpAttack : MonoBehaviour
{
    [SerializeField] float _jumpPower;
    [SerializeField] Transform _target;
    [SerializeField] float _rushPower;

    Rigidbody2D _rb;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        JumpAttack();
    }

    private void Update()
    {
        if(_rb.velocity.y < 0)
        {
            Attack();
        }
    }

    void Attack()
    {
        Vector3 dir = _target.position - transform.position;
        _rb.AddForce(dir * _rushPower, ForceMode2D.Impulse);
    }

    void JumpAttack()
    {
        _rb.AddForce(Vector3.up * _jumpPower, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("d");
            _rb.velocity = Vector3.zero;
        }
    }
}
