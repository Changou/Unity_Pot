using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearProduction : PhaseProduction
{

    [SerializeField] float _delay;
    [SerializeField] FadeInOut _fade;

    private void OnEnable()
    {
        StartCoroutine("GoEnding");   
    }

    IEnumerator GoEnding()
    {
        yield return new WaitForSeconds(_delay);

        _fade.FadeOut("Ending");
    }
}
