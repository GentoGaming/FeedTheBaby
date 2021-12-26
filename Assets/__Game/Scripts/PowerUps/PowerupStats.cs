using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PowerUp")]
public class PowerupStats : ScriptableObject
{
    [SerializeField] private PowerupType _type = default; 
    [SerializeField] private float _duration = 0f;
    [SerializeField] private float _speedIncrease = 0f;

    public PowerupType PowerupType { get { return _type; } }
    public float Duration { get { return _duration; } }
    public float SpeedIncrease { get { return _speedIncrease; } }
}
