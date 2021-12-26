using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");

    [NonSerialized] public Vector2 inputMovement = default;
    [NonSerialized] public bool isJumpingTriggered = false;
    [NonSerialized] public bool _isGrounded = true;
    [NonSerialized] public bool bounced = false;

    [SerializeField] private float _currentSpeed = 1f;
    [SerializeField] private float _groundDistance = 0.2f;
    [SerializeField] private LayerMask _ground = default;
    [SerializeField] private float _mouseSensitivity = 1;
    
    private bool _hasLanded = true;
    [Header("Sound")]
    [SerializeField] private AudioClip _landAudio;
    [SerializeField] private float _landVolume = 1;
    [SerializeField] private AudioClip _jumpAudio;
    [SerializeField] private float _jumpVolume = 1;

    [Header("Particles")]
    [SerializeField] private ParticleSystem _jumpParticle;
    [SerializeField] private ParticleSystem _landParticle;
    [SerializeField] private Transform _particlePlacement;

    [Header("Controller")]
    [SerializeField] private float _controllerDirectionSensitivity = 10f;
    [NonSerialized] public bool usingController = false;
    [NonSerialized] public Vector2 controllerLookDirection = default;
    private Vector2 lastdirection = default;
    
    [Header("Screen Shake")] 
    [SerializeField] private float _bounceShakeMagnitude = 0.2f;
    [SerializeField] private float _bounceShakeDuration = 0.5f;
    
    private Vector2Control _mousePosition;
    
    private float _startSpeed = 0;
    private Animator _animator;
    private Camera _mainCamera;
    private Ray _ray;
    private float _hitDist;
    private Plane _playerPlane;
    
    private Rigidbody _body;
    private Vector3 _inputs = Vector3.zero;
    private Transform _groundChecker;

    private Coroutine _speedPowerUp;
    
    [SerializeField] private float _jumpHeight = 2f;
    [Range(0.1f, 5f)][SerializeField]private float _fallAfter;
    [Range(20, 200)][SerializeField]private float _fallSpeed;
    
    
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Fall = Animator.StringToHash("Fall");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    private static readonly int Reset = Animator.StringToHash("Reset");


    private void Start()
    {
        _body = GetComponent<Rigidbody>();
        _groundChecker = transform.Find("GroundChecker");
        _animator = GetComponentInChildren<Animator>();
        _inputs = Vector3.zero;
        _mainCamera = GameManagement.GetMainCamera();
        _mousePosition = Mouse.current.position;
        _playerPlane = new Plane(Vector3.up, transform.position);
        _startSpeed = _currentSpeed;
        EventManager.RegisterListener<ActivatePowerUpEventInfo>(ActivateSpeed);
    }
    
    private void ActivateSpeed(EventInfo ei)
    {
        ActivatePowerUpEventInfo apuei = (ActivatePowerUpEventInfo)ei;

        if (apuei.PowerType == PowerupType.Speed)
        {
            if(_speedPowerUp != null)
            {
                StopCoroutine(_speedPowerUp);
            }
            _speedPowerUp = StartCoroutine(SpeedTimer(apuei.SpeedPercentage, apuei.Duration));
        }
    }

    private IEnumerator SpeedTimer(float speed, float duration)
    {
        _currentSpeed = _startSpeed;
        if (speed < 1) speed = 1; // eliminating division by 0
        _currentSpeed += _currentSpeed * (speed / 100);
        yield return new WaitForSeconds(duration);
        if (duration == 0) yield break;
        _currentSpeed = _startSpeed;
    }
    
    private void RotateCharacter()
    {
        if (_mainCamera == null)
        {
            return;
        }

        if (usingController){
            
            if(controllerLookDirection == Vector2.zero){
                if (inputMovement == Vector2.zero){
                    return;
                }
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(new Vector3(inputMovement.x,0,inputMovement.y)),
                    _controllerDirectionSensitivity * Time.deltaTime);
                return;
            }
            
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(new Vector3(controllerLookDirection.x,0,controllerLookDirection.y)),
                _controllerDirectionSensitivity * Time.deltaTime);
        }
        else{
            _ray = _mainCamera.ScreenPointToRay(_mousePosition.ReadValue());
            _playerPlane = new Plane(Vector3.up, transform.position);
            if (_playerPlane.Raycast(_ray, out _hitDist))
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(_ray.GetPoint(_hitDist) - transform.position),
                    _mouseSensitivity * Time.deltaTime);
            } 
        }
    }
    
    private void Animate()
    {
        if (inputMovement.x != 0 || inputMovement.y != 0)
            _animator.SetFloat(Speed, 1);
        else
            _animator.SetFloat(Speed, 0);
    }

    private bool IsIdlePlaying()
    {
        if (_animator.GetCurrentAnimatorStateInfo(2).IsName("IdleState") )
            return true;
        else
            return false;
    }

    void FixedUpdate()
    {
        RotateCharacter();
       
        _inputs.x = inputMovement.x;
        _inputs.z = inputMovement.y;
        
       _body.MovePosition(_body.position + _inputs * (_currentSpeed * Time.fixedDeltaTime));
       _isGrounded = Physics.CheckSphere(_groundChecker.position, _groundDistance, _ground, QueryTriggerInteraction.Ignore);
       Animate();

       if (_isGrounded)
       {
           if(!_hasLanded)
               LandedSound();
           
           _animator.SetBool(IsGrounded, true);
           ResetAnimator();
           bounced = false;
           _hasLanded = true;
       } 
       
        if (isJumpingTriggered && _isGrounded)
        {
            JumpSound();
            
            _animator.SetBool(IsGrounded, false);
            _animator.SetTrigger(Jump);
            Invoke(nameof(FallingDown), _fallAfter);
            isJumpingTriggered = false;
            WeakGravity();
            _body.AddForce(Vector3.up * _jumpHeight, ForceMode.VelocityChange);

            _hasLanded = false;
        }
    }

    private void ResetAnimator()
    {
        if(IsIdlePlaying())
            return;
        _animator.SetTrigger(Reset);
    }
    
    private void FallingDown()
    {
        _animator.SetBool(IsGrounded, false);
        _animator.SetTrigger(Reset);
       _animator.SetTrigger(Fall);
       StrongGravity();
    }

    private void StrongGravity()
    {
        Physics.gravity = new Vector3(0, -_fallSpeed, 0);
    }

    private void WeakGravity()
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
    }


    private void JumpAnimation()
    {
        _animator.SetBool(IsGrounded, false);
        _animator.SetTrigger(Reset);
        _animator.SetTrigger(Jump);
    }
    public void BounceOnBubble(int heightValue, float speedValue)
    {
        if (!_isGrounded)
        {
            ScreenShakeEventInfo ssei = new ScreenShakeEventInfo(_bounceShakeDuration, _bounceShakeMagnitude);
            EventManager.SendNewEvent(ssei);
            WeakGravity();
            JumpAnimation();            
            _body.AddForce(Vector3.up * heightValue * 2, ForceMode.VelocityChange);
            bounced = true;
            Invoke(nameof(FallingDown), _fallAfter);
        }
    }

    private void LandedSound()
    {
        if(!_landAudio)
            return;
        
        AudioEventInfo aei = new AudioEventInfo(_landAudio, _landVolume, transform.position);
        EventManager.SendNewEvent(aei);
        
        if(_landParticle && _particlePlacement)
        {
            ParticleSystem particle = Instantiate(_landParticle);
            particle.transform.position = _particlePlacement.position;
        }
    }

    private void JumpSound()
    {
        if(!_jumpAudio)
            return;
        
        AudioEventInfo aei = new AudioEventInfo(_jumpAudio, _jumpVolume, transform.position);
        EventManager.SendNewEvent(aei);
        
        if(_jumpParticle && _particlePlacement)
        {
            ParticleSystem particle = Instantiate(_jumpParticle);
            particle.transform.position = _particlePlacement.position;
        }
    }
}