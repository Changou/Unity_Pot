using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMad : BossRogic
{
    [SerializeField] float _rot;

    public enum PATTERNTYPE
    {
        LINE,
        SPAWN,
        FALL,

        MAX
    }

    [SerializeField] GameObject[] _prefabs;

    protected override void Update() { }

    protected override IEnumerator Think()
    {
        while (!IsDead)
        {
            if (!isDelay)
            {
                int ran = Random.Range(0, 9);
                switch (ran)
                {
                    case 0:
                    case 1:
                    case 2: 
                    case 3:
                        _pattern[0].PatternOn(true);
                        break;
                    case 4:
                    case 5:
                    case 6:
                        _pattern[0].PatternOn(true);
                        break;
                    case 7:
                    case 8:
                        _pattern[0].PatternOn(true);
                        break;
                }
                StartCoroutine(CoolTime());
            }
            yield return null;
        }
    }

    public void Spawn(Vector3 pos, PATTERNTYPE type)
    {
        GameObject person = Instantiate(_prefabs[Random.Range(0,_prefabs.Length)]);
        person.transform.position = pos;
        if(type == PATTERNTYPE.SPAWN)
        {
            person.GetComponentInChildren<Person>()._isAttack = true;
            return;
        }

        Vector3 rot = Vector3.zero;

        if (type == PATTERNTYPE.LINE) 
        {
            if (pos.x > 0) rot.z = _rot;
            else rot.z = _rot * -1;
        }
        else 
            rot.z = _rot * 2;

        person.transform.rotation = Quaternion.Euler(rot);
        person.GetComponentInChildren<Person>()._isAttack = false;
    }

}
