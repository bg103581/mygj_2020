using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    #region VARIABLES

    private int _health;

    [SerializeField]
    private float _startTimer;
    [SerializeField]
    private float _vagueTimer;
    private int _nbVague; // nbShrink = nbVague - 1
    [SerializeField]
    private ShrinkingZone _shrinkingZone;
    [SerializeField]
    private GameObject _spawnMobsGO;
    private SpawnMobs _spawnMobs;

    #endregion

    #region AWAKE

    public override void Awake() {
        Init();
    }

    #endregion

    private void Start() {
        StartCoroutine("StartGame");
    }

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
        _spawnMobs = _spawnMobsGO.GetComponent<SpawnMobs>();
        _nbVague = _spawnMobsGO.transform.childCount;
        _shrinkingZone.nbShrink = _nbVague - 1;

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

    private IEnumerator StartGame() {
        yield return new WaitForSeconds(_startTimer);
        //vague de mobs + timer vague
        _spawnMobs.Spawn();
        yield return new WaitForSeconds(_vagueTimer);

        //shrink zone + vague + timer jusqu'a la fin
        for (int i = 0; i < _nbVague - 1; i++) {
            _shrinkingZone.Shrink();
            _spawnMobs.Spawn();
            yield return new WaitForSeconds(_vagueTimer);
        }
    }

    #endregion
}
