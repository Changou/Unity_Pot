using Cainos.LucidEditor;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterBox : MonoBehaviour
{
    [Header("���͹ڽ�")]
    [SerializeField] DOTweenAnimation _letter;

    [Header("Ÿ��Ʋ")]
    [SerializeField] DOTweenAnimation _title;

    [Header("���� �г�")]
    [SerializeField] DOTweenAnimation _black;

    public void BlackOn()
    {
        _black.DORestartById("Black-On");
    }

    string[] _titleSting = 
        { "<size=60>�˰� ����</size>\r\n������ �븶����",
        "<size=60>����� ����</size>\r\n������ ���",
        "<size=60>����� �ձ��� ������</size>\r\n������ ��"
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
