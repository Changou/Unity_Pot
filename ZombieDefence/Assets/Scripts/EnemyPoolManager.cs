using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager i;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] int initCnt = 10;

    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        CreateEnemy(initCnt);
    }

    void CreateEnemy(int val = 1, int id = 0)
    {
        if(transform.childCount == 0)
        {
            for(int i =0;i<enemyPrefabs.Length;i++)
            {
                GameObject temp = new GameObject();
                temp.transform.SetParent(transform);
            }
        }
        if (val == 1) Instantiate(enemyPrefabs[id], transform.GetChild(id));
        else
        {
            for(int i = 0; i < enemyPrefabs.Length; i++)
            {
                for(int j = 0; j < val; j++)
                {
                    Instantiate(enemyPrefabs[i], transform.GetChild(i));
                }
            }
        }
    }

    public void UseEnemy(int id, Vector3 p)
    {
        if (transform.GetChild(id).childCount == 0) CreateEnemy(1, id);

        GameObject e = transform.GetChild(id).GetChild(0).gameObject;
        if(e.GetComponent<SpriteRenderer>() == null)
        {
            if (p.y == -0.4f) e.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
            else if (p.y == -1.8f) e.GetComponentInChildren<SpriteRenderer>().sortingOrder = 3;
            else if (p.y == -3.3f) e.GetComponentInChildren<SpriteRenderer>().sortingOrder = 5;
        }
        else
        {
            if (p.y == -0.4f) e.GetComponent<SpriteRenderer>().sortingOrder = 1;
            else if (p.y == -1.8f) e.GetComponent<SpriteRenderer>().sortingOrder = 3;
            else if (p.y == -3.3f) e.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
        e.transform.position = p;
        e.transform.SetParent(null);
        e.gameObject.SetActive(true);
    }
    public void ReturnEnemy(GameObject e, int id)
    {
        e.transform.SetParent(transform.GetChild(id));
    }
}
