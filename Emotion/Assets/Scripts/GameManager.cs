using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _Inst;

    bool _isPause = false;

    public bool _IsPause => _isPause;

    private void Awake()
    {
        _Inst = this;
    }

    public void Pause()
    {
        _isPause = true;
    }
    public void UnPause()
    {
        _isPause = false;
    }

    [SerializeField] PlayerInfo _playerInfo;

    public void SaveGameInfo()
    {
        PlayerPrefs.SetInt("NormalEXP", _playerInfo.WeaponExp(WEAPON.NORMAL));
        PlayerPrefs.SetInt("SwordEXP", _playerInfo.WeaponExp(WEAPON.SWORD));
        PlayerPrefs.SetInt("BowEXP", _playerInfo.WeaponExp(WEAPON.ARROW));
        PlayerPrefs.SetInt("WandEXP", _playerInfo.WeaponExp(WEAPON.WAND));

        PlayerPrefs.SetInt("NormalLevel", _playerInfo.WeaponLevel(WEAPON.NORMAL));
        PlayerPrefs.SetInt("SwordLevel", _playerInfo.WeaponLevel(WEAPON.SWORD));
        PlayerPrefs.SetInt("BowLevel", _playerInfo.WeaponLevel(WEAPON.ARROW));
        PlayerPrefs.SetInt("WandLevel", _playerInfo.WeaponLevel(WEAPON.WAND));
    }
}
