using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager2 : MonoBehaviour
{
    public static UIManager2 _Inst;

    [Header("UI")]
    [SerializeField] GameObject[] _ui;

    [Header("Re")]
    [SerializeField] GameObject _re;

    private void Awake()
    {
        _Inst = this;
    }

    public void AllHide()
    {
        foreach (GameObject ui in _ui)
        {
            ui.SetActive(false);
        }
    }

    public void OnRestartButton()
    {
        _re.SetActive(true);
    }
}
