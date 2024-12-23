using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    [Header("Bgm¿Ã∏ß")]
    [SerializeField] string _bgmName;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager._Inst.PlayBGM(_bgmName);
    }
}
