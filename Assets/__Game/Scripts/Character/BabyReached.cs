using UnityEngine;

public class BabyReached : MonoBehaviour
{
    [SerializeField] private Transform _babyMesh = null;
    [Header("Decide how many procent it should increase when eating")]
    [SerializeField, Range(1, 100)] private int _sizeIncrease = 5;

    [Header("Audio")] 
    [SerializeField] private float _eatingVolume = 1f;
    
    private Vector3 _currentSize = Vector3.one;
    private float _procentage = 1f;
    private Transform _transform;

    private void Awake(){
        EventManager.RegisterListener<BabyFedEventInfo>(BabyFed);
        _transform = transform;
    }

    void Start()
    {
        _procentage = (float)_sizeIncrease / (float)100;
    }

    void UpdateSize()
    {
        _babyMesh.localScale = _currentSize;
    }

    void BabyFed(EventInfo ei)
    {
        _currentSize += _currentSize.normalized * _procentage;
        UpdateEnemyMinDistanceEventInfo Uemdei = new UpdateEnemyMinDistanceEventInfo(_currentSize.x * 0.5f + 0.1f, gameObject, "New size for the baby");
        EventManager.SendNewEvent(Uemdei);

        UpdateSize();
    }

    private void OnDisable()
    {
        EventManager.UnregisterListener<BabyFedEventInfo>(BabyFed);
    }
}
