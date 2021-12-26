using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{

    [SerializeField] private AnimationCurve _spawrate = default;
    [SerializeField] private GameObject _powerupObject = null;
    [SerializeField] private PowerupStats[] _powerupStats = new PowerupStats[6];
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    [Header("Try to make the the sum of all to be 100")]
    [Header("Last index is the baby scream")]
    [SerializeField] private int[] _powerWeight = new int[6];
    private List<Vector3> _unusedPositions = new List<Vector3>();
    private List<GameObject> _UsablePowerups = new List<GameObject>();

    private int _index = 0;
    private float _timer = 0f;
    private void Start()
    {
        EventManager.RegisterListener<BabyFedEventInfo>(SpawnPowerup);
        EventManager.RegisterListener<ActivatePowerUpEventInfo>(ReturnToList);
        GameObject powerUp = Instantiate(_powerupObject);
        powerUp.SetActive(false);
        _UsablePowerups.Add(powerUp);
        foreach(Transform trans in _spawnPoints)
        {
            _unusedPositions.Add(trans.position);
        }
        StartCoroutine(FinalCountdown());
    }

    private IEnumerator FinalCountdown()
    {
        while (true)
        {
            _timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private bool CheckSpawn()
    {
        int rand = Random.Range(0,100);
        if(rand <= _spawrate.Evaluate(_timer))
        {
            rand = Random.Range(0, 100);
            int total = 0;
            int counter = 0;
            foreach (int i in _powerWeight) 
            {
                total += i;
                if(rand <= total)
                {
                    _index = counter;
                    Debug.Log(rand + " has spawned the " + (PowerupType)_index + " power up.");
                    return true;
                }
                counter++;
            }
            _index = counter;
            return true;
        }
        
        return false;
    }

    void SpawnPowerup(EventInfo ei)
    {
        if (!CheckSpawn()) return;
        PowerupType power = (PowerupType)_index;
        if(power == PowerupType.Scream)
        {
            BabyScreamEventInfo Bsei = new BabyScreamEventInfo();
            EventManager.SendNewEvent(Bsei);
            Debug.Log("Baby scream");
            return;
        }
        if (_unusedPositions.Count <= 0)
            return;
        Vector3 spawnPoint = _unusedPositions[Random.Range(0, _unusedPositions.Count)];
        _unusedPositions.Remove(spawnPoint);
        PowerupStats targetStats = null;
        foreach (PowerupStats stat in _powerupStats)
        {
            if(stat.PowerupType == power)
            {
                targetStats = stat;
            }
        }

        if (_UsablePowerups.Count > 0)
        {
            _UsablePowerups[0].GetComponent<PoweupObject>().ChangePowerUp(targetStats); 
            _UsablePowerups[0].SetActive(true);
            _UsablePowerups[0].transform.position = spawnPoint;
            _UsablePowerups.RemoveAt(0);
        }
        else
        {
            GameObject powerUp = Instantiate(_powerupObject);
            powerUp.transform.position = spawnPoint;
            powerUp.GetComponent<PoweupObject>().ChangePowerUp(targetStats);
        }
    }
    private void ReturnToList(EventInfo ei)
    {
        ei.GO.SetActive(false);
        _UsablePowerups.Add(ei.GO);
        _unusedPositions.Add(ei.GO.transform.position);
    }

    private void OnDisable()
    {
        EventManager.UnregisterListener<BabyFedEventInfo>(SpawnPowerup);
        EventManager.UnregisterListener<ActivatePowerUpEventInfo>(ReturnToList);
    }

}
