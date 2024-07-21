using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager _Inst;

    public enum UI 
    { 
        STATUS, 
        ENFORCE,

        MAX
    }

    [SerializeField] UIBase[] _ui;

    private void Awake()
    {
        _Inst = this;
        HideAll();

        Show_UI(UI.STATUS);
    }

    public void Show_UI_Only(UI _type)
    {
        for(int i = 0; i < (int)UI.MAX; i++)
        {
            if(i == (int)_type)
            {
                _ui[i].Show(true);
                continue;
            }
            _ui[i].Show(false);
        }  
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
