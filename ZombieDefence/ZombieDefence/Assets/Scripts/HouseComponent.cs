using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseComponent : MonoBehaviour
{
    public static HouseComponent i;
    int hp = 0;
    [SerializeField] int maxHp = 50;
    SpriteRenderer sr;
    Slider hpSlider;

    AudioSource sound;
    private void Awake()
    {
        i = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        sr = GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();

        if (gameObject.name.Contains("H1")) hpSlider = GameObject.Find("HpSlider1").GetComponent<Slider>();
        else if (gameObject.name.Contains("H2")) hpSlider = GameObject.Find("HpSlider2").GetComponent<Slider>();
        else if (gameObject.name.Contains("H3")) hpSlider = GameObject.Find("HpSlider3").GetComponent<Slider>();

        hpSlider.maxValue = maxHp;
        hpSlider.value = maxHp;

        GameManager.i.sEvents += () =>
         {
             hpSlider.transform.GetChild(0).GetComponent<Image>().enabled = true;
             hpSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>().enabled = true;
         };
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        hpSlider.value = hp;
        sound.Play();

        StartCoroutine(SetColor());
        if (hp <= 0)
        {
            GameManager.i.DestroyHomeSound();
            DestroyHouse();

        }
    }
    void DestroyHouse()
    {
        HouseManger.i.DecreaseHouse();
        hpSlider.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    IEnumerator SetColor()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }
}
