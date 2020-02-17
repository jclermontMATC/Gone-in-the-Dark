using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RestoringItem : InventoryItem
{
    public enum Type {HEALTH, STAMINA, OIL};
    public Type itemType;
    public int strength;
    public float duration;

    public RestoringItem(int _strength, int _type) {
        Type nt = (Type)_type;
        strength = _strength;
        itemType = nt;
        weight = 4f;
        if (nt == Type.HEALTH) {
            weight = 2f;
        }
        if (nt == Type.STAMINA) {
            duration = 30f;
        }
    }

    public override void Use()
    {
        switch (itemType) {
            case Type.HEALTH:
                //Restore health by strength
                break;
            case Type.STAMINA:
                //Restore stamina by strength
                break;
            case Type.OIL:
                //Restore oil by strength
                break;
        }
    }
}
