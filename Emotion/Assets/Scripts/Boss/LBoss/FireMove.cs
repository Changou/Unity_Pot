using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class FireMove : MonoBehaviour
{
    float _speed;
    public int dir;

    private void OnEnable()
    {
        _speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if(dir == 0)
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;
        }
        else
            transform.position += Vector3.right * _speed * Time.deltaTime;
        CheckAlive();
    }

    void CheckAlive()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        if (pos.x < -100f || pos.x > Screen.width + 100f)
            Destroy(gameObject);
    }
}
