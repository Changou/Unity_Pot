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
    protected override void DieAct()
    {
        GameObject[] p = GameObject.FindGameObjectsWithTag("Projectile");
        for(int i = 0; i< p.Length; i++)
        {
            Destroy(p[i]);
        }

        GameManager._Inst.Pause();
        UIManager._Inst.Clear_UI();
    }
}
