using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel_Enforce : UIBase
{
    [Header("무기 슬롯"), SerializeField] Slot[] _weaponSlot;

    [Header("[ 무기 정보 ]")]
    [SerializeField] WeaponBase[] _weapons;

    private void OnEnable()
    {
        Setting();
    }

    void Setting()
    {
        foreach(Slot slot in _weaponSlot)
        {
            foreach(WeaponBase weapon in _weapons)
            {
                if(weapon._WType == slot._SlotType)
                {
                    if (weapon._isGet)
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
