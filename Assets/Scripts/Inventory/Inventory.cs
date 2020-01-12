using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : GenericSingleton<Inventory>
{
    #region VARIABLES
    public List<Item> ItemList = new List<Item>();
    public int MaxInventorySpace = 20;
    public GameObject[] weapons;
    
    public string[,] itemCombinations = new string[16, 3];
    #endregion

    #region DELEGATE

    public delegate void OnItemChanged(); // define delegate
    public OnItemChanged onItemChangedCallback; // implement delegate

    #endregion

    public override void Awake() {
        itemCombinations[0, 0] = "Bois";
        itemCombinations[0, 1] = "Bois";
        itemCombinations[0, 2] = "Elastique";

        itemCombinations[1, 0] = "LanceBlond";
        itemCombinations[1, 1] = "Blondblond";
        itemCombinations[1, 2] = "Caillou";

        itemCombinations[2, 0] = "Blondblond";
        itemCombinations[2, 1] = "Metal";
        itemCombinations[2, 2] = "Bois";

        itemCombinations[3, 0] = "Arbablond";
        itemCombinations[3, 1] = "Metal";
        itemCombinations[3, 2] = "Conserve";

        itemCombinations[4, 0] = "Metal";
        itemCombinations[4, 1] = "Bois";
        itemCombinations[4, 2] = "Metal";

        itemCombinations[5, 0] = "Metal";
        itemCombinations[5, 1] = "Couteau";
        itemCombinations[5, 2] = "Bouteille";

        itemCombinations[6, 0] = "Bois";
        itemCombinations[6, 1] = "C1911";
        itemCombinations[6, 2] = "C1911";

        itemCombinations[7, 0] = "Charcute";
        itemCombinations[7, 1] = "Couteau";
        itemCombinations[7, 2] = "Conserve";

        itemCombinations[8, 0] = "Bois";
        itemCombinations[8, 1] = "Bois";
        itemCombinations[8, 2] = "Clous";

        itemCombinations[9, 0] = "Metal";
        itemCombinations[9, 1] = "Clous";
        itemCombinations[9, 2] = "Metal";

        itemCombinations[10, 0] = "Clouteuse";
        itemCombinations[10, 1] = "BatteClous";
        itemCombinations[10, 2] = "Clous";

        itemCombinations[11, 0] = "Clouteuse";
        itemCombinations[11, 1] = "Clouteuse";
        itemCombinations[11, 2] = "Essence";

        itemCombinations[12, 0] = "Essence";
        itemCombinations[12, 1] = "C1911";
        itemCombinations[12, 2] = "Conserve";

        itemCombinations[13, 0] = "Essence";
        itemCombinations[13, 1] = "Chalumtoi";
        itemCombinations[13, 2] = "Metal";

        itemCombinations[14, 0] = "Boring";
        itemCombinations[14, 1] = "Chalumtoi";
        itemCombinations[14, 2] = "Vodka";

        itemCombinations[15, 0] = "Laseros";
        itemCombinations[15, 1] = "Metal";
        itemCombinations[15, 2] = "Vodka";
    }

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
