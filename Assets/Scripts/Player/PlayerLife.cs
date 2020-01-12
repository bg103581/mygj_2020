using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour {
    #region VARIABLES

    private GameManager _gameManager;

    //public int DamageReceivedCAC;
    //public int DamageReceivedDISTANCE;
    //public int HealingReceived;

    #endregion

    #region AWAKE

    public void Awake() {
        _gameManager = FindObjectOfType<GameManager>();
    }

    #endregion

    #region UPDATE

    //public void Update() {
    //    NeedHealing();
    //    TakeDamage();
    //}

    #endregion

    #region METHODS

    public void NeedHealing(int HealingReceived) {
        _gameManager.AddHealth(HealingReceived);
        Debug.Log(_gameManager.Health);
    }

    public void TakeDamage(int DamageReceived) {
        _gameManager.SubstractHealth(DamageReceived);
        Debug.Log(_gameManager.Health);
        // else if (Input.GetKeyDown(KeyCode.D)) {
        //    _gameManager.SubstractHealth(DamageReceivedDISTANCE);
        //    Debug.Log(_gameManager.Health);
        //}

        if (_gameManager.Health == 0) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
        Debug.Log("You ded.");
    }

    #endregion

}
