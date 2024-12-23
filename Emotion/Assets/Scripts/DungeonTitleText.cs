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
            name = "분노하는 도시";
        }
        else if(name.Equals("Sad"))
        {
            name = "슬퍼하는 마을";
        }
        else if (name.Equals("Happy"))
        {
            name = "기쁨이 넘친 도로";
        }
        else
        {
            name = "절망으로 향하는 길";
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
