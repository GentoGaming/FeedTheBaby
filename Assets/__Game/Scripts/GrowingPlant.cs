using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingPlant : MonoBehaviour
{
    //will prob not 
    [SerializeField] private GameObject _scaleObj = null;
    [SerializeField] private float _growTime = 3f;
    [SerializeField] private float _maxScale = 1f;

    private bool _grown = false;
    private float _currentScale = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (!_grown)
        {
            _currentScale += Time.deltaTime / _growTime;
            _scaleObj.transform.localScale = new Vector3(_currentScale, _currentScale, _currentScale);
            _scaleObj.transform.localPosition = new Vector3(0,_currentScale * 0.5f,0);
            if (_currentScale >= _maxScale)
            {
                _grown = true;
            }
        }
    }
}
