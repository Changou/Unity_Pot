using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [SerializeField] Pattern _my;
    [SerializeField] protected float _patternTime;

    public void PatternOn(bool isOn)
    {
        _my.enabled = isOn;
    }
}
