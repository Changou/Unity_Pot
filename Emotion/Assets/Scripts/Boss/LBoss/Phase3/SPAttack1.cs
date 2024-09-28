using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPAttack1 : Pattern
{
    [SerializeField] GameObject _waringLine;
    [SerializeField] int _lineCnt;

    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(CrossAttack());
    }

    IEnumerator CrossAttack()
    {
        GameObject[] lines = new GameObject[_lineCnt];

        for(int i = 0; i < lines.Length; i++)
        {
            lines[i] = Instantiate(_waringLine);
            lines[i].transform.position = Vector3.zero;
        }
        lines[0].transform.rotation = Quaternion.Euler(0, 0, 20f);
        lines[1].transform.rotation = Quaternion.Euler(0, 0, -20f);
        yield return new WaitForSeconds(1);
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i].GetComponent<Collider2D>().enabled = true;
            Color color = lines[i].GetComponent<Renderer>().material.color;
            color.a = 255f;
            lines[i].GetComponent <Renderer>().material.color = color;
        }
        yield return new WaitForSeconds(1);
        for(int i = 0; i < lines.Length; i++)
        {
            Destroy(lines[i]);
        }
        yield return new WaitForSeconds(_patternTime);
        PatternOn(false);
    }

}
