using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smith : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (MoveController.i._ButtonF)
            {
                UIManager._Inst.Show_UI(UIManager.UI.ENFORCE);
                GameManager._Inst.Pause();
            }
        }
    }
}
