using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DIRECTION
{
    TOP,
    MIDDLE,
    BOTTOM
}

public class EnemySpawnComponent : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] float[] spawnP = { -0.4f, -1.8f, -3.3f };
    [SerializeField] float spawnRate = 2f;
    [SerializeField] int spawnCnt = 1;
    int spawnId = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.i.sEvents += () =>
        {
            StartCoroutine(SpawnEnemy());
        };
    }

    Vector3 SetPosition()
    {
        DIRECTION dir = (DIRECTION)Random.Range(0, 3);
        Vector3 pos = Vector3.zero;

        

        pos = new Vector3(transform.position.x, spawnP[(int)dir]);
        return pos;
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            if (spawnCnt % 5 == 0) spawnId = 1;
            if (spawnCnt % 10 == 0)
            {
                spawnId = 2;
                if(spawnRate > 0.6f) spawnRate -= 0.2f;
            }
            yield return new WaitForSeconds(spawnRate);
            EnemyPoolManager.i.UseEnemy(spawnId, SetPosition());
            spawnCnt++;
            spawnId = 0;
        }
    }
}
