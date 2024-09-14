using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObj : MonoBehaviour
{
    [SerializeField] SpriteRenderer _icon;

    int _myNo;
    int _iconIndex;
    bool _isFront;

    GameObject _front;
    GameObject _back;

    // юс╫ц
    private void Start()
    {
        InitCard(1, 1);
    }
    //===
    public void ToSpin(bool isFront)
    {
        _front.SetActive(isFront);
        _back.SetActive(!isFront);
    }

    public void InitCard(int no, int iconIndex)
    {
        _front = transform.GetChild(0).gameObject;
        _back = transform.GetChild(1).gameObject;

        _myNo = no;
        _iconIndex = iconIndex;
        _isFront = false;
        ToSpin(_isFront);
    }

    private void OnMouseDown()
    {
        ToSpin(_isFront = !_isFront);
    }
}
