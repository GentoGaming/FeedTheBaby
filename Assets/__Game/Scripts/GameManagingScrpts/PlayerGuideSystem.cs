using System.Collections.Generic;
using UnityEngine;

public class PlayerGuideSystem : MonoBehaviour
{
    [SerializeField] private GameObject _guideArrow = null;
    private Dictionary<GameObject, GuideArrow> _arrows = new Dictionary<GameObject, GuideArrow>();

    private void Awake()
    {
        EventManager.RegisterListener<AddingEnemyToGuideEventInfo>(AddingEnemyToList);
        EventManager.RegisterListener<RemovingEnemyFromGuideEventInfo>(RemovingEnemyFromList);
    }

    void AddingEnemyToList(EventInfo ei)
    {
        AssignArrow(ei.GO.transform);
    }

    private void AssignArrow(Transform trans)
    {
        GameObject currentKey = GetArrow();
        _arrows[currentKey].Target = trans;

    }

    private GameObject GetArrow()
    {
        foreach(GameObject entry in _arrows.Keys)
        {
            if (!entry.activeSelf)
            {
                entry.SetActive(true);
                _arrows[entry].enabled = true;
                return entry;
            }
        }

        GameObject newArrow = Instantiate(_guideArrow, transform);
        _arrows.Add(newArrow, newArrow.GetComponent<GuideArrow>());
        return newArrow;
    }

    void RemovingEnemyFromList(EventInfo ei)
    {
        foreach(GameObject entry in _arrows.Keys)
        {
            if(_arrows[entry].Target == ei.GO.transform)
            {
                _arrows[entry].Target = null;
                _arrows[entry].enabled = false;
                entry.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        EventManager.UnregisterListener<AddingEnemyToGuideEventInfo>(AddingEnemyToList);
        EventManager.UnregisterListener<RemovingEnemyFromGuideEventInfo>(RemovingEnemyFromList);
    }
}
