using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smith : MonoBehaviour
{
    [Header("[°­È­ UI]"), SerializeField] GameObject _UpgradeUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && MoveController.i._ButtonF)
        {
            _UpgradeUI.SetActive(true);
            
        }
    }
}
