using Cainos.LucidEditor;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterBox : MonoBehaviour
{
    [Header("레터박스")]
    [SerializeField] DOTweenAnimation _letter;

    [Header("타이틀")]
    [SerializeField] DOTweenAnimation _title;

    [Header("엔딩 패널")]
    [SerializeField] DOTweenAnimation _black;

    public void BlackOn()
    {
        _black.DORestartById("Black-On");
    }

    string[] _titleSting = 
        { "<size=60>검게 물든</size>\r\n절망의 대마법사",
        "<size=60>희망을 잃은</size>\r\n절망의 기사",
        "<size=60>멸망한 왕국의 마지막</size>\r\n절망의 왕"
    };

    public void TitleOn(int num)
    {
        _title.GetComponent<Text>().text = _titleSting[num];
        _title.DORestartById("Phase-TitleFadeIn");
    }

    public void LetterActive(bool isOn)
    {
        if (isOn)
            _letter.DORestartById("Down");
        else
            _letter.DORestartById("Up");
    }
}
