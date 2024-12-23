using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject _portal;

    [SerializeField] PlayerInfo _playerinfo;

    private void Start()
    {
        PlayerPrefs.SetInt("Sword", 0);
        PlayerPrefs.SetInt("Bow", 0);
        PlayerPrefs.SetInt("Wand", 0);

        _playerinfo.StartGameInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 0)
        {
            _portal.SetActive(true);
        }
    }
}
