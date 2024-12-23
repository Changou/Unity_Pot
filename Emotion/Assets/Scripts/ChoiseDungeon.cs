using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiseDungeon : MonoBehaviour
{
    [SerializeField] FadeInOut _fade;

    [SerializeField] GameObject _despairBtn;
    [SerializeField] PlayerInfo _playerinfo;

    private void OnEnable()
    {
        if (_playerinfo.WeaponAllGet())
            _despairBtn.SetActive(true);
        else
            _despairBtn.SetActive(false);
    }

    public void SellectedDungeonName(string name)
    {
        PlayerPrefs.SetString("Dungeon", name);
        _fade.FadeOut("Dungeon");
    }
}
