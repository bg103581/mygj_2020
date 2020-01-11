using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    #region VARIABLES
    public float radius = 3f;
    #endregion

    #region METHODS

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }

    #endregion
}
