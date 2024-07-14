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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }
    public void Direction(float x)
    {
        if (x < 0) trans.rotation = Quaternion.Euler(0, 180f, 0);
        else if (x > 0) trans.rotation = Quaternion.Euler(0, 0, 0);
    }
}
