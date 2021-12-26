using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab = null;
    [SerializeField] private GameObject _invisibleEnemyPrefab = null;
    [SerializeField] private GameObject _grossEnemy = null;

    [SerializeField] private List<Transform> _spawnLocations = new List<Transform>();

    [Header("Spawning Options")] 
    [SerializeField] private AnimationCurve _fruitEnemiesSpawnCurve = default;
    [SerializeField] private AnimationCurve _grossEnemiesSpawnCurve = default;
    [Space] 
    [SerializeField] private float _defualtEnemyWeigth = 50f;
    [SerializeField] private float _ghostEnemyWeigth = 50f;
    
    [Header("Audio Option")] 
    [SerializeField] private float _grossEnemyEatenVolume = 1;
    [SerializeField] private float _enemyEatenVolume = 1;
    
    private Dictionary<FruitType, List<Transform>> _activeFruits = new Dictionary<FruitType, List<Transform>>();

    private Dictionary<GameObject, EnemyBehaviour> _usableEnemies = new Dictionary<GameObject, EnemyBehaviour>();

    private Dictionary<GameObject, GrossEnemyBehaviour> _usableGrossEnemies = new Dictionary<GameObject, GrossEnemyBehaviour>();

    private List<FruitType> _activeSeason = new List<FruitType>();

    private int index = 0;
    void Awake()
    {
        EventManager.RegisterListener<AssignFruitEventInfo>(AssignFruitToList);
        EventManager.RegisterListener<ReturnEnemyToPoolEventInfo>(ReturnToPool);
        EventManager.RegisterListener<ChangeSeasonEventInfo>(ChangeSeason);
    }

    private float _timer = 0;
    private float _grossTimer = 0;
    private float _totalTime = 0;
    private void Update()
    {
        _timer += Time.deltaTime;
        _grossTimer += Time.deltaTime;
        _totalTime += Time.deltaTime;
        
        if (_grossTimer > _grossEnemiesSpawnCurve.Evaluate(_totalTime))
        {
            SpawnGrossEnemy();
            _grossTimer = 0f;
        }
        
        if(_timer > _fruitEnemiesSpawnCurve.Evaluate(_totalTime))
        {
            SpawnEnemy();
            _timer = 0f;
        }
    }

    private void ReturnGrossEnemy(GameObject obj, GrossEnemyBehaviour behaviour)
    {
        _usableGrossEnemies.Add(obj, behaviour);
        obj.SetActive(false);

        ReturnBubbleEventInfo Rbei = new ReturnBubbleEventInfo(_usableGrossEnemies[obj].Bubble, "");
        EventManager.SendNewEvent(Rbei);

        if (behaviour.Defeated)
        {
            IncreasePlayerScoreEventInfo Ipsei = new IncreasePlayerScoreEventInfo(_usableGrossEnemies[obj].Score);
            EventManager.SendNewEvent(Ipsei);
            
            AudioEventInfo aei = new AudioEventInfo(SoundManager.ToxicFood, _enemyEatenVolume, transform.position);
            EventManager.SendNewEvent(aei);
        }
    }

    private void ReturnToPool(EventInfo ei)
    {
        GrossEnemyBehaviour behaviour = ei.GO.GetComponent<GrossEnemyBehaviour>();
        if (behaviour != null)
        {
            ReturnGrossEnemy(ei.GO, behaviour);

            return;
        }
        _usableEnemies.Add(ei.GO, ei.GO.GetComponent<EnemyBehaviour>());
        if (_usableEnemies[ei.GO].FruitKidnapped == false)
        {
            if (_usableEnemies[ei.GO].Terrified != true)
            {
                IncreasePlayerScoreEventInfo Ipsei = new IncreasePlayerScoreEventInfo(_usableEnemies[ei.GO].Score);
                EventManager.SendNewEvent(Ipsei);
                
                AudioEventInfo aei = new AudioEventInfo(SoundManager.ChonccEatingSound, _grossEnemyEatenVolume, ei.GO.transform.position);
                EventManager.SendNewEvent(aei);
            }

            FruitBehaviour fruit = _usableEnemies[ei.GO].FruitTransform.GetComponent<FruitBehaviour>();
            _activeFruits[fruit.TypeOfFruit].Add(_usableEnemies[ei.GO].FruitTransform);

            ReturnBubbleEventInfo Rbei = new ReturnBubbleEventInfo(_usableEnemies[ei.GO].Bubble, "");
            EventManager.SendNewEvent(Rbei);
        }
        else
        {
            RemovingEnemyFromGuideEventInfo Refgei = new RemovingEnemyFromGuideEventInfo(ei.GO);
            EventManager.SendNewEvent(Refgei);
            KidnappedFruitEventInfo Kfei = new KidnappedFruitEventInfo();
            EventManager.SendNewEvent(Kfei);
        }

        ei.GO.SetActive(false);
    }

    private void AssignFruitToList(EventInfo ei)
    {
        FruitBehaviour fruit = ei.GO.GetComponent<FruitBehaviour>();
        if (fruit != null)
        {
            if (_activeFruits.ContainsKey(fruit.TypeOfFruit))
            {
                _activeFruits[fruit.TypeOfFruit].Add(fruit.transform);
            }
            else
            {
                _activeFruits.Add(fruit.TypeOfFruit, new List<Transform>());
                _activeFruits[fruit.TypeOfFruit].Add(fruit.transform);
            }
        }
    }

    private void ChangeSeason(EventInfo ei)
    {
        ChangeSeasonEventInfo Csei = (ChangeSeasonEventInfo)ei;
        _activeSeason.Clear();
        foreach(FruitType fruit in Csei.ActiveSeason)
        {
            _activeSeason.Add(fruit);
        }
    }

    private void SpawnGrossEnemy()
    {
        List<Vector3> trans = new List<Vector3>();
        foreach(FruitType fruit in _activeFruits.Keys)
        {
            if(_activeFruits[fruit].Count > 0)
            {
                int rand = Random.Range(0, _activeFruits[fruit].Count);
                trans.Add(_activeFruits[fruit][rand].position);
            }
        }
        int randIndex = Random.Range(0, _spawnLocations.Count);
        Vector3 spawnPos = _spawnLocations[randIndex].position;
        GameObject enemy;
        if (_usableGrossEnemies.Count > 0)
        {
            enemy = _usableGrossEnemies.First().Key;
            _usableGrossEnemies[enemy].ActivateEnemy(trans, spawnPos);
            _usableGrossEnemies.Remove(enemy);
        }
        else
        {
            enemy = Instantiate(_grossEnemy);
            GrossEnemyBehaviour grossBehaviour = enemy.GetComponent<GrossEnemyBehaviour>();
            grossBehaviour.ActivateEnemy(trans, spawnPos);
        }
    }

    private void SpawnEnemy()
    {
        int rand = Random.Range(0, _spawnLocations.Count);
        Transform targetTrans;
        Vector3 spawnPos = _spawnLocations[rand].position;
        GameObject enemy = null;
        if (_usableEnemies.Count > 0)
        {
            targetTrans = GetTargetFruit();
            if (targetTrans != null)
            {
                enemy = _usableEnemies.Keys.First();
                _usableEnemies[enemy].ActivateEnemy(targetTrans, spawnPos);
                _usableEnemies.Remove(enemy);
            }
        }
        else
        {
            targetTrans = GetTargetFruit();
            if (targetTrans != null)
            {
                float ran = Random.Range(0, _defualtEnemyWeigth + _ghostEnemyWeigth);
                switch (ran)
                {
                    case float i when ran <= _defualtEnemyWeigth:
                        enemy = Instantiate(_enemyPrefab);
                        break;
                    case float i when ran > _defualtEnemyWeigth:
                        enemy = Instantiate(_invisibleEnemyPrefab);
                        break;
                }

                EnemyBehaviour behaviour = enemy.GetComponent<EnemyBehaviour>();
                behaviour.ActivateEnemy(targetTrans, spawnPos);
            }
        }
    }

    private Transform GetTargetFruit()
    {
        foreach(FruitType type in _activeSeason)
        {
            if (_activeFruits.ContainsKey(type))
            {
                if (_activeFruits[type].Count > 0)
                {
                    Transform trans = _activeFruits[type][0];
                    _activeFruits[type].RemoveAt(0);
                    return trans;
                }
            }
        }
        return null;
    }

    void OnDisable()
    {
        EventManager.UnregisterListener<ReturnEnemyToPoolEventInfo>(ReturnToPool);
        EventManager.UnregisterListener<AssignFruitEventInfo>(AssignFruitToList);
        EventManager.UnregisterListener<ChangeSeasonEventInfo>(ChangeSeason);
    }
}
