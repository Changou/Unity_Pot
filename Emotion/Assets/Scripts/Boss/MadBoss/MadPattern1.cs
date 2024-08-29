using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadPattern1 : Pattern
{
    [Header("일직선 공격")]
    [SerializeField] Transform _target;
    [SerializeField] float _delay;
    [SerializeField] float _lineWidth;
    LineRenderer _warningLine;

    int dir;

    private void Awake()
    {
        _warningLine = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        dir = Random.Range(0, 2f) == 0 ? -1 : 1;

        Vector3 lineStartPos = Vector3.zero;
        lineStartPos.x = Screen.width + 50;
        lineStartPos = Camera.main.ScreenToWorldPoint(lineStartPos);
        lineStartPos.y = _target.position.y;
        lineStartPos.z = 0;
        Vector3 lineEndPos = lineStartPos;
        lineEndPos.x *= -1;

        if(dir < 0)
            StartCoroutine(Warning(lineStartPos, lineEndPos));
        else
            StartCoroutine(Warning(lineEndPos, lineStartPos));
    }

    IEnumerator Warning(Vector3 startPos, Vector3 endPos)
    {
        _warningLine.startWidth = _lineWidth;
        _warningLine.endWidth = _lineWidth;
        _warningLine.SetPosition(0, startPos);
        _warningLine.SetPosition(1, endPos);

        yield return new WaitForSeconds(_delay);
        _warningLine.enabled = false;
    }
}
