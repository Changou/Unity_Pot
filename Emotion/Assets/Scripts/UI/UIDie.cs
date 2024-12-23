using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDie : UIClear
{
    protected override void OnEnable()
    {
        StartCoroutine("ClearP");
    }
}
