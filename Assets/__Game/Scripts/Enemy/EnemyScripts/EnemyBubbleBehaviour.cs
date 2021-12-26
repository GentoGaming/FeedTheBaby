using System.Collections;
using UnityEngine;

public class EnemyBubbleBehaviour : MonoBehaviour, IBubbleTarget
{
    [SerializeField] private float _TrappedTime = 10f;

    private Coroutine _trappedCorutine;
    private EnemyBehaviour _enemyBehaviour = null;
    private GrossEnemyBehaviour _grossEnemy = null;
    private EnemyStealFruit _stealFruit = null;
    private InvisibleEnemy _invisible = null;
    private BubbleToFeed _activeBubble = null;

    void Awake()
    {
        _enemyBehaviour = GetComponent<EnemyBehaviour>();
        _grossEnemy = GetComponent<GrossEnemyBehaviour>();
        _stealFruit = GetComponent<EnemyStealFruit>();
        _invisible = GetComponent<InvisibleEnemy>();
    }

    public void TargetByBubble()
    {
        if (_enemyBehaviour != null && _enemyBehaviour.Bubble == null)
        {
            if (_enemyBehaviour.FruitKidnapped)
            {
                _stealFruit.DropFruit();
            }
            _enemyBehaviour?.ChangeAgentStatus(false);
            _trappedCorutine = StartCoroutine(TrappedByBubble());
        }
        else if (_grossEnemy?.Bubble == null)
        {
            _grossEnemy?.ChangeAgentStatus(false);
            _trappedCorutine = StartCoroutine(TrappedByBubble());
        }
    }

    public void AssignNewBubble(GameObject bubble)
    {
        _enemyBehaviour?.AssignBubble(bubble);
        _grossEnemy?.AssignBubble(bubble);
        if(bubble != null)
        {
            _activeBubble = bubble.GetComponent<BubbleToFeed>();
            _activeBubble._currentBubbleBehaviour = this;
        }
    }

    public void CancelCorutine()
    {
        StopCoroutine(_trappedCorutine);
    }

    public IEnumerator TrappedByBubble()
    {
        float currentTimer = 0;
        _invisible?.NoLongerStealth();
        TrapEnemyEventInfo Teei = new TrapEnemyEventInfo(this, gameObject, "");
        EventManager.SendNewEvent(Teei);
        while (currentTimer < _TrappedTime)
        {
            currentTimer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        GameObject bubble = _enemyBehaviour != null ? _enemyBehaviour.Bubble : _grossEnemy.Bubble;
        ReturnBubbleEventInfo Rbei = new ReturnBubbleEventInfo(bubble, "");
        EventManager.SendNewEvent(Rbei);

        _enemyBehaviour?.ChangeAgentStatus(true);
        _enemyBehaviour?.AssignBubble(null);

        _grossEnemy?.ChangeAgentStatus(true);
        _grossEnemy?.AssignBubble(null);
    }
}