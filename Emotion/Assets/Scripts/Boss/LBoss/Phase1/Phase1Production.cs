using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Phase1Production : PhaseProduction
{
    [Header("�÷��̾� �ִϸ��̼� ����")]
    [SerializeField] Animator _animPlayer;
    [SerializeField] DOTweenAnimation _player;
    [SerializeField] GameObject _playerUI;

    [Header("���� �ִϸ��̼� ����")]
    [SerializeField] DOTweenAnimation _boss;
    // Start is called before the first frame update
    void Start()
    {
        GameManager._Inst.Pause();
        _player.DOPlayById("Phase1-Appeared");
    }

    public void PlayerWalkAction(bool isOn)
    {
        _animPlayer.SetBool("isMove", isOn);
    }

    public void BossProduction(int num)
    {
        switch (num)
        {
            case 0:
                _boss.DOPlayById("Phase1-BossAppeared");
                break;
        }
    }

    public override void PhaseStart()
    {
        GameManager._Inst.UnPause();
        _boss.transform.GetChild(0).GetComponent<LBPhase1>().enabled = true;
        _playerUI.SetActive(true);
        PhaseManager._Inst.PhaseSetting();
        base.PhaseStart();
    }
}
