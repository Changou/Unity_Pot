using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBMove : Pattern
{
    [SerializeField] Transform _target;
    [SerializeField] Transform _myPos;
    [SerializeField] float _speed;

    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(PatternOff());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = _target.position - _myPos.position;

        if(dir.x < 0)
        {
            Vector3 scale = Vector3.one;
            scale.x = 1;
            transform.localScale = scale;
            _myPos.position += Vector3.left * _speed * Time.deltaTime;
        }
        else if(dir.x > 0)
        {
            Vector3 scale = Vector3.one;
            scale.x = -1;
            transform.localScale = scale;
            _myPos.position += Vector3.right * _speed * Time.deltaTime;
        }
    }

    IEnumerator PatternOff()
    {
        yield return new WaitForSeconds(_patternTime);
        PatternOn(false);
    }
}
