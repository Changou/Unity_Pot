using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseProduction : MonoBehaviour
{
    public virtual void PhaseStart()
    {
        gameObject.SetActive(false);
    }

    public virtual void TitleOn()
    {

    }
}
