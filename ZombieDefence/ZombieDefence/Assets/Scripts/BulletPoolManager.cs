using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager i;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int initBulletCount = 12;

    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateBullet(initBulletCount);
    }

    void CreateBullet(int cnt = 1)
    {
        for(int i = 0; i < cnt; i++)
        {
            Instantiate(bulletPrefab, transform);
        }
    }

    public void UseBullet(Vector3 p, Quaternion rot)
    {
        if (transform.childCount == 0) CreateBullet();      //풀안에 총알이 없다면 새로 생성

        ProjectileComponent b = transform.GetChild(0).GetComponent<ProjectileComponent>();

        b.transform.position = p;
        b.transform.rotation = rot;
        b.gameObject.SetActive(true);
        b.transform.parent = null;
        b.Move();
    }
    public void ReturnBullet(GameObject b)
    {
        b.SetActive(false);
        b.transform.SetParent(transform);
    }
}
