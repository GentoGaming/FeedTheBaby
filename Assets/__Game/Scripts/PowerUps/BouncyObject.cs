using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BouncyObject : MonoBehaviour
{
    [SerializeField, Range(10, 60)] private int _bouncyness = 25;
    [SerializeField] private float _bounceVolume = 1f;
    [SerializeField] private Animator _animator = null;
    [SerializeField] private bool _isTree = false;
    private PlayerController player = null;
    [SerializeField] private GameObject _particleLeaf;
    Projectile projectile = null;
    private static readonly int Bounce = Animator.StringToHash("Bounce");
    private GameObject _go = null;
    private ParticleSystem _particleSystem = null;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) return;
        
        player = GameManagement.GetPlayer().GetComponent<PlayerController>();
        projectile = GetComponent<Projectile>();
    }
    private void KillParticles()
    {
        Destroy(_go);
        _particleSystem = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            projectile?.BubblePop();
            
            if (other.transform.position.y < transform.position.y)
                return;

            if (_isTree)
            {
                if (_particleSystem == null)
                {
                    _go = Instantiate(_particleLeaf, transform.position, Quaternion.identity);
                    _particleSystem = _go.GetComponent<ParticleSystem>();
                    _particleSystem.Play();
                    Invoke(nameof(KillParticles), 10);
                }

                
            }
                            
         
            
            if(_animator) _animator.SetTrigger(Bounce);
            player.BounceOnBubble(_bouncyness, 0f);
            
            AudioEventInfo aei = new AudioEventInfo(SoundManager.Jump, _bounceVolume, transform.position);
            EventManager.SendNewEvent(aei);
        }
    }


}
