using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour{
    [Header("Camera Option")] 
    [SerializeField] private float smoothening = 3.0f;

    [Header("Zoom In Attributes")]
    [SerializeField] private float zoomInTimer = 3.0f;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;

    private Transform _playerTrans = null;
    private Transform _camTrans = null;
    private Vector3 _offset = Vector3.zero;
    private float _time = 0;
    private bool zooming = true;
    
    void Start()
    {
        Application.targetFrameRate = 120;
        _playerTrans = GameManagement.GetPlayer().transform;
        _camTrans = GameManagement.GetMainCamera().transform;
        _offset = _camTrans.position - _playerTrans.position;

        StartCoroutine(CameraZoomIn());
    }

    private void Update(){
        if (zooming)
            return;
        _camTrans.position = Vector3.Lerp(_camTrans.position, _playerTrans.position + _offset, 2);
        _time += Time.deltaTime;
    }

    private IEnumerator CameraZoomIn()
    {
        float waitTime = 1f/120f;

        if(startPosition == null || endPosition == null)
        {
            Debug.Log("You are missing start or end position for camera zoom");
        }
        else
        {
            float timer = 0.0f;
            while (timer < zoomInTimer)
            {
                Time.timeScale = 0.0f;
                timer += waitTime;

                _camTrans.position = Vector3.Lerp(startPosition.position, endPosition.position, timer / zoomInTimer);
                _camTrans.rotation = Quaternion.Slerp(startPosition.rotation, endPosition.rotation, timer / zoomInTimer);
                yield return new WaitForEndOfFrame();
            }
            Time.timeScale = 1.0f;
            zooming = false;
        }
 
    }
}