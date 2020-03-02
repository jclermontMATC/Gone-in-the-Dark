using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

abstract public class InventoryItem
{
    public int quantity;
    public string name;
    public float weight;

    abstract public ItemType GetItemType();
    abstract public void Use();
}
