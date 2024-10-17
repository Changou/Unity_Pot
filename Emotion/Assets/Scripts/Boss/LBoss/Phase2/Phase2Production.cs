using DG.Tweening;
using UnityEngine;

public class Phase2Production : PhaseProduction
{
    [Header("레터박스")]
    [SerializeField] LetterBox _letter;

    [SerializeField] LBPhase2 _boss;
    [SerializeField] GameObject _playerUI;


    public void OnEnable()
    {
        GameManager._Inst.Pause();
        _letter.LetterActive(false);
        UIManager2._Inst.AllHide();
    }

    public override void TitleOn()
    {
        _letter.TitleOn(1);
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
