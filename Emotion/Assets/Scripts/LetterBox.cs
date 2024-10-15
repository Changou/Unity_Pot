using Cainos.LucidEditor;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterBox : MonoBehaviour
{
    [Header("∑π≈Õπ⁄Ω∫")]
    [SerializeField] DOTweenAnimation _letter;

    [Header("≈∏¿Ã∆≤")]
    [SerializeField] DOTweenAnimation _title;

    public void BlackOn()
    {
        GameManager._Inst.FadePanel();
    }

    string[] _titleSting = 
        { "<size=60>∞À∞‘ π∞µÁ</size>\r\n¿˝∏¡¿« ¥Î∏∂π˝ªÁ",
        "<size=60>»Ò∏¡¿ª ¿“¿∫</size>\r\n¿˝∏¡¿« ±‚ªÁ",
        "<size=60>∏Í∏¡«— ø’±π¿« ∏∂¡ˆ∏∑</size>\r\n¿˝∏¡¿« ø’"
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
