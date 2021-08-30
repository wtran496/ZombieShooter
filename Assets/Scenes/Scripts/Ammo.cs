using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots = null;

    //show content in inspector
    [System.Serializable]
    private class AmmoSlot {
        public AmmoType ammoType = 0;
        public int ammoAmount = 0;
    }

    public int GetCurrentAmmo(AmmoType ammoType) {
        return GetAmmoSlot(ammoType).ammoAmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType) {
        GetAmmoSlot(ammoType).ammoAmount--;
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmount) {
        GetAmmoSlot(ammoType).ammoAmount += ammoAmount;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType) {
        foreach (AmmoSlot slot in ammoSlots) {
            if (slot.ammoType == ammoType) {
                return slot;
            }
        }
        return null;
    }
}
