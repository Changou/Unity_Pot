using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3Production : PhaseProduction
{
    [Header("레터박스")]
    [SerializeField] LetterBox _letter;

    [SerializeField] LBPhase3 _boss;
    [SerializeField] GameObject _playerUI;

    [SerializeField] DOTweenAnimation _light;

    public void OnEnable()
    {
        GameManager._Inst.Pause();
        _letter.LetterActive(false);
        UIManager2._Inst.AllHide();
        _light.DORestartById("Light-On");
    }

    public override void TitleOn()
    {
        _letter.TitleOn(2);
    }

    public override void PhaseStart()
    {
        GameManager._Inst.UnPause();
        _boss.enabled = true;
        _playerUI.SetActive(true);
        PhaseManager._Inst.PhaseSetting();
        base.PhaseStart();
    }
}
