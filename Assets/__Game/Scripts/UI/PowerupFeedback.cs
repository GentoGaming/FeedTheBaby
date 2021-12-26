using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupFeedback : MonoBehaviour
{
    [SerializeField] private PowerupType[] _powerupTypes = new PowerupType[5];
    [SerializeField] private Image[] _uiIcons = new Image[5];

    private List<PowerUpCounter> _powerUpCounters = new List<PowerUpCounter>();

    void Start()
    {
        EventManager.RegisterListener<ActivatePowerUpEventInfo>(ActivatePowerUp);
        EventManager.RegisterListener<UsedPowerupEventInfo>(DeactivatePower);

        for (int i = 0; i < _powerupTypes.Length; i++)
        {
            PowerUpCounter puCounter = new PowerUpCounter();
            puCounter.MyType = _powerupTypes[i];
            puCounter.MyIcon = _uiIcons[i];
            puCounter.MyIcon.enabled = false;
            _powerUpCounters.Add(puCounter);
        }
    }

    void Update()
    {
        foreach(PowerUpCounter pCounter in _powerUpCounters)
        {
            if (pCounter.Active)
                pCounter.UpdateMyBehaviour();
        }
    }

    private void ActivatePowerUp(EventInfo ei)
    {
        ActivatePowerUpEventInfo Apuei = (ActivatePowerUpEventInfo)ei;
        foreach(PowerUpCounter counter in _powerUpCounters)
        {
            if(counter.MyType == Apuei.PowerType)
            {
                counter.AssignPower(Apuei.Duration);
            }
        }
    } 

    public void DeactivatePower(EventInfo ei)
    {
        UsedPowerupEventInfo Epei = (UsedPowerupEventInfo)ei;
        foreach (PowerUpCounter counter in _powerUpCounters)
        {
            if (counter.MyType == Epei.UsedType)
            {
                counter.ManuallyCancel();
            }
        }
    }

    void OnDisable()
    {
        EventManager.UnregisterListener<ActivatePowerUpEventInfo>(ActivatePowerUp);
    }
}

public class PowerUpCounter : MonoBehaviour
{
    public PowerupType MyType { get; set; }
    public bool Active { get; private set; } = false;
    public Image MyIcon { get; set; } = null;

    private float _currentDuration = 0f;
    private float _startDuration = 0f;
    private bool _noDuration = false;
    public void UpdateMyBehaviour()
    {
        if(!_noDuration && _currentDuration > 0)
        {
            _currentDuration -= Time.deltaTime;
            MyIcon.fillAmount = _currentDuration / _startDuration;
            if(_currentDuration <= 0)
            {
                Active = false;
                MyIcon.enabled = false;
            }
        }
    }

    public void AssignPower(float duration)
    {
        _currentDuration = duration;
        _startDuration = duration;

        if(!Active)
        {
            MyIcon.enabled = true;
            if(duration == 0)
            {
                _noDuration = true;
            }
            Active = true;
        }
    }

    public void ManuallyCancel()
    {
        Active = false;
        _noDuration = false;
        MyIcon.enabled = false;
        _currentDuration = 0;
    }
}
