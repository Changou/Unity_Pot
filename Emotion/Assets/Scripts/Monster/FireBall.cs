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
        transform.localPosition += dir.normalized * _speed * Time.deltaTime; 

        CheckAlive();
    }
    void CheckAlive()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        if (pos.x < -100f || pos.x > Screen.width + 100f)
            Destroy(gameObject);
    }
}
