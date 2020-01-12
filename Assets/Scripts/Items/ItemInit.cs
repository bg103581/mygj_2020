using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInit : MonoBehaviour
{
    public List<Item> Items;

    private void Awake() {
        foreach (Item item in Items) {
            item.isInInventory = false;
            item.itemCount = 0;

            item.isInCraft = false;
            item.craftCount = 0;
        }
    }
}
