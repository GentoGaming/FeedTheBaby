using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyStats _stats = null;
    [Header("Update ... times per second")]
    [SerializeField] private float _updateFrecuency = 10;
    [SerializeField, Range(0.1f, 1.5f)] private float _minDistance = 0.1f;
    public Vector3 FruitTarget { get; set; } = Vector3.zero;
    public Transform FruitTransform { get; set; } = null;
    public bool FruitKidnapped { get; set; } = false;
    public GameObject Bubble { get; private set; } = null;
    public int Score { get { return _stats.Score; } }
    public bool Terrified { get; set; } = false;

    [Header("Audio Option")] 
    [SerializeField] private float _trappedVolume;
    
    private NavMeshAgent _enemyAgent = null;
    private float _frecuancyCounter = 0;
    private Vector3 _returnPoint = Vector3.zero;
    private EnemyStealFruit _enemyFruit = null;
    private Collider _enemyCollider = null;
    private InvisibleEnemy _invisible = null;
    private Transform _transform = default;

    private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Awake()
    {
        _enemyAgent = GetComponent<NavMeshAgent>();
        _enemyFruit = GetComponent<EnemyStealFruit>();
        _enemyCollider = GetComponent<Collider>();
        _invisible = GetComponent<InvisibleEnemy>();
        _enemyAgent.speed = _stats.MaxSpeed;
        _updateFrecuency = 1 / _updateFrecuency;
        _animator = GetComponent<Animator>();
        _transform = transform;
    }

    public void OnEnemySpawn()
    {
        EventManager.RegisterListener<BubbleArrivedToNest>(DisableAgentScript);
        gameObject.SetActive(true);
    }

    private void DisableAgentScript(EventInfo eventinfo)
    {
        BubbleArrivedToNest Batnei = (BubbleArrivedToNest)eventinfo;
        if (Batnei.GO == gameObject)
        {
            _enemyAgent.enabled = false;
        }
    }

    private void Update()
    {
        _animator.SetFloat(Speed, _enemyAgent.velocity.magnitude > 1 ? 1f : 0f);

        _frecuancyCounter += Time.deltaTime;
        if (_frecuancyCounter >= _updateFrecuency)
        {
            if (_enemyAgent.isActiveAndEnabled)
            {
                _enemyAgent.destination = FruitTarget;
            }

            _frecuancyCounter = 0;
        }

        CheckDistance();
    }

    public virtual void ActivateEnemy(Transform target, Vector3 spawnPoint)
    {
        OnEnemySpawn();
        _enemyAgent.enabled = false;
        _invisible?.TurnInvisible();
        FruitTarget = target.position;
        FruitTransform = target;
        Bubble = null;
        _returnPoint = spawnPoint;
        transform.position = spawnPoint;
        FruitKidnapped = false;
        Terrified = false;
        _enemyFruit.RemoveFruit();
        _enemyAgent.enabled = true;
        ChangeAgentStatus(true);
    }

    public void ChangeAgentStatus(bool status)
    {
        _enemyAgent.isStopped = !status;
        _enemyCollider.enabled = status;
    }

    public void ChangeToReturn()
    {
        FruitTarget = _returnPoint;
    }

    public void ChangeMoveSpeed(float decrease)
    {
        float procentage = 1 - (decrease / 100);
        _enemyAgent.speed = _stats.MaxSpeed * procentage;
    }

    private void CheckDistance()
    {
        Vector3 diff = transform.position - FruitTarget;
        if (diff.magnitude < 1 && (Terrified || FruitKidnapped))
        {
            ReturnEnemyToPoolEventInfo Retpei = new ReturnEnemyToPoolEventInfo(gameObject, "returning");
            EventManager.SendNewEvent(Retpei);
        }
    }

    public void GetScared()
    {
        Terrified = true;
        FruitTarget = _returnPoint;
    }

    public void AssignBubble(GameObject bubble)
    {
        AudioEventInfo aei = new AudioEventInfo(SoundManager.EnemyTrappedSound, _trappedVolume,_transform.position);
        EventManager.SendNewEvent(aei);
        
        Bubble = bubble;
    }

    private void OnDisable()
    {
        EventManager.UnregisterListener<BubbleArrivedToNest>(DisableAgentScript);
    }
}
