using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_Enforce : UIBase
{
    [Header("무기 슬롯"), SerializeField] GameObject[] _weaponSlot;

    [Header("[ 무기 정보 ]")]
    [SerializeField] GameObject[] _weapons;

    private void OnEnable()
    {
        Setting();
    }

    void Setting()
    {
        foreach(GameObject slot in _weaponSlot)
        {
            foreach(GameObject weapon in _weapons)
            {
                if(weapon.GetComponent<WeaponBase>()._WType == slot.GetComponent<Slot>()._SlotType)
                {
                    if (weapon.GetComponent<WeaponBase>()._IsGet)
                    {
                        slot.gameObject.SetActive(true);
                        break;
                    }
                    slot.gameObject.SetActive(false);
                }
            }
        }
    }

    public void EnforceClose()
    {
        UIManager._Inst.Closed_UI(UIManager.UI.ENFORCE);
        UIManager._Inst.Show_UI(UIManager.UI.STATUS);
        GameManager._Inst.UnPause();
    }
}
