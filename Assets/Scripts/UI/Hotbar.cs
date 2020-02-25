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
            hotbarImages[x] = hbrt.GetComponentInChildren<Image>();
        }
    }

    public void UpdateImages(List<InventoryItem> items) {
        foreach (InventoryItem i in items) {
            switch (i.name) {
                case "Health Potion":
                    break;
            }
        }
    }
}
