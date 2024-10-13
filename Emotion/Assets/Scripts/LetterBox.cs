using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBox : MonoBehaviour
{
    [Header("레터박스")]
    [SerializeField] DOTweenAnimation _letter;

    [Header("타이틀")]
    [SerializeField] DOTweenAnimation[] _titles;

    public void TitleOn(int num)
    {
        _titles[num].DOPlayById("Phase-TitleFadeIn");
    }

    public void LetterActive(bool isOn)
    {
        if (isOn)
            _letter.DORestartById("Down");
        else
            _letter.DORestartById("Up");
    }
}
