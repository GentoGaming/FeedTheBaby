using UnityEngine;

public class BubbleToFeed : MonoBehaviour
{
    [Range(2f, 30f)][SerializeField] private float liftEnemyUp = 20f;
    [Range(0.1f, 1f)][SerializeField] private float moveToNestSpeed = 0.5f;
    // Start is called before the first frame update
    private bool _moveToNest = false;
    private  GameObject _enemyTrapped;
    private float _startDistance, _tParam, _originalNestYAxisPos;
    public EnemyBubbleBehaviour _currentBubbleBehaviour { get; set; } = null;

    // P0 to P4 are the points from the bubble to Nest , bezier curve . 
    private Vector3 _nestPosition, _p0, _p1, _p2, _p3;
        
    void Awake()
    {
        _nestPosition = GameManagement.GetNestPosition();
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_moveToNest)
        {
            // if bubble arrived to nest feed the enemy to baby , Else move the bubble in a curve way to the nest
            if (Vector3.Distance(_nestPosition, _enemyTrapped.transform.position) < 0.1f)
            {
                _moveToNest = false;
                EventManager.SendNewEvent(new BabyFedEventInfo(gameObject, "Feed the baby"));
                EventManager.SendNewEvent(new ReturnEnemyToPoolEventInfo(_enemyTrapped, "returning"));
            }
            else
            {
                _tParam += Time.deltaTime * moveToNestSpeed;
                _enemyTrapped.transform.position = Mathf.Pow(1 - _tParam, 3) * _p0 +
                                                   3 * Mathf.Pow(1 - _tParam, 2) * _tParam * _p1 +
                                                   3 * (1 - _tParam) * Mathf.Pow(_tParam, 2) * _p2 +
                                                   Mathf.Pow(_tParam, 3) * _p3;
            }

        }
    }

    private void DisableEnemyNav()
    {
        EventManager.SendNewEvent(new BubbleArrivedToNest(_enemyTrapped, "bubble on its way to nest"));
        _p0 = _enemyTrapped.transform.position;
          
          _p1 = _p0;
          _p1.y = liftEnemyUp;
          
          _p2 = _nestPosition;
          _p2.y = liftEnemyUp;
          
          _p3 = _nestPosition;
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || _moveToNest) return;
        PlayerController player = other.GetComponent<PlayerController>();
        _tParam = 0;
        _moveToNest = true;
        _enemyTrapped = transform.parent.gameObject;
        _currentBubbleBehaviour.CancelCorutine();
        DisableEnemyNav();
    }
}
