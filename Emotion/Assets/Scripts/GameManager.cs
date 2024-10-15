using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _Inst;

    bool _isPause = false;

    public bool _IsPause => _isPause;

    private void Awake()
    {
        _Inst = this;
    }

    public void Pause()
    {
        _isPause = true;
    }
    public void UnPause()
    {
        _isPause = false;
    }

    [SerializeField] LetterBox _letter;
    public void GameOver()
    {
        FadePanel();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    [SerializeField] FadeInOut _fade;

    public void FadePanel()
    {
        _fade.FadeOut();
        UIManager2._Inst.OnRestartButton();
    }
}
