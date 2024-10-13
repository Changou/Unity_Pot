using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Production : MonoBehaviour
{
    [Header("레터박스")]
    [SerializeField] LetterBox _letter;

    [SerializeField] LBPhase2 _boss;
    [SerializeField] GameObject _playerUI;

    public void Phase2PStart()
    {
        GameManager._Inst.Pause();
        _letter.LetterActive(false);
        UIManager2._Inst.AllHide();
    }

    public void TitleOn()
    {
        _letter.TitleOn(1);
    }

    public void Phase2Start()
    {
        GameManager._Inst.UnPause();
        _boss.enabled = true;
        _playerUI.SetActive(true);
        PhaseManager._Inst.PhaseSetting();
    }
}
