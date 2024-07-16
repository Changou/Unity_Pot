using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    [Header("플레이어 정보")]
    [SerializeField] PlayerInfo playerInfo;

    float moveX;
    Vector3 movement = new Vector3();

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    Collider2D col;

    bool isJump = false;
    bool isDash = false;
    bool isAttack = false;
    bool isShot = false;

    [Header("활")]
    [SerializeField] Transform bow;
    [SerializeField] GameObject arrow;
    [SerializeField] float shotDelay;

    [Header("대검 정보")]
    [SerializeField] LongSword longSword;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttack)
            HorizontalMoving();
        if (MoveController.i._MoveY > 0)
        {
            Jump();
        }
        if (MoveController.i._LeftClick)
        {
            Attack();
        }
        if (MoveController.i._RightClick) 
        {
            Dash();
        }
        if (isJump)
        {
            anim.SetFloat("jumpVel", rb.velocity.y);
        }
    }

    void HorizontalMoving() //이동
    {
        moveX = MoveController.i._MoveX;
        movement = new Vector3(moveX, 0, 0);
        movement.Normalize();
        
        if (moveX != 0) //방향이 입력되었을 때
        {
            if((moveX > 0 && transform.localScale.x < 0) || (moveX < 0 && transform.localScale.x >0)) //입력된 방향과 캐릭터의 방향이 다를 시
            {
                transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, 1); // 방향전환
            }
            if(rb.velocity.y == 0) //점프 중이면
                anim.SetBool("isMove", true);
        }
        else anim.SetBool("isMove", false);

        transform.position += movement * playerInfo._Speed * Time.deltaTime;
    }

    void Attack() //공격
    {
        if (playerInfo._WeaponState == WEAPON.NORMAL)
        {
            if (!isAttack)
            {
                if (MoveController.i._MoveX != 0 && !isJump)
                {
                    anim.SetTrigger("MoveAttack");
                }
                else
                    anim.SetTrigger("Attack");
                StartCoroutine(AttackDelay(playerInfo._AttackDelay));
            }
        }
        else if(playerInfo._WeaponState == WEAPON.SWORD)
        {
            if (!isAttack)
            {
                longSword.Attack();
                StartCoroutine(AttackDelay(longSword._AttackDelay));
            }
        }
        else if(playerInfo._WeaponState == WEAPON.ARROW) //bow 스크립트 분활
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(mouse.y - bow.position.y, mouse.x - bow.position.x)* Mathf.Rad2Deg;
            bow.rotation = Quaternion.AngleAxis(angle , Vector3.forward);
            if(!isShot)
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

    IEnumerator ShotDelay(float delay) //발사체 간격
    {
        isShot = true;
        yield return new WaitForSeconds(delay);
        isShot = false;
    }

    IEnumerator AttackDelay(float delay) //공격 간격
    {
        yield return new WaitForSeconds(0.2f);
        isAttack = true;
        anim.SetBool("isMove", false);
        yield return new WaitForSeconds(delay);
        isAttack = false; 
    }

    void Dash() //대쉬
    {
        if (!isDash)
        {
            isDash = true;
            playerInfo.OnDash();
            anim.SetBool("isDash", true);
            StartCoroutine(FinishDash());
        }
    }

    IEnumerator FinishDash() //대쉬 간격
    {
        yield return new WaitForSeconds(playerInfo._DashTime);
        playerInfo.OffDash();
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("isDash", false);
        isDash = false;
    }

    void Jump() //점프
    {
        if (!isJump)
        {
            rb.AddForce(Vector3.up * playerInfo._JumpPower, ForceMode2D.Impulse);
            isJump = true;
            anim.SetBool("isJump", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground")) 
        {
            isJump = false;
            anim.SetBool("isJump", false);
        }
    }
}
