using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBigMessageBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _txtMessage;

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Open(string dig)
    {
        _txtMessage.text = dig;
    }
}
