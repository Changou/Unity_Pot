using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bow : WeaponBase
{
    [Header("È­»ì")]
    [SerializeField] GameObject prefab;

    Vector2 mouse;
    float angle;


    bool isShot = false;

    // Update is called once per frame
    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
        angle += transform.parent.parent.localScale.x < 0 ? 180 : 0;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public virtual void Shot()
    {
        if (!isShot)
        {
            GameObject arrow = Instantiate(prefab);

            arrow.GetComponent<Arrow>()._damage = _ATK + (_LV * 2);

            arrow.transform.SetParent(transform.GetChild(0));
            arrow.transform.localPosition = Vector3.zero;
            if (transform.parent.parent.localScale.x < 0)
            {
                arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward)
                    * new Quaternion(0, 0, 180, 0);
            }
            else
                arrow.transform.rotation = transform.rotation;
            arrow.transform.SetParent(null);
            StartCoroutine(ShotDelay());
        }
    }

    IEnumerator ShotDelay()
    {
        isShot = true;
        yield return new WaitForSeconds(_delay);
        isShot = false;
    }
}
