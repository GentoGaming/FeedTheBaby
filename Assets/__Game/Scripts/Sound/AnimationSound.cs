using UnityEngine;

public class AnimationSound : MonoBehaviour{
    [SerializeField] private AudioClip _audioClip = default;
    [SerializeField] private float _volume = 1;
    [SerializeField] private PlayerController _controller;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void PlaySound()
    {
        if(!_controller._isGrounded) return;
        
        AudioEventInfo aei = new AudioEventInfo(_audioClip, _volume, _transform.position);
        EventManager.SendNewEvent(aei);
    }
}
