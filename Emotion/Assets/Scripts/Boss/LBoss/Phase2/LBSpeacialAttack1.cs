using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LBSpeacialAttack1 : Pattern
{
    [SerializeField] Transform _myPos;
    [SerializeField] Animator _anim;
    [SerializeField] GameObject _prefab;

    private void OnEnable()
    {
        SwordEnergy();
    }

    void SwordEnergy()
    {
        TransPosition();
        StartCoroutine(RandomDelay());
    }

    IEnumerator RandomDelay()
    {
        yield return new WaitForSeconds(0.5f);
        int ran = Random.Range(0, 2);
        Vector3 scale = new Vector3(ran > 0 ? 1 : -1, 1, 1);
        _myPos.localScale = scale;
        _anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.3f);

        GameObject sEnergy = Instantiate(_prefab);
        Vector3 pScale = sEnergy.transform.localScale;
        Vector3 pos = sEnergy.transform.position;
        if (_myPos.localScale.x < 0)
        {
            pScale.x = 1;
            pos.x = 2;
        }
        else
        {
            pScale.x = -1;
            pos.x = -2f;
        }
        sEnergy.transform.localScale = pScale;
        sEnergy.transform.position = pos;

        yield return new WaitForSeconds(0.5f);

        yield return new WaitForSeconds(_patternTime);
        PatternOn(false);
    }

    void TransPosition()
    {
        Vector3 pos = _myPos.position;
        pos.x = 0;
        _myPos.position = pos;
    }
}
