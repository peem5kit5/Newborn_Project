using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EventData")]
public class Event_SO : ScriptableObject
{
    public string EventName;
    public int EventID;
    public EventType eventType;
    public enum EventType
    {
        Relationship,
        Trading,
        Help,
    }

    public bool isActive;
    public bool isFinished;
    public bool isFailed;
    public int RewardGolds;
    

    
}
   