﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public int maxItems = 50;

    public float slowStaminaWeight = 30f;
    public float noStaminaWeight = 50f;

    private List<InventoryItem> items = new List<InventoryItem>();

    public void AddPotion(int type) {
        items.Add(new RestoringItem(25, type));
    }

    public float GetInventoryWeight() {
        float total = 0f;
        foreach (InventoryItem i in items) {
            total += i.weight;
        }
        return total;
    }

    public float RegenerationSpeed() {
        float totalWeight = GetInventoryWeight();
        if (totalWeight > 30f)
        {
            return 0.5f;
        }
        else if (totalWeight > 50f) {
            return 0f;
        }
        return 1f;
    }
}
