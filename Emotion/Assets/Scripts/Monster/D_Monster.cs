using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_Monster : MonoBehaviour
{
    public enum M_STATE
    {
        IDLE,
        MOVE,
        CHASE,
        DIED
    }

    [Header("몬스터"), SerializeField] M_STATE _state;
    [SerializeField] float _ActDelay = 3f;

    [Header("콜라이더")] 
    [SerializeField] CircleCollider2D _cirCollider;
    [SerializeField] CapsuleCollider2D _capCollider;

    Animator _anim;

    float _time = 0;

    private void Awake()
    {
        _state = M_STATE.IDLE;
        _anim = GetComponent<Animator>();
        _cirCollider = GetComponent<CircleCollider2D>();
        _capCollider = GetComponent<CapsuleCollider2D>();
        _capCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_state == M_STATE.IDLE || _state == M_STATE.MOVE)
        {
            AIMove();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_cirCollider.enabled)
            {
                _cirCollider.enabled = false;
                _capCollider.enabled = true;
                _state = M_STATE.CHASE;
            }
            else if (_capCollider.enabled)
            {
                Debug.Log("데미지");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _state = M_STATE.IDLE;
            _time = _ActDelay;
            _cirCollider.enabled = true;
            _capCollider.enabled = false;
        }
    }

    private void AIMove()
    {
        if (_state == M_STATE.MOVE)
        {
            _time -= 1 * Time.deltaTime;
            transform.position += (transform.localScale.x > 0 ? Vector3.left : Vector3.right)* Time.deltaTime;
            if (_time <= 0)
            {
                _state = M_STATE.IDLE;
                _time = _ActDelay;
                _anim.SetBool("Move", false);
            }

        }
        else if(_state == M_STATE.IDLE)
        {
            _time -= 1 * Time.deltaTime;
            if(_time <= 0)
            {
                _state = M_STATE.MOVE;
                _time = _ActDelay;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
                _anim.SetBool("Move", true);
            }
        }
    }
}
