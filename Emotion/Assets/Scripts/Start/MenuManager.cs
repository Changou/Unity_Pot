using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] GameObject[] menu;

    int _currentMenu;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager._Inst.PlayBGM("StartPage");
        _currentMenu = 0;
        MenuSetting();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            NextMenu();
        }
        else if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            PrevMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectMenu();
        }
    }

    [Header("페이드 인 아웃")]
    [SerializeField] FadeInOut _fade;

    void SelectMenu()
    {
        if(_currentMenu == 0)
        {
            _fade.FadeOut("Tutorial");
        }
        else
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    void PrevMenu()
    {
        _currentMenu = --_currentMenu < 0 ? menu.Length - 1 : _currentMenu;
        MenuSetting();
    }

    void NextMenu()
    {
        _currentMenu = ++_currentMenu > (menu.Length - 1) ? 0 : _currentMenu;
        MenuSetting();
    }
    void MenuSetting()
    {
        for(int i = 0; i < menu.Length; i++)
        {
            if (i == _currentMenu)
                continue;
            menu[i].GetComponent<Text>().color = Color.black;
            menu[i].GetComponent<Text>().fontSize = 70;
        }
        menu[_currentMenu].GetComponent<Text>().color = Color.white;
        menu[_currentMenu].GetComponent<Text>().fontSize = 80;
    }
}
