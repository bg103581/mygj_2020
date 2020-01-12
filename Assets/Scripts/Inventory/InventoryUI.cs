using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    #region VARIABLES
    public Transform MaterialsParent;
    public Transform CraftMaterialsParent;
    public Transform CraftParent;
    public GameObject CraftCanvas;

    public Transform weaponPos;
    public GameObject weaponPrefab;
    public Transform weaponParent;

    private int craftCount = 0;
    private List<TextMeshProUGUI> CraftSlotList = new List<TextMeshProUGUI>();
    string[] itemNames;

    Inventory inventory;
    InventorySlot[] materialSlots;
    public InventorySlot[] craftMaterialSlots;
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
        craftMaterialSlots = CraftMaterialsParent.GetComponentsInChildren<InventorySlot>();
        craftSlot = CraftParent.GetComponentInChildren<InventorySlot>();

        weaponPos = GameObject.FindGameObjectWithTag("Player").transform.Find("MainCamera").Find("PivotWeapon").Find("GunPosition");
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
                
                for (int i = 0; i < craftMaterialSlots.Length; i++) {
                    //if (parent.GetComponent<InventorySlot>().item.isInCraft) {
                    //    if (craftMaterialSlots[i].item == parent.GetComponent<InventorySlot>().item) {
                    //        parent.GetComponent<InventorySlot>().item.itemCount--;
                    //        parent.GetComponent<InventorySlot>().item.craftCount++;
                    //        CraftSlotList[i].text = parent.GetComponent<InventorySlot>().item.craftCount.ToString();
                    //        break;
                    //    }
                    //} else {
                    //    if (craftMaterialSlots[i].item == null) {
                    //        Debug.Log(craftMaterialSlots[i] + " slot IS NULL");
                    //        craftMaterialSlots[i].AddToSlot(parent.GetComponent<InventorySlot>().item);
                    //        parent.GetComponent<InventorySlot>().item.itemCount--;
                    //        parent.GetComponent<InventorySlot>().item.craftCount++;
                    //        CraftSlotList[i].text = parent.GetComponent<InventorySlot>().item.craftCount.ToString();
                    //        parent.GetComponent<InventorySlot>().item.isInCraft = true;
                    //        break;
                    //    }
                    //}

                    if (craftMaterialSlots[i].item == null) {
                        Debug.Log(craftMaterialSlots[i] + " slot IS NULL");
                        craftMaterialSlots[i].AddToSlot(parent.GetComponent<InventorySlot>().item);
                        parent.GetComponent<InventorySlot>().item.itemCount--;
                        parent.GetComponent<InventorySlot>().item.craftCount++;
                        CraftSlotList[i].text = parent.GetComponent<InventorySlot>().item.craftCount.ToString();
                        parent.GetComponent<InventorySlot>().item.isInCraft = true;
                        break;
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

    public void OnCraftClick(GameObject parent) {
        if (parent.GetComponent<InventorySlot>().item != null) {
            if (parent.GetComponent<InventorySlot>().item.craftCount > 0) {
                for (int i = 0; i < materialSlots.Length; i++) {
                    if (parent.GetComponent<InventorySlot>().item.isInInventory) {
                        if (materialSlots[i].item == parent.GetComponent<InventorySlot>().item) {
                            if (parent.GetComponent<InventorySlot>().item.craftCount > 0) {
                                parent.GetComponent<InventorySlot>().item.craftCount--;
                                parent.GetComponent<InventorySlot>().item.itemCount++;
                                //CraftSlotList[i].text = parent.GetComponent<InventorySlot>().item.craftCount.ToString();
                            }
                        }
                    } else {
                        if (materialSlots[i].item == null) {
                            materialSlots[i].AddToSlot(parent.GetComponent<InventorySlot>().item);
                            parent.GetComponent<InventorySlot>().item.craftCount--;
                            parent.GetComponent<InventorySlot>().item.itemCount++;
                            //CraftSlotList[i].text = parent.GetComponent<InventorySlot>().item.craftCount.ToString();
                            parent.GetComponent<InventorySlot>().item.isInInventory = true;
                            break;
                        }
                    }
                }

                parent.GetComponent<InventorySlot>().gameObject.transform.Find("ItemCount").GetComponent<TextMeshProUGUI>().text = parent.GetComponent<InventorySlot>().item.craftCount.ToString();

                if (parent.GetComponent<InventorySlot>().item.craftCount <= 0) {
                    parent.GetComponent<InventorySlot>().item.craftCount = 0;
                    parent.GetComponent<InventorySlot>().item.isInCraft = false;
                    parent.GetComponent<InventorySlot>().ClearSlot();
                    parent.GetComponent<InventorySlot>().gameObject.transform.Find("ItemCount").GetComponent<TextMeshProUGUI>().text = "";
                }
            }
        }
    }

    public void TryCraft() {
        itemNames = new string[3];

        for (int i = 0; i < 3; i++) {
            if (craftMaterialSlots[i].item != null) {
                Debug.Log(craftMaterialSlots[i].item.name);
                itemNames[i] = craftMaterialSlots[i].item.name;
                Debug.Log(itemNames[i]);
            }
        }

        string[] nameCombo = new string[3];
        for (int j = 0; j < 16; j++) {
            for (int k = 0; k < 3; k++) {
                nameCombo[k] = Inventory.Instance.itemCombinations[j, k];
            }
            if (compareArray<string>(itemNames, nameCombo)) {
                weaponPrefab = Inventory.Instance.weapons[j];
                Instantiate(weaponPrefab, weaponPos.position, weaponPos.rotation);
                weaponPrefab.transform.SetParent(weaponPos);
            }
        }
    }

    public static bool compareArray<T>(T[] aListA, T[] aListB) {
        if (aListA == null || aListB == null || aListA.Length != aListB.Length)
            return false;
        if (aListA.Length == 0)
            return true;
        Dictionary<T, int> lookUp = new Dictionary<T, int>();
        // create index for the first list
        for (int i = 0; i < aListA.Length; i++) {
            int count = 0;
            if (!lookUp.TryGetValue(aListA[i], out count)) {
                lookUp.Add(aListA[i], 1);
                continue;
            }
            lookUp[aListA[i]] = count + 1;
        }
        for (int i = 0; i < aListB.Length; i++) {
            int count = 0;
            if (!lookUp.TryGetValue(aListB[i], out count)) {
                // early exit as the current value in B doesn't exist in the lookUp (and not in ListA)
                return false;
            }
            count--;
            if (count <= 0)
                lookUp.Remove(aListB[i]);
            else
                lookUp[aListB[i]] = count;
        }
        // if there are remaining elements in the lookUp, that means ListA contains elements that do not exist in ListB
        return lookUp.Count == 0;
    }
    #endregion
}
