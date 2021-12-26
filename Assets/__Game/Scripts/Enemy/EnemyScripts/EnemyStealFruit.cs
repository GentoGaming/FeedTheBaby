using UnityEngine;

public class EnemyStealFruit : MonoBehaviour, IInteractFruit
{
    [SerializeField] private Transform _fruitHolder = null;
    [Header("Speed decrease while carrying a fruit in procent")]
    [SerializeField,Range(0f, 50f)] private float _speedDecrease = 20f;

    [Header("Audio Options")] 
    [SerializeField] private float _pickUpFruitVolume = 1f;

    
    private EnemyBehaviour _enemyBehaviour = null;
    private GameObject _pickedFruit = null;
    private InvisibleEnemy _invisible = null;
    private Animator _animator = default;
    private Transform _transform;
    private static readonly int PickingUp = Animator.StringToHash("PickingUp");
    private static readonly int IsHolding = Animator.StringToHash("IsHolding");

    void Awake()
    {
        EventManager.RegisterListener<BabyScreamEventInfo>(TerrifyEnemy);
        _enemyBehaviour = GetComponent<EnemyBehaviour>();
        _invisible = GetComponent<InvisibleEnemy>();
        _animator = GetComponent<Animator>();
        _transform = transform;
    }

    public void PickUpFruit(GameObject fruit)
    {
        if (_enemyBehaviour.Terrified)
        {
            return;
        }
        else if (_enemyBehaviour.Bubble == null && fruit.transform == _enemyBehaviour.FruitTransform)
        {
            _invisible?.NoLongerStealth();
            AddingEnemyToGuideEventInfo Aetgei = new AddingEnemyToGuideEventInfo(gameObject);
            EventManager.SendNewEvent(Aetgei);
            _pickedFruit = fruit;
           
            AudioEventInfo aei = new AudioEventInfo(SoundManager.EnemyFoodSound, _pickUpFruitVolume, _transform.position);
            EventManager.SendNewEvent(aei);
            
            _animator.SetBool(IsHolding, true);
            _animator.SetTrigger(PickingUp);
            
            _pickedFruit.transform.SetParent(_fruitHolder);
            _enemyBehaviour.ChangeToReturn();
            _enemyBehaviour.ChangeMoveSpeed(_speedDecrease);
            _pickedFruit.transform.localPosition = Vector3.zero;
            _enemyBehaviour.FruitKidnapped = true;
        }
    }

    public void RemoveFruit()
    {
        if (_pickedFruit != null)
        {
            _animator.SetBool(IsHolding, false);
            _pickedFruit.SetActive(false);
            _pickedFruit = null;
        }
    }

    public void DropFruit()
    {
        _animator.SetBool(IsHolding, false);
        RemovingEnemyFromGuideEventInfo Refgei = new RemovingEnemyFromGuideEventInfo(gameObject);
        EventManager.SendNewEvent(Refgei);
        _pickedFruit.transform.SetParent(null);
        _enemyBehaviour.ChangeMoveSpeed(0);
        _enemyBehaviour.FruitTarget = _pickedFruit.transform.position;
        _pickedFruit = null;
        _enemyBehaviour.FruitKidnapped = false;
    }

    void TerrifyEnemy(EventInfo ei)
    {
        _invisible?.NoLongerStealth();
        if (_enemyBehaviour.FruitKidnapped)
        {
            DropFruit();
            _enemyBehaviour.GetScared();
        }
    }

    void OnDisable()
    {
        EventManager.UnregisterListener<BabyScreamEventInfo>(TerrifyEnemy);
    }
}
