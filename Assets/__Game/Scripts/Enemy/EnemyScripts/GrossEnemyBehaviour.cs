using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrossEnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyStats _stats = null;

    public GameObject Bubble { get; private set; } = null;
    public int Score { get { return _stats.Score; } }

    public bool Defeated { get; private set; }

    private NavMeshAgent _enemyAgent = null;
    private Vector3 _returnPoint = Vector3.zero;
    private Collider _enemyCollider = null;
    [SerializeField] private List<Vector3> _routes = new List<Vector3>();
    private int _index = 0;

    private void Awake()
    {
        _enemyAgent = GetComponent<NavMeshAgent>();
        _enemyCollider = GetComponent<Collider>();
        _enemyAgent.speed = _stats.MaxSpeed;
    }

    public void OnEnemySpawn()
    {
        EventManager.RegisterListener<BubbleArrivedToNest>(DisableAgentScript);
    }

    public void Update()
    {
        if (_enemyAgent.isActiveAndEnabled)
        {
            if(_index < _routes.Count)
            _enemyAgent.destination = _routes[_index];
        }
        CheckDistance();
    }

    public virtual void ActivateEnemy(List<Vector3> targets, Vector3 spawnPoint)
    {
        _enemyAgent.enabled = false;
        OnEnemySpawn();
        gameObject.SetActive(true);
        Defeated = false;
        Bubble = null;
        _index = 0;
        _routes.Clear();
        transform.position = spawnPoint;
        foreach(Vector3 target in targets)
        {
            _routes.Add(target);
        }
        _routes.Add(spawnPoint);
        _enemyAgent.enabled = true;
        ChangeAgentStatus(true);
        _enemyAgent.SetDestination(_routes[_index]);
    }

    private void CheckDistance()
    {
        if (_index >= _routes.Count)
        {
            ReturnGrossEnemyEventInfo Rgeei = new ReturnGrossEnemyEventInfo(this, gameObject, "returning");
            EventManager.SendNewEvent(Rgeei);
        }
        else
        {
            Vector3 diff = transform.position - _routes[_index];
            if (diff.magnitude < 1)
            {
                _index += 1;
                if (_index >= _routes.Count)
                {
                    ReturnEnemyToPoolEventInfo retpei = new ReturnEnemyToPoolEventInfo(gameObject, "returning Gros enemy");
                    EventManager.SendNewEvent(retpei);
                }
                else
                {
                    _enemyAgent.SetDestination(_routes[_index]);
                }
            }
        }
    }

    public void AssignBubble(GameObject bubble)
    {
        Bubble = bubble;
    }

    private void DisableAgentScript(EventInfo eventinfo)
    {
        BubbleArrivedToNest Batnei = (BubbleArrivedToNest)eventinfo;
        if (Batnei.GO == gameObject)
        {
            _enemyAgent.enabled = false;
        }
    }

    public void ChangeAgentStatus(bool status)
    {
        _enemyAgent.isStopped = !status;
        _enemyCollider.enabled = status;
        Defeated = !status;
    }

    private void OnDisable()
    {
        EventManager.UnregisterListener<BubbleArrivedToNest>(DisableAgentScript);
    }
}
