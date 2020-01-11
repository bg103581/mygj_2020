using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;

    [SerializeField]
    private Transform[] _spawnPositions;

    // Start is called before the first frame update
    void Start()
    {
        //get child transforms
        _spawnPositions = new Transform[transform.childCount];
        int i = 0;

        Transform[] _everyTransforms = gameObject.GetComponentsInChildren<Transform>();
        foreach(Transform t in _everyTransforms) {
            if (t.gameObject != this.gameObject) {
                _spawnPositions[i] = t;
                i++;
            }
        }

        //spawn prefab player to random child transform
        Instantiate(_playerPrefab, _spawnPositions[Random.Range(0, transform.childCount - 1)].position, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
    }
}
