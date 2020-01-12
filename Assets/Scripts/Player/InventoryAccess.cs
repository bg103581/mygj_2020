using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryAccess : MonoBehaviour
{
    #region VARIABLES
    public GameObject InventoryCanvas;
    public GameObject CraftCanvas;
    public GameObject GameCanvas;

    private GameObject GameCamera;
    private GameObject player;
    
    [SerializeField]
    private List<TextMeshProUGUI> SlotList = new List<TextMeshProUGUI>();
    [SerializeField]
    private List<TextMeshProUGUI> CraftSlotList = new List<TextMeshProUGUI>();
    #endregion

    #region AWAKE

    private void Awake() {
        
        foreach (Transform transform in InventoryCanvas.transform.Find("MaterialsParent").GetComponentInChildren<Transform>()) {
            SlotList.Add(transform.Find("ItemCount").GetComponent<TextMeshProUGUI>());
        }

        foreach (Transform transform in CraftCanvas.transform.Find("MaterialsParent").GetComponentInChildren<Transform>()) {
            CraftSlotList.Add(transform.Find("ItemCount").GetComponent<TextMeshProUGUI>());
        }

        InventoryCanvas.SetActive(false);
        CraftCanvas.SetActive(false);
    }

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        GameCamera = player.transform.Find("MainCamera").gameObject;
    }

    #endregion

    #region UPDATE

    public void Update() {
        if (InventoryCanvas.activeInHierarchy) {

            for (int i = 0; i < Inventory.Instance.ItemList.Count; i++) {
                if (Inventory.Instance.ItemList[i] != null) {
                    if (Inventory.Instance.ItemList[i].itemCount > 0) {
                        SlotList[i].text = Inventory.Instance.ItemList[i].itemCount.ToString();
                    } else {
                        SlotList[i].text = "";
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.I)) {
                GameCamera.GetComponent<MouseLook>().enabled = true;
                InventoryCanvas.SetActive(false);

                for (int i = 0; i < 3; i++) {
                    ClearCraftSlot(i);
                }

                CraftCanvas.SetActive(false);
            }

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        } else {
            if (Input.GetKeyDown(KeyCode.I)) {
                GameCamera.GetComponent<MouseLook>().enabled = false;
                InventoryCanvas.SetActive(true);
                CraftCanvas.SetActive(true);

                GameCanvas.GetComponent<InventoryUI>().UpdateUI();
            }

            Cursor.visible = false;
        }
    }

    private void ClearCraftSlot(int i) {
        if (i < Inventory.Instance.ItemList.Count) {
            if (Inventory.Instance.ItemList[i].craftCount > 0) {
                Inventory.Instance.ItemList[i].isInCraft = false;
                Inventory.Instance.ItemList[i].itemCount += Inventory.Instance.ItemList[i].craftCount;
                Inventory.Instance.ItemList[i].craftCount = 0;
                CraftSlotList[i].text = "";
                CraftSlotList[i].gameObject.GetComponentInParent<InventorySlot>().ClearSlot();
            }
        }
        
    }

    #endregion
}
