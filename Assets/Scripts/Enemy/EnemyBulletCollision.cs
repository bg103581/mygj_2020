using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCollision : MonoBehaviour 
{
    [SerializeField]
    private MobDistance _mobOrigin;

    private PlayerLife _playerLife;

    private void Start() {
        _playerLife = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            _playerLife.TakeDamage(_mobOrigin.damage);
        }
        Destroy(gameObject);
    }
}
