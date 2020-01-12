using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    [SerializeField]
    private Weapon _weaponOrigin;

    private void OnTriggerEnter(Collider other) {
        _weaponOrigin.EnemyTrigger(other);
        Destroy(gameObject);
    }
}
