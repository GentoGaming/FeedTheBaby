using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[DefaultExecutionOrder(-90)]
public class SoundManager : MonoBehaviour{
    public static SoundManager SoundManagerInstance;
    public static AudioClip EnemyTrappedSound, FireSound, ChonccEatingSound, EnemyFoodSound, BubbleSound, BubblePop,
        PowerUp, ToxicFood, Walking, Jump, Land;

    [SerializeField] private AudioClip _music = null;
    [SerializeField] private float _speedUpTime = 0.01f;
    [SerializeField] private float _maxSpeedUp = 1.5f;
    [SerializeField] private AudioClip _endMusic = null;
    [SerializeField] private float _musicVolume = 0.05f;
    [SerializeField] private AudioMixerGroup _mixer;
    
    [System.Serializable]
    public struct SoundClipStruct
    {
        public AudioClip enemyTrappedSound;
        public AudioClip fireSound;
        public AudioClip chonccEatingSound;
        public AudioClip enemyFoodSound;
        public AudioClip bubbleSound;
        public AudioClip bubblePop;
        public AudioClip powerUp;
        public AudioClip toxicFood;
        public AudioClip walking;
        public AudioClip jump;
        public AudioClip land;
    }

    public SoundClipStruct audioClips;

    [Header("AudioPool")] 
    [SerializeField] private GameObject _soundObject = null;
    [SerializeField] private Transform _soundObjectHolder = null;
    [SerializeField] private int _amount = 0;

    private AudioSource[] _audioSources = default;
    private GameObject[] _objects = default;
    
    private Transform _transform =default;
    private int _counter = 0;
    private AudioSource _musicSource = null;
    private bool _fastMusic = false;
    
    private void Awake()
    {
        if (SoundManagerInstance) Destroy(gameObject);
        else SoundManagerInstance = this;
        
        EventManager.RegisterListener<AudioEventInfo>(PlaySoundEvent);
        
        EnemyTrappedSound = audioClips.enemyTrappedSound;
        FireSound = audioClips.fireSound;
        ChonccEatingSound = audioClips.chonccEatingSound;
        EnemyFoodSound = audioClips.enemyFoodSound;
        BubbleSound = audioClips.bubbleSound;
        BubblePop = audioClips.bubblePop;
        PowerUp = audioClips.powerUp;
        ToxicFood = audioClips.toxicFood;
        Walking = audioClips.walking;
        Jump = audioClips.jump;
        Land = audioClips.land;

        _objects = new GameObject[_amount];
        _audioSources = new AudioSource[_amount];
        for (int i = 0; i < _amount; i++)
        {
            _objects[i] = Instantiate(_soundObject, _soundObjectHolder);
            _audioSources[i] = _objects[i].GetComponent<AudioSource>();
        }

        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.volume = _musicVolume;
        _musicSource.loop = true;
        _musicSource.outputAudioMixerGroup = _mixer;
        _musicSource.clip = _music;
        _musicSource.Play();
    }

    private void OnDisable()
    {
        EventManager.UnregisterListener<AudioEventInfo>(PlaySoundEvent);
    }

    public void FastMusic()
    {
        if (_fastMusic || _musicSource.clip == _endMusic)
        {
            return;
        }

        StartCoroutine(RampUpMusic());
    }

    private IEnumerator RampUpMusic()
    {
        while (_musicSource.pitch < _maxSpeedUp)
        {
            if(_musicSource.clip == _endMusic) yield break;
            _musicSource.pitch += Time.deltaTime * _speedUpTime;
            yield return new WaitForEndOfFrame();
        }
    }
    
    public void EndMusic()
    {
        print("Hello");
        if (_musicSource.clip == _endMusic)
        {
            return;
        }
        
        _musicSource.Stop();
        _musicSource.pitch = 1f;
        _musicSource.clip = _endMusic;
        _musicSource.loop = false;
        _musicSource.Play();
    }
    
    private void PlaySoundEvent(EventInfo ei)
    {
        var eventInfo = (AudioEventInfo)ei;
        PlaySound(eventInfo.Clip, eventInfo.Volume, eventInfo.Position);
    }
    
    public void PlaySound(AudioClip clip, float volume, Vector3 position)
    {
        if(clip == null)
            return;
        
        _objects[_counter].transform.position = position;
        _audioSources[_counter].clip = clip;
        _audioSources[_counter].volume = volume;
        _audioSources[_counter].Play();

        _counter++;
        if (_counter > _amount - 1) _counter = 0;
    }
}