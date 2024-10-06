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

    public void PhaseEndAndNextPhase()
    {
        _currentPhase++;
        PhaseSetting();
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
