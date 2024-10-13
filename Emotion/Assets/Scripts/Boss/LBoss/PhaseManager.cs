using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager _Inst;

    private void Awake()
    {
        _Inst = this;
    }

    [Header("페이즈 상태")]
    [SerializeField] int _currentPhase = 1;
    [SerializeField] GameObject[] _phaseBoss;
    [SerializeField] GameObject[] _phaseUI;

    //private void Start()
    //{
    //    PhaseSetting();
    //}

    [Header("페이즈 연출")]
    [SerializeField] Phase2Production _phase2P;

    public void PhaseEndAndNextPhase()
    {
        _currentPhase++;
        PhaseSetting();
    }

    public void Phase2TitleOn()
    {
        _phase2P.TitleOn();
    }

    public void Phase2Start()
    {
        _phase2P.Phase2PStart();
    }

    public void PhaseSetting()
    {
        for (int i = 0; i < _phaseBoss.Length; i++)
        {
            if (i == _currentPhase - 1)
            {
                _phaseBoss[i].SetActive(true);
                _phaseUI[i].SetActive(true);
                continue;
            }
            _phaseBoss[i].SetActive(false);
            _phaseUI[i].SetActive(false);
        }
    }
}
