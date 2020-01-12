using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    #region VARIABLES
    public Transform MaterialsParent;
    public Transform WeaponsParent;
    public Transform CraftMaterialsParent;
    public Transform CraftParent;
    public GameObject CraftCanvas;

    private int craftCount = 0;
    private List<TextMeshProUGUI> CraftSlotList = new List<TextMeshProUGUI>();

    Inventory inventory;
    InventorySlot[] materialSlots;
    InventorySlot[] weaponSlots;
    InventorySlot[] craftMaterialSlots;
    InventorySlot craftSlot;
    #endregion

    #region METHODS

    private void Awake() {
        foreach (Transform transform in CraftCanvas.transform.Find("MaterialsParent").GetComponentInChildren<Transform>()) {
            CraftSlotList.Add(transform.Find("ItemCount").GetComponent<TextMeshProUGUI>());
        }
    }

    void Start() {
        inventory = Inventory.Instance;
        inventory.onItemChangedCallback += UpdateUI;

        materialSlots = MaterialsParent.GetComponentsInChildren<InventorySlot>();
        weaponSlots = WeaponsParent.GetComponentsInChildren<InventorySlot>();
        craftMaterialSlots = CraftMaterialsParent.GetComponentsInChildren<InventorySlot>();
        craftSlot = CraftParent.GetComponentInChildren<InventorySlot>();
    }

    public void UpdateUI() {
        
        for (int i = 0; i < materialSlots.Length; i++) {
            if (i < inventory.ItemList.Count) {
                if (inventory.ItemList[i].itemCount > 0) {
                    materialSlots[i].AddToSlot(inventory.ItemList[i]);
                } else {
                    materialSlots[i].ClearSlot();
                }
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

    public void OnItemClick(GameObject parent) {
        if (parent.GetComponent<InventorySlot>().item != null) {
            if (parent.GetComponent<InventorySlot>().item.itemCount > 0) {

                parent.GetComponent<InventorySlot>().item.itemCount--;

                for (int i = 0; i < craftMaterialSlots.Length; i++) {
                    if (parent.GetComponent<InventorySlot>().item.isInCraft) {
                        if (craftMaterialSlots[i].item == parent.GetComponent<InventorySlot>().item) {
                            parent.GetComponent<InventorySlot>().item.craftCount++;
                            CraftSlotList[i].text = parent.GetComponent<InventorySlot>().item.craftCount.ToString();
                            break;
                        }
                    } else {
                        if (craftMaterialSlots[i].item == null) {
                            craftMaterialSlots[i].AddToSlot(parent.GetComponent<InventorySlot>().item);
                            parent.GetComponent<InventorySlot>().item.craftCount++;
                            CraftSlotList[i].text = parent.GetComponent<InventorySlot>().item.craftCount.ToString();
                            parent.GetComponent<InventorySlot>().item.isInCraft = true;
                            break;
                        }
                    }
                }

                if (parent.GetComponent<InventorySlot>().item.itemCount <= 0) {
                    parent.GetComponent<InventorySlot>().item.itemCount = 0;
                    parent.GetComponent<InventorySlot>().item.isInInventory = false;
                    parent.GetComponent<InventorySlot>().ClearSlot();
                }
            }
        }
        

    }
    #endregion
}
