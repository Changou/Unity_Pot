using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager _Inst;

    public enum UI 
    { 
        HP, 
        ENFORCE,

        MAX
    }

    [SerializeField] UIBase[] _ui;

    private void Awake()
    {
        _Inst = this;
        HideAll();

        Show_UI(UI.HP);
    }

    public void Show_UI(UI _type)
    {
        _ui[(int)_type].Show(true);
    }

    public void Closed_UI(UI _type)
    {
        _ui[(int)_type].Show(false);
    }

    void HideAll()
    {
        foreach(UIBase ui in _ui)
        {
            ui.Show(false);
        }
    }
}
