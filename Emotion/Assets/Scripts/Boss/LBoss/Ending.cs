using DG.Tweening;
using UnityEngine;

public class Ending : PhaseProduction
{
    [Header("레터박스")]
    [SerializeField] LetterBox _letter;

    public void OnEnable()
    {
        GameManager._Inst.Pause();
        _letter.LetterActive(false);
        UIManager2._Inst.AllHide();
        _letter.BlackOn();
    }

}
