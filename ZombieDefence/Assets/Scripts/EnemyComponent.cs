using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ENEMY_TYPE
{
    ENEMY,
    STRONG,
    BOSS
}

public class EnemyComponent : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;

    [SerializeField] float[] speed = { -1.5f, -1f, -0.5f};
    [SerializeField] int[] hp = { 4, 10, 20};
    [SerializeField] int[] maxHp = { 4, 10, 20};
    [SerializeField] int[] atk = { 2, 5, 10};
    [SerializeField] int[] getScore = { 1, 5, 10};
    [SerializeField] ENEMY_TYPE t = ENEMY_TYPE.ENEMY;

    AudioSource sound;

    private void OnEnable()
    {
        hp[(int)t] = maxHp[(int)t];
        if (rb == null) return;
        else rb.velocity = new Vector2(speed[(int)t], 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        if (gameObject.name.Contains("Strong")) t = ENEMY_TYPE.STRONG;
        else if (gameObject.name.Contains("Boss")) t = ENEMY_TYPE.BOSS;
        else t = ENEMY_TYPE.ENEMY;

        rb.velocity = new Vector2(speed[(int)t], 0);
    }
    public void TakeDamage(int dmg)
    {
        hp[(int)t] -= dmg;
        sound.Play();
        StartCoroutine(SetColor());

        if (hp[(int)t] <= 0)
        {
            GameManager.i.AddScore(getScore[(int)t]);
            DestroyEnemy();
        }
    }
    IEnumerator SetColor()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    void DestroyEnemy()
    {
        gameObject.SetActive(false);
        sr.color = Color.white;
        EnemyPoolManager.i.ReturnEnemy(gameObject, (int)t);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("House"))
        {
            collision.GetComponent<HouseComponent>().TakeDamage(atk[(int)t]);
            DestroyEnemy();
        }
        else if(collision.CompareTag("EndLine"))
        {
            DestroyEnemy();
        }
    }
}
