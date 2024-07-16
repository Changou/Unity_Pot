using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongSword : MonoBehaviour
{
    [Header("���")]
    [SerializeField] int atk;
    [SerializeField] float attackDelay;

    [Header("��˾׼�")]
    [SerializeField] Animator anim;
    [SerializeField] Transform trans;

    public float _AttackDelay => attackDelay;

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }
}
