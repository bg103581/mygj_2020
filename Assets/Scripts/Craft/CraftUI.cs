using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftUI : MonoBehaviour
{
    #region VARIABLES
    public Transform MaterialsParent;
    public Transform CraftParent;

    Inventory inventory;
    InventorySlot[] materialSlots;
    InventorySlot craftSlot;
    #endregion

    #region METHODS
    void Start() {
        inventory = Inventory.Instance;
        //inventory.onItemChangedCallback += UpdateCraftUI();

        materialSlots = MaterialsParent.GetComponentsInChildren<InventorySlot>();
        craftSlot = CraftParent.GetComponentInChildren<InventorySlot>();
    }

    public void UpdateCraftUI(Item item) {

        for (int i = 0; i < materialSlots.Length; i++) {
            if (materialSlots[i] == null) {
                materialSlots[i].AddToSlot(item);
                return;
            }
        }

        //for (int j = 0; j < weaponSlots.Length; j++) {
        //    if (j < inventory.ItemList.Count) {
        //        materialSlots[j].AddToSlot(inventory.ItemList[j]);
        //    }
        //}
    }
    #endregion
}
