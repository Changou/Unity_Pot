using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBMove : Pattern
{
    [SerializeField] Transform _target;
    [SerializeField] float _speed;

    Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = _target.position - transform.position;

        if(dir.x < 0)
        {
            Vector3 scale = Vector3.one;
            scale.x = 1;
            transform.localScale = scale;
            transform.localPosition += Vector3.left * _speed * Time.deltaTime;
        }
        else if(dir.x > 0)
        {
            Vector3 scale = Vector3.one;
            scale.x = -1;
            transform.localScale = scale;
            transform.localPosition += Vector3.right * _speed * Time.deltaTime;
        }
    }

    protected override IEnumerator Attack()
    {
        _anim.SetBool("IsRun", true);
        yield return new WaitForSeconds(_patternTime);
        _anim.SetBool("IsRun", false);
        PatternOn(false);
    }
}
