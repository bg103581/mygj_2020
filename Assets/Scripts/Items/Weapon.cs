using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Weapon : MonoBehaviour
{
    public float damage;
    public float bulletSpeed;
    public float attackRate;
    [HideInInspector]
    public float nextAttackTime = 0f;
    public bool isCac;
    
    public GameObject bullet;
    public Transform firePoint;

    [HideInInspector]
    public Collider col;

    private void Start() {
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other) {
        EnemyTrigger(other);
    }

    public void EnemyTrigger(Collider enemyCol) {
        if (enemyCol.tag == "EnemyCac") {
            DamageEnemy(enemyCol.transform, "cac");
        } else if (enemyCol.tag == "EnemyDistance") {
            DamageEnemy(enemyCol.transform, "distance");
        }
    } 

    private void DamageEnemy(Transform mob, string type) {
        GameObject mobGo = mob.transform.parent.gameObject;
        if (type == "cac") {
            Debug.Log("cac");
            mobGo.GetComponent<MobCAC>().currentHealth -= damage;
        }
        else if (type == "distance") {
            Debug.Log("distance");
            mobGo.GetComponent<MobDistance>().currentHealth -= damage;
        }
    }
}
