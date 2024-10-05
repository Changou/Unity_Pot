using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : Pattern
{
    [SerializeField] Transform _target;
    [SerializeField] GameObject _prefab;

    // Start is called before the first frame update
    protected override void OnEnable()
    {
        Attack();
    }

    void Attack()
    {
        Transform tmp = _target;
        float x = tmp.parent.position.x - transform.position.x;
        float y = tmp.position.y - transform.position.y;

        GameObject fire = Instantiate(_prefab, transform.parent);
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

        fire.transform.rotation = Quaternion.Euler(0, 0, angle + 180);

        fire.GetComponent<FireBall>().Dir = new Vector3(x,y,0);
        fire.transform.SetParent(null);

        PatternOn(false);
    }
}
