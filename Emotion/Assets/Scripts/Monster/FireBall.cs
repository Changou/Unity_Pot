using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] float _speed = 3f;

    private void Awake()
    {
        Destroy(gameObject, 3f);
    }

    Vector3 dir;

    public Vector3 Dir
    {
        set { dir = value; }
    }

    private void Update()
    {
        transform.localPosition += dir * _speed * Time.deltaTime; 
    }
}
