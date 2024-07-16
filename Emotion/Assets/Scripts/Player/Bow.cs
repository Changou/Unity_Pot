using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bow : MonoBehaviour
{
    [Header("È­»ì")]
    [SerializeField] GameObject prefab;
    [SerializeField] float shotDelay;

    Vector2 mouse;
    float angle;


    bool isShot = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
        angle += transform.parent.localScale.x < 0 ? 180 : 0;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Shot()
    {
        if (!isShot)
        {
            GameObject arrow = Instantiate(prefab);
            arrow.transform.SetParent(transform.GetChild(0));
            arrow.transform.localPosition = Vector3.zero;
            if(transform.parent.localScale.x < 0)
            {

                arrow.transform.rotation = Quaternion.AngleAxis(angle - 180f, Vector3.forward);
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
        yield return new WaitForSeconds(shotDelay);
        isShot = false;
    }
}
