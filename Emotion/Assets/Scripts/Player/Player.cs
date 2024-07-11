using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum WEAPON
{
    NORMAL,
    SWORD,
    ARROW,
    WAND
}

public class Player : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] float dashPower;
    [SerializeField] float dashTime;
    [SerializeField] Vector2 colOffset;
    [SerializeField] float attackDelay;
    [SerializeField] WEAPON weaponState; 

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    Collider2D col;

    [SerializeField] bool isRight = true;
    bool isJump = false;
    bool isDash = false;
    bool isAttack = false;
    bool isShot = false;
    [SerializeField] bool isMove = false;

    [Header("활")]
    [SerializeField] Transform bow;
    [SerializeField] GameObject arrow;
    [SerializeField] float shotDelay;

    float defaultSpeed;
    Vector2 mouse;
    float angle;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Dash();
        Attack();
    }

    void Attack()
    {
        if (weaponState == WEAPON.NORMAL)
        {
            if (Input.GetMouseButtonDown(0) && !isAttack)
            {
                if (isMove && !isJump)
                {
                    anim.SetTrigger("MoveAttack");
                }

                else
                    anim.SetTrigger("Attack");
                StartCoroutine(AttackDelay());
            }
        }
        else if(weaponState == WEAPON.ARROW)
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(mouse.y - bow.position.y, mouse.x - bow.position.x)* Mathf.Rad2Deg;
            bow.rotation = Quaternion.AngleAxis(angle , Vector3.forward);
            if(Input.GetMouseButtonDown(0) && !isShot)
            {
                GameObject arrowObj = Instantiate(arrow);
                arrowObj.transform.SetParent(bow.GetChild(0));
                arrowObj.transform.localPosition = Vector3.zero;
                arrowObj.transform.rotation = bow.rotation;
                arrowObj.transform.SetParent(null);
                StartCoroutine(ShotDelay(shotDelay));
            }
        }
    }

    IEnumerator ShotDelay(float delay)
    {
        isShot = true;
        yield return new WaitForSeconds(delay);
        isShot = false;
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.2f);
        isAttack = true;
        yield return new WaitForSeconds(attackDelay);
        isAttack = false;
        anim.SetBool("isMove", false);
    }

    void Dash()
    {
        if (Input.GetMouseButtonDown(1) && !isDash)
        {
            isDash = true;
            speed = dashPower;
            anim.SetBool("isDash", true);
            StartCoroutine(FinishDash());
        }
    }

    IEnumerator FinishDash()
    {
        yield return new WaitForSeconds(dashTime);
        speed = defaultSpeed;
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("isDash", false);
        isDash = false;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isJump)
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            isJump = true;
            anim.SetBool("isJump", true);
        }
        if (rb.velocity.y < 0)
        {
            anim.SetBool("falling", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground")) 
        {
            isJump = false;
            anim.SetBool("isJump", false);
            anim.SetBool("falling", false);
        }
    }

    private void Move()
    {
        
        if (Input.GetKeyDown(KeyCode.D) && !isRight)
        {
            isRight = true;
            col.offset = colOffset;
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) && !isAttack)
        {
            if(!isRight) sr.flipX = true;
            else sr.flipX = false;
            transform.position += (isRight ? Vector3.right : Vector3.left) * speed * Time.deltaTime;
            MovingAnim();
        }
        if (Input.GetKeyDown(KeyCode.A) && isRight)
        {
            isRight = false;
            col.offset = new Vector2(colOffset.x * -1, colOffset.y);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("isMove", false);
            isMove = false;
        }
    }

    void MovingAnim()
    {
        isMove = true;
        if (isJump)
            anim.SetBool("isMove", false);
        else
            anim.SetBool("isMove", true);
    }
}
