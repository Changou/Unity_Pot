using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadPattern1 : Pattern
{
    [Header("일직선 공격")]
    [SerializeField] protected Transform _target;
    [SerializeField] float _delay;
    [SerializeField] float _lineWidth;
    LineRenderer _warningLine;

    int dir;

    private void Awake()
    {
        _warningLine = GetComponent<LineRenderer>();
    }

    protected virtual void OnEnable()
    {
        dir = Random.Range(0, 2) == 0 ? -1 : 1;

        Vector3 lineStartPos = Vector3.zero;
        lineStartPos.x = Screen.width + 50;

        lineStartPos = Camera.main.ScreenToWorldPoint(lineStartPos);
        lineStartPos.y = _target.GetChild(0).transform.position.y;
        lineStartPos.z = 0;
        Vector3 lineEndPos = lineStartPos;
        lineEndPos.x *= -1;

        if(dir < 0)
            StartCoroutine(Warning(BossMad.PATTERNTYPE.LINE, lineStartPos, lineEndPos));
        else
            StartCoroutine(Warning(BossMad.PATTERNTYPE.LINE, lineEndPos, lineStartPos));
    }

    protected IEnumerator Warning(BossMad.PATTERNTYPE pType, Vector3 startPos, Vector3 endPos)
    {
        
        _warningLine.startWidth = _lineWidth;
        _warningLine.endWidth = _lineWidth;
        _warningLine.SetPosition(0, startPos);
        _warningLine.SetPosition(1, endPos);

        _warningLine.enabled = true;
        yield return new WaitForSeconds(_delay);
        _warningLine.enabled = false;
        transform.GetComponent<BossMad>().Spawn(startPos, pType);
        PatternOn(false);
    }
}
