using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    #region VARIABLES
    public Item item;
    public float radius = 3f;
    #endregion

    #region METHODS

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            //Debug.Log(item.name + " picked.");
            // Check object type
            // Increment object type in gamemanager
            // Add to inventory
            // Update object type count
            bool wasPickedUp = Inventory.Instance.AddItem(item);
            
            if (wasPickedUp) {
                item.itemCount++;
                Destroy(gameObject);

                Debug.Log(item.itemCount + " " + item.name);
            }
        }
    }

    #endregion
}
