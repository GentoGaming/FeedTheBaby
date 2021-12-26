using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonIndicator : MonoBehaviour
{
    [SerializeField] private Image[] _uiFruitIcon = new Image[4];

    private Dictionary<FruitType, Image> _seasons = new Dictionary<FruitType, Image>();
    void Awake()
    {
        int i = 0;
        foreach(Image image in _uiFruitIcon)
        {
            _seasons.Add((FruitType)i, image);
            i++;
        }
        EventManager.RegisterListener<ChangeSeasonEventInfo>(UpdateSeasons);
    }

    void UpdateSeasons(EventInfo ei)
    {
        ChangeSeasonEventInfo Csei = (ChangeSeasonEventInfo)ei;
        foreach(FruitType type in _seasons.Keys)
        {
            _seasons[type].enabled = false;
            foreach(FruitType newType in Csei.ActiveSeason)
            {
                if(type == newType)
                {
                    _seasons[type].enabled = true;
                }
            }
        }
    }

    void OnDisable()
    {
        EventManager.UnregisterListener<ChangeSeasonEventInfo>(UpdateSeasons);
    }
}
