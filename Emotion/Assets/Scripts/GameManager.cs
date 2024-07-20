using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
