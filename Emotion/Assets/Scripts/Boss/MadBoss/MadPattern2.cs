using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadPattern2 : MadPattern1
{
    protected override void OnEnable()
    {
        Vector3 lineStartPos = Vector3.zero;
        lineStartPos.y = Screen.height + 50;

        lineStartPos = Camera.main.ScreenToWorldPoint(lineStartPos);
        lineStartPos.x = _target.GetChild(0).transform.position.x;
        lineStartPos.z = 0;
        Vector3 lineEndPos = lineStartPos;
        lineEndPos.y = lineStartPos.y * -1;

        StartCoroutine(Warning(BossMad.PATTERNTYPE.FALL, lineStartPos, lineEndPos));
    }
}
