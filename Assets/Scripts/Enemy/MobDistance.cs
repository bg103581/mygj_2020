using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobDistance : MonoBehaviour
{
    public float damage = 5f;
    public float attackRate = 2f;
    [HideInInspector]
    public float nextAttackTime = 0f;

    public float damageDistance = 10f;
    public float attackRateDistance = 1.5f;
    [HideInInspector]
    public float nextAttackTimeDistance = 0f;

    public float maxHealth = 20f;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0f) {
            Destroy(gameObject);
        }
    }
}
