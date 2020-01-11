using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    #region VARIABLES
    public Transform MaterialsParent;
    public Transform WeaponsParent;
    
    Inventory inventory;
    InventorySlot[] materialSlots;
    InventorySlot[] weaponSlots;
    #endregion

    #region METHODS
    void Start() {
        inventory = Inventory.Instance;
        inventory.onItemChangedCallback += UpdateUI;

        materialSlots = MaterialsParent.GetComponentsInChildren<InventorySlot>();
        weaponSlots = WeaponsParent.GetComponentsInChildren<InventorySlot>();
    }

    void UpdateUI() {
        
        for (int i = 0; i < materialSlots.Length; i++) {
            if (i < inventory.ItemList.Count) {
                    materialSlots[i].AddToSlot(inventory.ItemList[i]);
            } else {
                materialSlots[i].ClearSlot();
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
