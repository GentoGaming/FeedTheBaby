using System.Collections.Generic;
using UnityEngine;

public class BubbleDispenser : MonoBehaviour
{
    [SerializeField] private GameObject _bubblePrefab = null;
    private List<GameObject> _usedBubbles = new List<GameObject>();

    private void OnEnable()
    {
        EventManager.RegisterListener<TrapEnemyEventInfo>(DistributeBubble);
        EventManager.RegisterListener<ReturnBubbleEventInfo>(ReturnBubble);
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject newBubble = Instantiate(_bubblePrefab);
            newBubble.SetActive(false);
            _usedBubbles.Add(newBubble);
        }
    }

    private void DistributeBubble(EventInfo ei)
    {
        TrapEnemyEventInfo Teei = (TrapEnemyEventInfo)ei;
        if (_usedBubbles.Count > 0)
        {
            _usedBubbles[0].transform.SetParent(Teei.GO.transform);
            _usedBubbles[0].SetActive(true);
            Teei.TrappedTarget.AssignNewBubble(_usedBubbles[0]);
            _usedBubbles[0].transform.localPosition = Vector3.up;
            _usedBubbles.RemoveAt(0);
        }
        else
        {
            GameObject _newBubble = Instantiate(_bubblePrefab, Teei.GO.transform);
            _newBubble.SetActive(true);
            _newBubble.transform.localPosition = Vector3.up;
            Teei.TrappedTarget.AssignNewBubble(_newBubble);
        }
    }

    private void ReturnBubble(EventInfo ei)
    {
        if(ei.GO != null)
        {
            ei.GO.SetActive(false);
            _usedBubbles.Add(ei.GO);
        }
    }

    private void OnDisable()
    {
        EventManager.UnregisterListener<TrapEnemyEventInfo>(DistributeBubble);
        EventManager.UnregisterListener<ReturnBubbleEventInfo>(ReturnBubble);
    }
}
