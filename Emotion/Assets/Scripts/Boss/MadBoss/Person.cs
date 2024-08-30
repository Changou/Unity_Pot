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
    
    public bool _isAttack;

    Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = FindAnyObjectByType<Player>().transform.parent;
    }

    private void Start()
    {
        Destroy(transform.parent.gameObject, 3f);
    }

    private void Update()
    {
        if (_isAttack)
        {
            Direction();
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
        _rb.AddRelativeForce(Vector2.right * _chaseSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }

    void Direction()
    {
        Vector3 dir = _target.position - transform.position;

        Vector3 scale = transform.parent.localScale;
        if ((dir.x > 0 && transform.parent.localScale.x < 0) ||
                (dir.x < 0 && transform.parent.localScale.x > 0))
            scale.x *= -1;

        transform.parent.localScale = scale;
    }

    void FlyAttack()
    {
        
        _rb.AddRelativeForce(Vector2.up * _speed , ForceMode2D.Force);
    }
}
