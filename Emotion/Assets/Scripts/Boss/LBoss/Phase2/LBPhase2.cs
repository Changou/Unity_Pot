using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBPhase2 : MonoBehaviour
{
    public enum STATE
    {
        CHASE,
        ATTACK,
        SKILL,
        DIE,

        MAX
    }

    [Header("행동시간"), SerializeField] float _actTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rogic());
    }

    IEnumerator Rogic()
    {
        yield return null;
    }

    IEnumerator Stand()
    {
        yield return new WaitForSeconds(_actTime);
    }
}
