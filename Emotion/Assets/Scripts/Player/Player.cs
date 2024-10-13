using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : LivingEntity
{
    [Header("플레이어 정보")]
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] Transform _myTrans;

    float moveX;
    Vector3 movement = new Vector3();

    Rigidbody2D rb;
    Animator anim;
    Collider2D _coll;

    bool isAttack = false;

    float _DashTime = 0;

    [Header("검")]
    [SerializeField] Collider2D _AttackPoint;

    [Header("활")]
    [SerializeField] Bow bow;
    [SerializeField] float shotDelay;

    [Header("대검 정보")]
    [SerializeField] LongSword longSword;

    [Header("완드")]
    [SerializeField] Wand wand;

    [SerializeField] float _rayDist;
    [SerializeField] float _rayDistWall;

    float _stopMove = 1f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
        _AttackPoint.enabled = false;
        
    }
    private void Start()
    {
        OnDeath += () =>
        {
            anim.SetTrigger("Die");
            GameManager._Inst.Pause();
            UIManager2._Inst.AllHide();
        };
        rb.gravityScale = 0f;
        _coll.isTrigger = true;
    }

    void EndGame()
    {
        GameManager._Inst.GameOver();
    }
    void Update()
    {
        if (!GameManager._Inst._IsPause && !isAttack)
        {
            if (MoveController.i._LeftClick && rb.velocity.y == 0)
                Attack();
            Dash();
            HorizontalMoving();
            if (_DashTime == 0)
            {
                if (MoveController.i._MoveY > 0)
                {
                    rb.gravityScale = 1.5f;
                    Jump();
                }
                if (rb.velocity.y != 0)
                {
                    anim.SetFloat("jumpVel", rb.velocity.y);

                }
                WeaponChange();
            }
        }
    }

    RaycastHit2D _rayHitGround;
    RaycastHit2D _rayHitWall;

    private void FixedUpdate()
    {
        Debug.DrawRay(rb.position, Vector2.down * _rayDist, new Color(0, 1, 0));
        _rayHitGround = Physics2D.Raycast(rb.position, Vector2.down, _rayDist, LayerMask.GetMask("Ground"));
        if (rb.velocity.y < -3f && _rayHitGround.collider != null) 
        {
            rb.gravityScale = 0f;
            _coll.isTrigger = false;
        }

        Debug.DrawRay(rb.position, _myTrans.localScale.x * Vector2.right * _rayDistWall, new Color(0,1,0));
        _rayHitWall = Physics2D.Raycast(rb.position, _myTrans.localScale.x * Vector2.right, _rayDistWall, LayerMask.GetMask("Wall"));

        if (_rayHitWall.collider != null) { _stopMove = 0f; }
        else _stopMove = 1f;
    }

    void WeaponSwap()
    {
        if(MoveController.i._MouseWheel > 0)
        {
            playerInfo.WeaponUp();
        }
        else if(MoveController.i._MouseWheel < 0)
        {
            playerInfo.WeaponDown();
        }
    }

    void WeaponChange()
    {
        WeaponSwap();
        for(int i = 0; i < (int)WEAPON.MAX; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == (int)playerInfo._WeaponState ? true : false);
        }
    }

    void HorizontalMoving() //이동
    {
        moveX = MoveController.i._MoveX;
        movement = new Vector3(moveX, 0, 0);
        movement.Normalize();

        if (moveX != 0)
        {
            if ((moveX > 0 && _myTrans.localScale.x < 0) || (moveX < 0 && _myTrans.localScale.x > 0)) // 입력 방향과 캐릭터 방향이 다를 때
            {
                _myTrans.localScale = new Vector3(_myTrans.localScale.x * (-1), _myTrans.localScale.y, 1); 
            }
            if (rb.velocity.y == 0)
                anim.SetBool("isMove", true);
        }
        else anim.SetBool("isMove", false);

        _myTrans.position += movement * playerInfo._Speed * Time.deltaTime * _stopMove;
    }

    void Attack() //공격
    {
        if (playerInfo._WeaponState == WEAPON.NORMAL)
        {   
            anim.SetTrigger("Attack");
            isAttack = true;
        }
        else if(playerInfo._WeaponState == WEAPON.SWORD)
        {
            longSword.Attack();
        }
        else if(playerInfo._WeaponState == WEAPON.ARROW)
        {
            bow.Shot();
        }
        else
        {
            wand.Shot();
        }
    }

    public void OnAttack()
    {
        _AttackPoint.enabled = true;
    }

    public void AttackFin()
    {
        isAttack = false;
        _AttackPoint.enabled = false;
    }

    void Dash() //대쉬
    {
        if (MoveController.i._RightClick && _DashTime == 0)
        {
            playerInfo.OnDash();
            anim.SetBool("isDash", true);
            _DashTime = playerInfo._DashTime;
        }
        else if(_DashTime > 0)
        {
            _DashTime -= 1f * Time.deltaTime;
        }
        else if(_DashTime < 0)
        {
            _DashTime = 0f;
            anim.SetBool("isDash", false);
            playerInfo.OffDash();
        }
    }

    void Jump() //점프
    {
        if (rb.velocity.y == 0)
        {
            rb.AddForce(Vector3.up * playerInfo._JumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            anim.SetBool("isJump", false);
            _coll.isTrigger = true;
        }
    }
}
