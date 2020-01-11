using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Interactable") {
            // Check object type
            // Increment object type in gamemanager
            // Add to inventory
            // Update object type count
            Debug.Log("Object obtained.");
        }
    }
}
