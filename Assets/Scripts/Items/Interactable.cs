using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    #region VARIABLES
    public Item item;
    #endregion

    #region METHODS

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            //Debug.Log(item.name + " picked.");
            // Check object type
            // Increment object type in gamemanager
            // Add to inventory
            // Update object type count
            bool wasPickedUp = Inventory.Instance.AddItem(item);
            
            if (wasPickedUp) {
                //item.itemCount++;
                Destroy(gameObject);

                Debug.Log(item.itemCount + " " + item.name);
            }
        }
    }

    #endregion
}
