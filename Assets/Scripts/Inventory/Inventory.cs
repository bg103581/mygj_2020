using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : GenericSingleton<Inventory>
{
    #region VARIABLES
    public List<Item> ItemList = new List<Item>();
    public int MaxInventorySpace = 20;
    #endregion

    #region DELEGATE

    public delegate void OnItemChanged(); // define delegate
    public OnItemChanged onItemChangedCallback; // implement delegate

    #endregion

    #region METHODS

    public bool AddItem(Item item) {
        if (ItemList.Count >= MaxInventorySpace) {
            Debug.Log("Inventaire plein!");
            return false;
        }

        if (!item.isInInventory) {

            if (!ItemList.Contains(item)) {
                ItemList.Add(item);
            }

            item.isInInventory = true;
        }

        item.itemCount++;

        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke(); // Trigger OnItemChanged event
        }
        
        return true;
    }

    public void RemoveItem(Item item) {
        ItemList.Remove(item);
        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke(); // Trigger OnItemChanged event
        }
    }

    #endregion
}
