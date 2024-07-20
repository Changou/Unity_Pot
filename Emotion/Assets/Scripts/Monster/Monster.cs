using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("[ ∏ÛΩ∫≈Õ ]")]
    [SerializeField] int hp;
    [SerializeField] int atk;
    [SerializeField] float speed;

    bool _isDelay = false;
    Vector2 velo = Vector2.zero;

    private void Update()
    {
        Moving();
    }

    void Moving()
    {
        if (!_isDelay)
        {
            
            StartCoroutine(Delay());
        }
    }
    IEnumerator Delay()
    {
        _isDelay = true;
        yield return new WaitForSeconds(1f);
        _isDelay = false;
    }
}
