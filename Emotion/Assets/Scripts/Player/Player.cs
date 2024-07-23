using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("�÷��̾� ����")]
    [SerializeField] PlayerInfo playerInfo;

    float moveX;
    Vector3 movement = new Vector3();

    Rigidbody2D rb;
    Animator anim;

    bool isAttack = false;

    float _DashTime = 0;

    [Header("Ȱ")]
    [SerializeField] Bow bow;
    [SerializeField] float shotDelay;

    [Header("��� ����")]
    [SerializeField] LongSword longSword;

    [Header("�ϵ�")]
    [SerializeField] Wand wand;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!GameManager._Inst._IsPause)
        {
            if (MoveController.i._LeftClick && rb.velocity.y == 0)
                Attack();
            else if (!isAttack)
            {
                Dash();
                HorizontalMoving();
                if (_DashTime == 0)
                {
                    if (MoveController.i._MoveY > 0)
                    {
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

    void HorizontalMoving() //�̵�
    {
        moveX = MoveController.i._MoveX;
        movement = new Vector3(moveX, 0, 0);
        movement.Normalize();

        if (moveX != 0)
        {
            if ((moveX > 0 && transform.localScale.x < 0) || (moveX < 0 && transform.localScale.x > 0)) // �Է� ����� ĳ���� ������ �ٸ� ��
            {
                transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, 1); 
            }
            if (rb.velocity.y == 0)
                anim.SetBool("isMove", true);
        }
        else anim.SetBool("isMove", false);

        transform.position += movement * playerInfo._Speed * Time.deltaTime;
    }

    void Attack() //����
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

    public void AttackFin()
    {
        isAttack = false;
    }

    void Dash() //�뽬
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

    void Jump() //����
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
        }
    }
}
