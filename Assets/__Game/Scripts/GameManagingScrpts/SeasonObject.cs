using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Seasons")]
public class SeasonObject : ScriptableObject
{

    [field: SerializeField] private FruitType[] _activeSeasons = new FruitType[1];

    public FruitType[] ActiveSeasons { get { return _activeSeasons; } }
}
