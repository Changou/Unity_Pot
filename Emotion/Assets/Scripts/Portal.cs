using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] GameObject _spriteF;
    [SerializeField] GameObject _nextP;

    private void Update()
    {
        if (_spriteF.activeSelf && Input.GetKeyDown(KeyCode.F)) 
        {
            _nextP.SetActive(true);
            GameManager._Inst.Pause();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _spriteF.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _spriteF.SetActive(false);
        }
    }
}
