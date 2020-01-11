using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMobs : MonoBehaviour
{
    [SerializeField]
    private GameObject _mobPrefab;
    [SerializeField]
    private int _nbMobToSpawn = 5;
    [SerializeField]
    private int _nbMobToAdd = 3;
    private int _actualNbMobToSpawn;
    [SerializeField]
    private float _timeToSpawn = 2f;
    [SerializeField]
    private float _timeToSub = 0.5f;
    private float _actualTimeToSpawn;
    
    private Transform[] _spawnPositions;
    private Transform _actualZoneTransform;

    private int _nbChild;
    [HideInInspector]
    public int _actualZone = 0;

    private bool _isSpawning;
    private bool _stopSpawn;

    // Start is called before the first frame update
    void Start() {
        _nbChild = transform.childCount;
        _actualZoneTransform = transform.GetChild(_actualZone);
        _actualNbMobToSpawn = _nbMobToSpawn;
        _actualTimeToSpawn = _timeToSpawn;

        //get child transforms
        UpdateSpawnPos();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) && !_isSpawning && !_stopSpawn) {
            StartCoroutine("Spawn");
        }
    }

    IEnumerator Spawn() {
        _isSpawning = true;

        for (int i = 0; i < _actualNbMobToSpawn; i++) {
            int indexPos = Random.Range(0, _actualZoneTransform.childCount);
            yield return new WaitForSeconds(_actualTimeToSpawn);
            Instantiate(
                _mobPrefab, 
                _spawnPositions[indexPos].position, 
                Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
        }

        _actualZone++;
        if (!_stopSpawn) {
            _stopSpawn = _actualZone > transform.childCount - 1;

            try {
                _actualZoneTransform = transform.GetChild(_actualZone);
            } catch (System.Exception) {
                Debug.Log("nik");
            }
            
            _actualNbMobToSpawn += _nbMobToAdd;
            _actualTimeToSpawn -= _timeToSub;

            UpdateSpawnPos();
        }

        _isSpawning = false;
    }

    private void UpdateSpawnPos() {
        _spawnPositions = new Transform[_actualZoneTransform.childCount];
        int i = 0;

        Transform[] _everyTransforms = _actualZoneTransform.gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform t in _everyTransforms) {
            if (t.gameObject != _actualZoneTransform.gameObject) {
                _spawnPositions[i] = t;
                i++;
            }
        }
    }
}
