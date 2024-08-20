using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    [SerializeField] Transform dest;

    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] Ease _easeX;
    [SerializeField] Ease _easeY;

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveX(dest.position.x, x).SetEase(_easeX);
        transform.DOMoveY(dest.position.y, y).SetEase(_easeY);
    }
}
