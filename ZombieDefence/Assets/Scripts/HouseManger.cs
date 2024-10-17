using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManger : MonoBehaviour
{
    public static HouseManger i;

    [SerializeField] int houseCnt = 3;

    private void Awake()
    {
        i = this;
    }

    public void DecreaseHouse()
    {
        houseCnt--;
        if(houseCnt <= 0)
        {
            GameManager.i.GameOver();
        }
    }

}
