using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [Header("�ɷ�ġ")]
    [SerializeField] int hp;
    [SerializeField] int atk;

    Slider hpSlider;

    private void Awake()
    {
        hpSlider = GameObject.Find("HpSlider").GetComponent<Slider>();
    }

    private void Start()
    {
        hpSlider.value = hp;
    }

}
