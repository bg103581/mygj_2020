using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryAccess : MonoBehaviour
{
    #region VARIABLES
    public GameObject InventoryCanvas;
    public GameObject GameCamera;
    
    [SerializeField]
    private List<TextMeshProUGUI> SlotList = new List<TextMeshProUGUI>();
    #endregion

    #region AWAKE

    private void Awake() {
        
        foreach (Transform transform in InventoryCanvas.transform.Find("MaterialsParent").GetComponentInChildren<Transform>()) {
            SlotList.Add(transform.Find("ItemCount").GetComponent<TextMeshProUGUI>());
        }

        InventoryCanvas.SetActive(false);
    }

    #endregion

    #region UPDATE

    public void Update() {
        if (InventoryCanvas.activeInHierarchy) {

            for (int i = 0; i < Inventory.Instance.ItemList.Count; i++) {
                if (Inventory.Instance.ItemList[i] != null) {
                    SlotList[i].text = Inventory.Instance.ItemList[i].itemCount.ToString();
                }
            }

            if (Input.GetKeyDown(KeyCode.I)) {
                GameCamera.GetComponent<MouseLook>().enabled = true;
                InventoryCanvas.SetActive(false);
            }

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        } else {
            if (Input.GetKeyDown(KeyCode.I)) {
                GameCamera.GetComponent<MouseLook>().enabled = false;
                InventoryCanvas.SetActive(true);
            }

            Cursor.visible = false;
        }
    }

    #endregion
}
