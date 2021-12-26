using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField, Range(2f, 20f)] private float _maxSpeed = 10f;
    [SerializeField] private int _score = 10;

    public float MaxSpeed { get { return _maxSpeed; } }
    public int Score { get { return _score; } }
}
