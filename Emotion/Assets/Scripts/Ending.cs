using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [SerializeField] Text _text;

    [SerializeField] string[] _sentence;
    [SerializeField] SceneLoader _scene;

    DOTweenAnimation _danim;

    int _index = 0;

    private void Awake()
    {
        _danim = GetComponent<DOTweenAnimation>();
        _text.text = _sentence[_index];
    }

    // Start is called before the first frame update
    void Start()
    {
        _danim.DORestartById("TitleFadeIn");
    }

    public void NextSentence()
    {
        if(++_index >= _sentence.Length)
        {
            _scene.OnLoadScene("Start");
        }
        else
        {
            _text.text = _sentence[_index];
            _danim.DORestartById("TitleFadeIn");
        }

    }
}
