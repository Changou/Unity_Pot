using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("[ ∏ÛΩ∫≈Õ ]")]
    [SerializeField] int hp;
    [SerializeField] int atk;
    [SerializeField] float speed;

    Rigidbody2D rb;
    public int nextMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("Think", 2);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(nextMove, rb.velocity.y);
    }
    void Think()
    {
        nextMove = Random.Range(-1, 2);

        Invoke("Think", 2);
    }
}
