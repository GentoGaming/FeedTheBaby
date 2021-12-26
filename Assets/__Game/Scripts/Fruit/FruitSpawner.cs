using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _fruitPrototype = null;
    [SerializeField] private Transform _spawnPos = null;
    // Start is called before the first frame update
    void Start()
    {
        if(_fruitPrototype != null && _spawnPos != null)
        {
            GameObject fruit = Instantiate(_fruitPrototype);
            fruit.transform.position = _spawnPos.position;
            fruit.transform.rotation = _spawnPos.rotation;
        }
    }
}
