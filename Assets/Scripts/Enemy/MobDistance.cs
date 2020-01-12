﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobDistance : MonoBehaviour
{
    public int damage = 5;
    public float attackRate = 2f;
    [HideInInspector]
    public float nextAttackTime = 0f;

    public float damageDistance = 10f;
    public float attackRateDistance = 1.5f;
    [HideInInspector]
    public float nextAttackTimeDistance = 0f;
    public float bulletSpeed = 10f;

    public float maxHealth = 20f;
    public float currentHealth;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager.actualNbMobs += 1;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0f) {
            _gameManager.actualNbMobs -= 1;
            _gameManager.score += 7;
            Destroy(gameObject);
        }
    }
}
