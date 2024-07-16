using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSword : MonoBehaviour
{
    [Header("대검")]
    [SerializeField] int atk;
    [SerializeField] float attackDelay;

    [Header("대검액션")]
    [SerializeField] Animator anim;
    [SerializeField] Transform trans;

    public float _AttackDelay => attackDelay;

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }
}
