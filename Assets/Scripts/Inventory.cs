using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int maxItems = 50;

    public float slowStaminaWeight = 30f;
    public float noStaminaWeight = 50f;

    private List<InventoryItem> items = new List<InventoryItem>();

    public RectTransform canvas;
    public GameObject hotbarPrefab;
    

    private void Start()
    {
        
    }

    public void AddPotion(int type) {
        items.Add(new RestoringItem(25, type));
        canvas.Find("Hotbar").GetComponent<Hotbar>().UpdateImages(items);
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
