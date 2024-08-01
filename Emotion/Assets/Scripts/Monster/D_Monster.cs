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

    [Header("∏ÛΩ∫≈Õ"), SerializeField] M_STATE _state;
    [SerializeField] float _ActDelay = 3f;
    [SerializeField] float _speed = 2f;

    [Header("≈∏∞Ÿ")] Transform _target;

    [SerializeField] float _detectionDis = 5f;

    Animator _anim;

    float _time = 0;

    private void Awake()
    {
        _state = M_STATE.IDLE;
        _anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(_state == M_STATE.IDLE || _state == M_STATE.MOVE)
        {
            AIMove();
        }
        else if(_state == M_STATE.CHASE)
        {
            Chase();
            _anim.SetBool("Move", true);
        }
    }

    public void SetState(M_STATE state, Transform target = null)
    {
        _state = state;
        _target = target;
    }

    void Chase()
    {
        Vector3 dir = _target.position - transform.position;
        dir.y = 0; dir.z = 0;
        if((dir.x > 0 && transform.localScale.x > 0) ||
            (dir.x < 0 && transform.localScale.x < 0))
        {
            Vector3 tmp = transform.localScale;
            tmp.x *= -1;
            transform.localScale = tmp;
        }
        transform.position += dir.normalized * _speed * Time.deltaTime;
    }


    private void AIMove()
    {
        if (_state == M_STATE.MOVE)
        {
            _time -= 1 * Time.deltaTime;
            transform.position += (transform.localScale.x > 0 ? Vector3.left : Vector3.right) * Time.deltaTime;
            if (_time <= 0)
            {
                _state = M_STATE.IDLE;
                _time = _ActDelay;
                return;
            }
            _anim.SetBool("Move", true);
        }
        else if(_state == M_STATE.IDLE)
        {
            _time -= 1 * Time.deltaTime;
            if(_time <= 0)
            {
                _state = M_STATE.MOVE;
                _time = _ActDelay;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
                return;
            }
            _anim.SetBool("Move", false);
        }
    }
}
