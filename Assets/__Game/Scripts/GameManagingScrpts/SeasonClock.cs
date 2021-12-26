using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SeasonHolder")]
public class SeasonClock : ScriptableObject
{
    [SerializeField] private float _timeBetweenSeasons = 30f;

    [field: SerializeField] private SeasonObject[] _seasonsInOrder = new SeasonObject[0];
    //[field: SerializeField] private ScriptableObject _season1 = null;
    //[field: SerializeField] private SeasonObject _season2 = null;
    //[field: SerializeField] private SeasonObject _season3 = null;

    public float TimeInBetween { get { return _timeBetweenSeasons; } }
    public SeasonObject[] SeasonsOrder { get { return _seasonsInOrder; } }
}