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
    [SerializeField] PhaseProduction[] _production;

    public void PhaseEndAndNextPhase()
    {
        _currentPhase++;
        PhaseSetting();
    }

    public void PhaseTitleOn()
    {
        _production[_currentPhase].TitleOn();
    }

    public void ProductionStart()
    {
        _production[_currentPhase].gameObject.SetActive(true);
    }
    
    public void PhaseStart()
    {
        _production[_currentPhase - 1].PhaseStart();
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
