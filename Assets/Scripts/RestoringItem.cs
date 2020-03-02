using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RestoringItem : InventoryItem
{
    public ItemType itemType;
    public int strength;
    public float duration;

    public RestoringItem(int _strength, int _type) {
        ItemType nt = (ItemType)_type;
        strength = _strength;
        itemType = nt;
        weight = 4f;
        if (nt == ItemType.HEALTH) {
            weight = 2f;
        }
        if (nt == ItemType.STAMINA) {
            duration = 30f;
        }
    }

    public override ItemType GetItemType() { return itemType; }

    public override void Use()
    {
        switch (itemType) {
            case ItemType.HEALTH:
                //Restore health by strength
                break;
            case ItemType.STAMINA:
                //Restore stamina by strength
                break;
            case ItemType.OIL:
                //Restore oil by strength
                break;
        }
    }
}
