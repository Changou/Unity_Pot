using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{

    [Header("페이드 인 / 아웃에 사용할 이미지"), SerializeField]
    Image _fadeImage;

    [Header("페이드 비율"), SerializeField]
    float _rate = 0.5f;


    [Header("씬 로드"), SerializeField]
    SceneLoader _sceneLoader;

    void Awake()
    {
        _fadeImage.color = Color.black;
    }
    IEnumerator CRT_FadeInOut(bool isFadeIn, float rate, string name = "")
    {
        if (isFadeIn)
        {
            Color tmp = new Color(0, 0, 0, 1);

            while (tmp.a > 0f)
            {
                tmp.a -= _rate * Time.unscaledDeltaTime;

                _fadeImage.color = tmp;

                Mathf.Clamp(tmp.a, 0f, 1f);

                yield return null;

            }
        }
        else
        {
            Color tmp = new Color(0, 0, 0, _fadeImage.color.a);

            while (tmp.a < 1f)
            {
                tmp.a += _rate * Time.unscaledDeltaTime;

                _fadeImage.color = tmp;

                Mathf.Clamp(tmp.a, 0f, 1f);

                yield return null;
            }
            if (!name.Equals(""));
                _sceneLoader.OnLoadScene(name);
        }

    }

    void Start()
    {
        _fadeImage.color = Color.black;

        StartCoroutine(CRT_FadeInOut(true, _rate));
    }

    public void FadeOut(string name = "")
    {
        if(SceneManager.GetActiveScene().name != "Start")
            GameManager._Inst.SaveGameInfo();

        StopAllCoroutines();
        StartCoroutine(CRT_FadeInOut(false, _rate, name));
    }

    public void InBossStage()
    {
        FadeOut(PlayerPrefs.GetString("Dungeon"));
    }
}
