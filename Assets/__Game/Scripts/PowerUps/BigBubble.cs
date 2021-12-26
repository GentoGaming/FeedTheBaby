using System;
using System.Collections.Generic;
using UnityEngine;

public class BigBubble : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _maxFlyDistance = 20f;
    [SerializeField] private LayerMask _hitLayer = default;
    RaycastHit[] _hits;
    SphereCollider _collider = null;

    List<GameObject> _hitEnemies = new List<GameObject>();
    float _currentDistance = 0f;

    void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        float distance = _speed * Time.deltaTime;
        _hits = Physics.SphereCastAll(transform.position, _collider.radius, transform.forward, distance, _hitLayer);
        CheckHits();
        transform.position += transform.forward * distance;
        _currentDistance += distance;

        if (_currentDistance >= _maxFlyDistance)
        {
            KillBubble();
        }
    }

    private void CheckHits()
    {
        foreach(RaycastHit hit in _hits)
        {
            IBubbleTarget target = hit.collider.GetComponent<IBubbleTarget>();
            if (target != null)
            {
                target.TargetByBubble();
                _hitEnemies.Add(hit.collider.gameObject);
                hit.transform.SetParent(transform);
                hit.transform.localPosition = Vector3.zero;
            }
            else if (hit.collider.CompareTag("Environment"))
            {
                KillBubble();
            }
        }
    }

    private void KillBubble()
    {
        foreach (GameObject obj in _hitEnemies)
        {
            obj.transform.SetParent(null);
        }
        _currentDistance = 0f;
        _hitEnemies.Clear();
        gameObject.SetActive(false);
    }
}
