using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootComponent : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float shootRate = 0.2f;
    [SerializeField] float reload = 1f;
    [SerializeField] int magazine = 10;
    [SerializeField] int maxMagazine = 10;

    bool canFire = false;
    bool isreload = false;
    float reloadSpeed = 1f;

    Text bulletT;
    Slider reloadS;
    AudioSource shootSound;

    void Start()
    {
        bulletT = GameObject.Find("BulletT").GetComponent<Text>();
        reloadS = GameObject.Find("ReloadSlider").GetComponent<Slider>();
        shootSound = GetComponent<AudioSource>();
        reloadS.maxValue = reload;
        reloadS.value = 0;

        GameManager.i.sEvents += () =>
        {
            canFire = true;
            GameObject.Find("Bullet").GetComponent<Image>().enabled = true;
            bulletT.enabled = true;
            reloadS.transform.GetChild(0).GetComponent<Image>().enabled = true;
            reloadS.transform.GetChild(1).GetChild(0).GetComponent<Image>().enabled = true;
        };
    }

    // Update is called once per frame
    void Update()
    {
        ReloadSlider();
        if (!canFire || GameManager.i.isGameOver) return;
        if (Input.GetMouseButton(0))
        {
            Shoot();
            shootSound.Play();
        }
    }

    void ReloadSlider()
    {
        if (isreload && reloadS.value < reloadS.maxValue)
        {
            reloadS.value += Time.deltaTime * reloadSpeed;
        }
        else
        {
            reloadS.value = 0;
            isreload = false;
        }
    }

    void Shoot()
    {
        magazine--;
        bulletT.text = "X " + magazine;

        Vector3 shootPos = new Vector3(transform.position.x + 1f, transform.position.y + 0.15f);
        BulletPoolManager.i.UseBullet(shootPos, bulletPrefab.gameObject.transform.rotation);

        if (magazine <= 0) 
        { 
            StartCoroutine(Reload());
            isreload = true;
        }
        else StartCoroutine(Wating());
    }
    IEnumerator Reload()
    {
        canFire = false;
        yield return new WaitForSeconds(reload);
        magazine = maxMagazine;
        bulletT.text = "X " + magazine;
        canFire = true;
    }

    IEnumerator Wating()
    {
        canFire = false;
        yield return new WaitForSeconds(shootRate);
        canFire = true;
    }
}
