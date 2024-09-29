using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBSpeacialAttack2 : Pattern
{
    [Header("패턴 관련")]
    [SerializeField] GameObject _prefab;
    [SerializeField] float _rotZ;
    [SerializeField] int _posMinY;
    [SerializeField] int _posMaxY;
    [SerializeField] int _lineCount;
    [SerializeField] float _warningTime;

    Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(WideAttack());
    }

    IEnumerator WideAttack()
    {
        GameObject[] line = new GameObject[_lineCount];
        for(int i = 0; i < _lineCount; i++)
        {
            line[i] = Instantiate(_prefab);

            Vector3 pos = new Vector3(0, Random.Range(_posMinY, _posMaxY), 0);
            Quaternion rot = Quaternion.Euler(0, 0, Random.Range(-_rotZ, _rotZ));

            line[i].transform.position = pos;
            line[i].transform.rotation = rot;
        }
        yield return new WaitForSeconds(_warningTime);
        _anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);

        foreach(GameObject warning in line)
        {
            warning.GetComponent<Collider2D>().enabled = true;

            Color color = warning.GetComponent<Renderer>().material.color;
            color.a = 255;
            warning.GetComponent <Renderer>().material.color = color;

        }

        yield return new WaitForSeconds(1f);

        for(int i = 0; i < line.Length; i++)
        {
            Destroy(line[i]);
        }
        yield return new WaitForSeconds(_patternTime);
        PatternOn(false);
    }
}
