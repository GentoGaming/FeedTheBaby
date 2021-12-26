using UnityEngine;

[DefaultExecutionOrder(-100)]
public class GameManagement : MonoBehaviour
{
    private static GameManagement instance = null;
    [SerializeField] private SeasonClock _seasons = null;
    [SerializeField] private Transform _nestObject = null;
    [SerializeField] private GameObject _playerObject = null;
    [SerializeField] private Camera _mainCamera = null;

    private float _seasonTimer = 0;
    private int _seasonIndex = 0;

    void Awake()
    {    
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        ChangeSeasonEventInfo Csei = new ChangeSeasonEventInfo(_seasons.SeasonsOrder[_seasonIndex].ActiveSeasons);
        EventManager.SendNewEvent(Csei);
    }

    private void Update()
    {
        _seasonTimer += Time.deltaTime;

        if(_seasonTimer >= _seasons.TimeInBetween)
        {
            _seasonIndex++;
            if(_seasonIndex >= _seasons.SeasonsOrder.Length)
            {
                _seasonIndex = _seasons.SeasonsOrder.Length - 1;
            }
            _seasonTimer = 0;
            ChangeSeasonEventInfo Csei = new ChangeSeasonEventInfo(_seasons.SeasonsOrder[_seasonIndex].ActiveSeasons);
            EventManager.SendNewEvent(Csei);
        }
    }

    public static Camera GetMainCamera()
    {
        return instance._mainCamera;
    }
    
    
    public static Vector3 GetNestPosition()
    {
        return instance._nestObject.position;
    }

    public static GameObject GetPlayer()
    {
        return instance._playerObject;
    }
}
