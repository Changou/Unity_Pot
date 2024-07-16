using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public static MoveController i;

    float moveX;
    float moveY;

    bool leftClick = false;
    bool rightClick = false;
    bool buttonF = false;

    public float _MoveX => moveX;
    public float _MoveY => moveY;
    public bool _LeftClick => leftClick;
    public bool _RightClick => rightClick;
    public bool _ButtonF => buttonF;
    private void Awake()
    {
        i = this;
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");



        leftClick = Input.GetMouseButtonDown(0) ? true : false;
        rightClick = Input.GetMouseButtonDown(1) ? true : false;
        buttonF = Input.GetKeyDown(KeyCode.F) ? true : false;
    }
}
