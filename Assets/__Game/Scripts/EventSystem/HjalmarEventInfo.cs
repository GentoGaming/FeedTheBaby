using UnityEngine;

public class HjalmarEventInfo : MonoBehaviour
{}

public class KidnappedFruitEventInfo : EventInfo{
    public KidnappedFruitEventInfo(GameObject gO = null, string discription = "") : base(gO, discription)
    {
    }
}


public class FailedKidnappingFruitEventInfo : EventInfo
{
    public FailedKidnappingFruitEventInfo(GameObject gO, string discription) : base(gO, discription)
    {
    }
}

public class IncreasePlayerScoreEventInfo : EventInfo
{
    public int Score { get; private set; }
    public IncreasePlayerScoreEventInfo(int score, GameObject gO = null, string fiffel = ""): base(gO, fiffel)
    {
        Score = score;
    }
}

public class ChangeSeasonEventInfo : EventInfo
{
    public FruitType[] ActiveSeason { get; private set; }
    public ChangeSeasonEventInfo(FruitType[] activeSeason) : base()
    {
        ActiveSeason = activeSeason;
    }
}

public class ReturnGrossEnemyEventInfo : EventInfo
{
    public GrossEnemyBehaviour EnemyBehaviour { get; private set; }
    public ReturnGrossEnemyEventInfo(GrossEnemyBehaviour enemyBehaviour, GameObject gO, string disctiption) : base(gO, disctiption)
    {
        EnemyBehaviour = enemyBehaviour;
    }
} 

public class UsedPowerupEventInfo : EventInfo
{
    public PowerupType UsedType { get; private set; }
    public UsedPowerupEventInfo(PowerupType type) : base()
    {
        UsedType = type;
    }
}