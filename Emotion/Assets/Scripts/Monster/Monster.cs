using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("[ 몬스터 ]")]
    [SerializeField] int hp;
    [SerializeField] int atk;
    [SerializeField] float speed;

    Rigidbody2D rb;
    public int nextMove;
    Animator _anim;

    [Header("딜레이"), SerializeField] float _delay;
    float _delayTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hp <= 0) return;

        Think();
        rb.velocity = new Vector2(nextMove, rb.velocity.y);
    }
    void Think()
    {
        if (_delayTime <= 0)
        {
            nextMove = Random.Range(-1, 2);
            _delayTime = _delay;
        }
        else
            Delay();
    }

    void Delay()
    {
        _delayTime -= 1f * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            --hp;
            if (hp <= 0)
            {
                _anim.SetTrigger("Died");
                transform.GetComponent<Collider2D>().enabled = false;
            }
            else
                _anim.SetTrigger("Damage");
        }
    }

    public void Died()
    {
        Destroy(gameObject);
    }
}
