using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : Pattern
{
    [SerializeField] Transform _target;
    [SerializeField] GameObject _danger;
    [SerializeField] GameObject _fireWall;
    [SerializeField] float _dangerTime;
    [SerializeField] float _lifeTime;

    private void OnEnable()
    {
        StartCoroutine(Targeting());
    }

    IEnumerator Targeting()
    {
        GameObject danger = Instantiate(_danger);

        danger.transform.position = _target.position;
        yield return new WaitForSeconds(_dangerTime);
        Vector3 pos = danger.transform.position;
        Destroy(danger);

        GameObject fire = Instantiate(_fireWall);
        pos.y = 2;
        fire.transform.position = pos;
        Destroy(fire, _lifeTime);
    }
}
