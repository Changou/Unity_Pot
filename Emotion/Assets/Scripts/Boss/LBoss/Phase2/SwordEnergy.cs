using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnergy : MagicDamage
{
    [SerializeField] float _speed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.x > 0)
            transform.position += Vector3.right * _speed * Time.deltaTime;
        else
            transform.position += Vector3.left * _speed * Time.deltaTime;
    }
}
