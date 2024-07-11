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
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            PrevMenu();
        }
    }

    void PrevMenu()
    {
        _currentMenu--;
        if (_currentMenu < 0)
        {
            _currentMenu = menu.Length - 1;
        }
        MenuSetting();
    }

    void NextMenu()
    {
        _currentMenu++;
        if(_currentMenu > 2)
        {
            _currentMenu = 0;
        }
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
