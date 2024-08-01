using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionColl : MonoBehaviour
{
    public D_Monster _monster;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _monster.SetState(D_Monster.M_STATE.CHASE, collision.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _monster.SetState(D_Monster.M_STATE.IDLE);
        }
    }
}
