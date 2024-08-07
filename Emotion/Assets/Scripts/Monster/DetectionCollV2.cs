using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionCollV2 : MonoBehaviour
{
    [SerializeField] bool _detection = false;
    [SerializeField] Transform _target;

    public bool Detection 
    { 
        get { return _detection;}
    }

    public Transform Target
    {
        get { return _target; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _detection = true;
            _target = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _detection = false;
            _target = null;
        }
    }
}
