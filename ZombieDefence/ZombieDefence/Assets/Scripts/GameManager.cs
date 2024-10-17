using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager i;

    public bool isGameStart { get; private set; } = false;
    public bool isGameOver { get; private set; } = false;

    GameObject titleUI;
    GameObject overUI;
    Text scoreT;
    Text bestT;
    int score = 0;

    public Action sEvents;

    AudioSource destroySound;
    AudioSource bgmSound;
    [SerializeField] AudioClip endClip;

    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        titleUI = GameObject.Find("TitleUI");
        overUI = GameObject.Find("OverUI");
        scoreT = GameObject.Find("ScoreText").GetComponent<Text>();
        bestT = GameObject.Find("BestText").GetComponent<Text>();

        destroySound = GetComponent<AudioSource>();
        bgmSound = GameObject.Find("BGM").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (!isGameStart) GameStart();
            else if (isGameOver) Restart();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    void GameStart()
    {
        isGameStart = true;
        titleUI.SetActive(false);
        bestT.text = "BEST:" + LoadBestScore();
        scoreT.enabled = true;
        bestT.enabled = true;
        sEvents();
    }

    public void GameOver()
    {
        isGameOver = true;
        overUI.transform.GetChild(0).gameObject.SetActive(true);
        bgmSound.clip = endClip;
        bgmSound.loop = false;
        bgmSound.Play();

        SaveScore();
    }

    public void AddScore(int addscore)
    {
        if (isGameOver) return;

        score += addscore;
        scoreT.text = "SCORE:" + score;
    }

    void SaveScore()
    {
        if (PlayerPrefs.HasKey("BEST"))
        {
            int best = PlayerPrefs.GetInt("BEST");
            if (score > best)
            {
                PlayerPrefs.SetInt("BEST", score);
            }
        }
        else PlayerPrefs.SetInt("BEST", score);
    }

    string LoadBestScore()
    {
        int best = PlayerPrefs.HasKey("BEST") ? PlayerPrefs.GetInt("BEST") : 0;
        return best.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    public void DestroyHomeSound()
    {
        destroySound.Play();
    }

}
