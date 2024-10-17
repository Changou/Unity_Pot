using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MOVE_POINT
{
    TOP,
    MIDDLE,
    BOTTOM
}

public class MoveController : MonoBehaviour
{
    [SerializeField] float[] spawnP = { -0.4f, -1.8f, -3.3f };
    [SerializeField] MOVE_POINT moving = MOVE_POINT.MIDDLE;
    bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.i.sEvents += () =>
        {
            isStart = true;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart || GameManager.i.isGameOver) return;
        Moving();
    }
    void Moving()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moving++;
            if (moving > (MOVE_POINT)2) moving = 0;
  
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moving--;
            if (moving < (MOVE_POINT)0) moving = (MOVE_POINT)2;
        
        }
        transform.position = SetPosition();
    }

    Vector2 SetPosition()
    {
        Vector2 pos = Vector2.zero;
        if (moving == MOVE_POINT.TOP) pos = new Vector2(transform.position.x, spawnP[(int)moving]);
        else if (moving == MOVE_POINT.MIDDLE) pos = new Vector2(transform.position.x, spawnP[(int)moving]);
        else if (moving == MOVE_POINT.BOTTOM) pos = new Vector2(transform.position.x, spawnP[(int)moving]);

        return pos;
    }
}
