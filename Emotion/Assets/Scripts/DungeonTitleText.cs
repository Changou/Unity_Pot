using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonTitleText : MonoBehaviour
{
    [SerializeField] Text _title;
    [SerializeField] float _delay;

    private void Start()
    {
        string name = PlayerPrefs.GetString("Dungeon");
        if (name.Equals("Anger"))
        {
            name = "�г��ϴ� ����";
        }
        else if(name.Equals("Sad"))
        {
            name = "�����ϴ� ����";
        }
        else if (name.Equals("Happy"))
        {
            name = "����� ��ģ ����";
        }
        else
        {
            name = "�������� ���ϴ� ��";
        }
        _title.text = name;

        StartCoroutine("FadeTitle");
    }

    IEnumerator FadeTitle()
    {
        yield return new WaitForSeconds(_delay);

        Color tmp;
        while(_title.color.a > 0)
        {
            tmp = _title.color;
            tmp.a -= 1 * Time.deltaTime;
            _title.color = tmp;
            yield return null;
        }
    }
}
