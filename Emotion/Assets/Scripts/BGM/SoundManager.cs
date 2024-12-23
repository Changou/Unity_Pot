using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;

[System.Serializable]
public class Sound  //컴포넌트 추가 불가능. MonoBehavior 상속 안 받아서.
{
    public string name;     //곡 이름
    public AudioClip clip;  //곡
}

public class SoundManager : MonoBehaviour
{
    #region singleton
    static public SoundManager _Inst;  

    private void Awake()  
    {
        if (_Inst == null)  
        {
            _Inst = this;  
            DontDestroyOnLoad(gameObject);  
        }
        else
            Destroy(this.gameObject);
    }
    #endregion singleton

    public Sound[] effectSounds;  // 효과음 오디오 클립들
    public Sound[] bgmSounds;  // BGM 오디오 클립들

    public AudioSource audioSourceBGM;  
    public AudioSource[] audioSourceEffects;

    public AudioMixer masterMixer;

    public string[] playSoundName;  // 재생 중인 효과음 사운드 이름 배열

    // Start is called before the first frame update
    void Start()
    {
        playSoundName = new string[audioSourceEffects.Length];
    }


    public void PlaySE(string _name)    //효과음 재생
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying)
                    {
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].Play();
                        playSoundName[j] = effectSounds[i].name;
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용 중입니다.");
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
    }
    public void PlayBGM(string _name)   //BGM재생
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (_name == bgmSounds[i].name)
            {
                audioSourceBGM.clip = bgmSounds[i].clip;
                masterMixer.SetFloat("BGM", 0);
                audioSourceBGM.Play();
                return;
            }
        }
        
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
    }

    public void StopAllSE() //모든 효과음 재생 중지
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }
    public void StopSE(string _name)    //특정 효과음 재생 중지
    {
        bool notSound = true;
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                notSound = false;
                break;
            }
        }
        if (notSound)
        {
            Debug.Log("재생 중인" + _name + "사운드가 없습니다. ");
        }
    }
    public void StopAllBGM()
    {
        audioSourceBGM.Stop();
    }
    public void PauseBGM()
    {
        audioSourceBGM.Pause();
    }
}
