using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack2 : Pattern
{
    [SerializeField] SpriteRenderer _dir;
    [SerializeField] Sprite[] _directionImg;
    [SerializeField] float _waitTime;
    [SerializeField] GameObject _fire;

    [SerializeField] float _coolTime;

    private void OnEnable()
    {
        int ran = Random.Range(0, 2);

        StartCoroutine(Attack(ran));
    }

    IEnumerator Attack(int dir)
    {
        _dir.sprite = _directionImg[dir];
        _dir.gameObject.SetActive(true);
        yield return new WaitForSeconds(_waitTime);
        _dir.gameObject.SetActive(false);

        GameObject fire = Instantiate(_fire);
        fire.transform.position = new Vector3(0, fire.transform.position.y, 0); 
        fire.AddComponent<FireMove>();
        fire.GetComponent<FireMove>().dir = dir;

        yield return new WaitForSeconds(_coolTime);
        PatternOn(false);
    }
}
