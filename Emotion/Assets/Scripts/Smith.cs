using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Smith : MonoBehaviour
{
    [SerializeField] GameObject _text;

    private void Update()
    {
        if(_text.activeSelf && MoveController.i._ButtonF)
        {
            UIManager._Inst.Show_UI_Only(UIManager.UI.ENFORCE);
            GameManager._Inst.Pause();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _text.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _text.SetActive(false);
        }
    }
}
