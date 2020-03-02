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


    private void Update()
    {
        KeyCode[] codes = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0 };
        for (int i = 0; i < items.Count; i++) {
            if (Input.GetKeyDown(codes[i])) {
                items[i].Use();
            }
        }
        CheckEmptyItems();
    }

    public void CheckEmptyItems() {
        GameObject hotbar = GameObject.FindGameObjectWithTag("Hotbar");
        bool change = false;
        for (int i = items.Count - 1; i >= 0; i--) {
            if (items[i].quantity <= 0) {
                items.RemoveAt(i);
                change = true;
            }
        }
        if (change) {
            hotbar.GetComponent<Hotbar>().UpdateImages(items);
        }
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
