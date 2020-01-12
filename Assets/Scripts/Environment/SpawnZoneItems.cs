using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZoneItems : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _itemPrefabs;
    [SerializeField]
    private float[] _itemRatio;
    //[SerializeField]
    private Transform[] itemSpots;
    
    void Start()
    {
        //get all children (pos)
        itemSpots = new Transform[transform.childCount];
        int i = 0;

        Transform[] _everyTransforms = GetComponentsInChildren<Transform>();
        foreach (Transform t in _everyTransforms) {
            if (t.gameObject != gameObject) {
                itemSpots[i] = t;
                i++;
            }
        }
        
        foreach(Transform spot in itemSpots) {
            GameObject randomPrefab = ChoosePrefabRandomly();

            if (randomPrefab != null) {
                Instantiate(randomPrefab, spot.position, spot.rotation);
            }
        }
    }

    private GameObject ChoosePrefabRandomly() {
        float minRatio = 100f;
        int indiceMinRatio = 0;

        for (int i = 0; i < _itemPrefabs.Length; i++) {
            if (_itemRatio[i] <= minRatio) {
                minRatio = _itemRatio[i];
                indiceMinRatio = i;
            }

            float rand = Random.Range(0f, 100f);

            if (rand <= _itemRatio[i]) {
                return _itemPrefabs[i];
            }
        }

        return _itemPrefabs[indiceMinRatio];
    }

}
