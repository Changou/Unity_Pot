using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [SerializeField] Pattern _my;
    [SerializeField] protected float _patternTime;

    protected virtual void OnEnable()
    {
        transform.localScale = Vector3.one;
        StartCoroutine(Attack());
    }

    protected virtual IEnumerator Attack(){ yield return null; }

    public void PatternOn(bool isOn)
    {
        _my.enabled = isOn;
    }
}
