using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public GameObject hotbarPrefab;

    private GameObject[] hotbarBoxes;
    private Image[] hotbarImages;

    private Sprite[] itemIcons;

    // Start is called before the first frame update
    void Start()
    {
        itemIcons = Resources.LoadAll<Sprite>("Sprites/Items/ItemIcons");
        RectTransform rt = gameObject.GetComponent<RectTransform>();
        hotbarBoxes = new GameObject[10];
        hotbarImages = new Image[10];
        for (int x = 0; x < 10; x++) {
            hotbarBoxes[x] = Instantiate(hotbarPrefab, rt);
            RectTransform hbrt = hotbarBoxes[x].GetComponent<RectTransform>();
            hbrt.anchoredPosition = new Vector2(5 + x * 85, 5);
            hotbarImages[x] = hbrt.Find("Image").GetComponent<Image>();
        }
    }

    private Sprite GetItemIcon(string spriteName) {
        foreach (Sprite sprite in itemIcons)
            if (sprite.name == spriteName)
                return sprite;
        
        return null;
    }

    public void UpdateImages(List<InventoryItem> items) {
        for (int i = 0; i < hotbarImages.Length; i++) {
            if (items.Count > i) {
                InventoryItem item = items[i];
                if (item != null) {
                    switch (item.GetItemType()) {
                        case ItemType.OIL:
                            hotbarImages[i].sprite = GetItemIcon("OilBottle_0");
                            hotbarImages[i].enabled = true;
                            break;
                    }
                }
                else {
                    hotbarImages[i].enabled = false;
                }
            } else {
                hotbarImages[i].enabled = false;
            }
        }
    }
}
