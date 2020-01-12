using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    #region VARIABLES
    public Image icon;
    public Transform ItemCount;

    public Item item = null; // Keep track of current item in the slot
    #endregion

    #region METHODS
    public void AddToSlot(Item newItem) {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void ClearSlot() {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }
    #endregion
}
