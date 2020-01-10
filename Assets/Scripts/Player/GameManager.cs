using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    #region VARIABLES

    private int _health;

    #endregion

    #region AWAKE

    public override void Awake() {
        Init();
    }

    #endregion

    #region METHODS

    public int Health {
        get {
            return _health;
        }

        set {
            if (value >= 0) {
                _health = value;
            }
        }
    }

    public void Init() {
        Health = 1000;
    }

    public void AddHealth(int healthToAdd) {
        if (healthToAdd >= 0) { // assure that added health is always positive

            Health += healthToAdd;

            if (Health > 1000) {
                Health = 1000; // set maximum health to 1000
            }
        }
    }

    public void SubstractHealth(int healthToSubstract) {
        if (healthToSubstract >= 0) {
            Health -= healthToSubstract;

            if (Health < 0) {
                Health = 0;
            }
        }
    }

    #endregion
}
