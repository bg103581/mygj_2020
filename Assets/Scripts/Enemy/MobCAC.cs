using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCAC : MonoBehaviour
{
    public float damage = 5f;
    public float attackRate = 2f;
    [HideInInspector]
    public float nextAttackTime = 0f;

    public float maxHealth = 20f;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
