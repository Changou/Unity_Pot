using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Phase2Production : PhaseProduction
{
    [Header("레터박스")]
    [SerializeField] LetterBox _letter;

    [SerializeField] LBPhase2 _boss;
    [SerializeField] GameObject _playerUI;

    [Header("연출 효과")]
    [SerializeField] GameObject _fireWall;
    [SerializeField] float _firedelay;

    [Header("Bgm"), SerializeField] string _name;


    public void OnEnable()
    {
        GameManager._Inst.Pause();
        _letter.LetterActive(false);
        UIManager._Inst.HideAll();
        StartCoroutine("Production");
    }

    IEnumerator Production()
    {
        yield return new WaitForSeconds(2);
        PhaseManager._Inst.PhaseSetting();
        UIManager._Inst.HideAll();
        GameObject fire = Instantiate(_fireWall);

        yield return new WaitForSeconds(_firedelay);
        Destroy(fire);
        SoundManager._Inst.PlayBGM(_name);
        _boss.gameObject.SetActive(true);
        TitleOn();
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
