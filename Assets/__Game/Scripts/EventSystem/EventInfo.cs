using UnityEngine;

public class EventInfo
{
    public GameObject GO { get; private set; }
    public string Discription { get; private set; }

    public EventInfo(GameObject senderObject = null, string discription = "")
    {
        GO = senderObject;
        Discription = discription;
    }
}

public class BabyScreamEventInfo : EventInfo
{
    public BabyScreamEventInfo(GameObject go = null, string discription = "") : base(go, discription)
    {
    }
}

public class AddingEnemyToGuideEventInfo : EventInfo
{
    public AddingEnemyToGuideEventInfo(GameObject gO) : base(gO)
    {
    }
}

public class RemovingEnemyFromGuideEventInfo : EventInfo
{
    public RemovingEnemyFromGuideEventInfo(GameObject gO) : base(gO)
    {
    }
}

public class BabyReachedEventInfo : EventInfo
{ 
    public BabyReachedEventInfo(GameObject gO, string discription): base(gO, discription)
    {
    }
}

public class BabyFedEventInfo : EventInfo
{
    public BabyFedEventInfo(GameObject gO, string discription) : base(gO, discription)
    {
    }
}

public class UpdateEnemyMinDistanceEventInfo: EventInfo
{
    public float NewMinDistance { get; private set; }
    public UpdateEnemyMinDistanceEventInfo(float newMinDistance, GameObject gO, string discription) : base(gO, discription)
    {
        NewMinDistance = newMinDistance;
    }
}

public class TrapEnemyEventInfo : EventInfo
{
    public EnemyBubbleBehaviour TrappedTarget { get; private set; }
    public TrapEnemyEventInfo(EnemyBubbleBehaviour trappedTarget, GameObject gO, string discription) : base(gO, discription)
    {
        TrappedTarget = trappedTarget;
    }
}

public class ReturnBubbleEventInfo : EventInfo
{
    public ReturnBubbleEventInfo(GameObject gO, string discription) : base(gO, discription)
    {
    }
}

public class BubbleArrivedToNest:EventInfo 
{
    public BubbleArrivedToNest(GameObject gO, string discription) : base(gO, discription)
    {
    }
}


public class ReturnEnemyToPoolEventInfo : EventInfo
{
    public ReturnEnemyToPoolEventInfo(GameObject gO, string discription) : base(gO, discription)
    {
    }
}

public class ProjectileEventInfo : EventInfo
{
    public int ID{ get; private set; }
    public Transform Parent{ get; private set; }
    public ProjectileEventInfo(int id, Transform parent, GameObject senderObject, string discription) : base(senderObject, discription){
        ID = id;
        Parent = parent;
    }
}

public class AssignFruitEventInfo : EventInfo
{
    public AssignFruitEventInfo(GameObject gO, string discription): base(gO, discription)
    {
    }
}

public class HighScoreEventInfo : EventInfo
{
    public int Score{ get; private set; }
    public string Name{ get; private set; }
    public HighScoreEventInfo(int score, string scoreName, GameObject senderObject = null, string discription = "") : base(senderObject,
        discription){
        Score = score;
        Name = scoreName;
    }
}

public class AssignPlayerCameraEventInfo : EventInfo
{
    public Camera PlayerCam { get; private set; }
    public AssignPlayerCameraEventInfo(Camera cam, GameObject gO = null, string discription = "") : base(gO, discription)
    {
        PlayerCam = cam;
    }
}

public class PauseEventInfo : EventInfo
{
    public PauseEventInfo(GameObject senderObject = null, string discription = "") : base(senderObject, discription)
    {
    }
}

public enum PowerupType{BigBubble = 0, RapidFire = 1, MultiShot = 2, Speed = 3, Explosion = 4, Scream = 5}

public class ActivatePowerUpEventInfo : EventInfo
{
    public PowerupType PowerType {get; private set; }
    public float Duration;
    public float SpeedPercentage;

    public ActivatePowerUpEventInfo(PowerupType powerType, float duration, float speedPercentage = 0, GameObject senderObject = null, string discription = "") : base(senderObject,
        discription)
    {
        PowerType = powerType;
        Duration = duration;
        SpeedPercentage = speedPercentage;
    }
}

public class EndGameEventInfo : EventInfo
{
    public EndGameEventInfo(GameObject senderObject = null, string discription = "") : base(senderObject, discription)
    {
    }
}

public class AudioEventInfo : EventInfo
{
    public AudioClip Clip { get; private set; }
    public float Volume{ get; private set; }
    public Vector3 Position{ get; private set; }
    
    public AudioEventInfo(AudioClip clip, float volume, Vector3 position, GameObject senderObject = null, string discription = "") :
        base(senderObject, discription)
    {
        Clip = clip;
        Volume = volume;
        Position = position;
    }
}

public class ScreenShakeEventInfo : EventInfo{
    public float Duration{ get; private set; }
    public float Magnitude{ get; private set; }
    
    public ScreenShakeEventInfo(float duration, float magnitude, GameObject senderObject = null, string discription = "") : base(senderObject,
        discription)
    {
        Duration = duration;
        Magnitude = magnitude;
    }
}