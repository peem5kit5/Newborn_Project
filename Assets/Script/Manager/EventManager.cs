using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    private Dictionary<string, Event> eventsHolder;

    public Event[] EventArray;
    public Event CurrentEvent;

    public override void Awake()
    {
        base.Awake();
        eventsHolder = EventDict();
    }

    public void InitEvent(EventCondition _eventCondition)
    {
        if (CurrentEvent == null || CurrentEvent.IsHappenning == false)
        {
            int _random = Random.Range(0, eventsHolder.Count);
            GetEventByName(EventArray[_random].EventName);
            CurrentEvent.Condition = SetEventCondition(_eventCondition);
            CurrentEvent.Update();
        }
        else
            Debug.Log("There already have Event Happening right now.");
    }

    public void UpdateEvent()
    {
        if (CurrentEvent != null)
            CurrentEvent.Update();
        else
            Debug.LogError("There no Event Assigned.");
    }

    public EventCondition SetEventCondition(EventCondition _eventCondition)
    {
        return _eventCondition;
    }

    public void GetEventByName(string _name)
    {
        if (eventsHolder.TryGetValue(_name, out var _event))
            CurrentEvent = _event;
        else
            Debug.LogError("This Event is missing in array.");
    }

    private Dictionary<string, Event> EventDict()
    {
        Dictionary<string, Event> _dict = new Dictionary<string, Event>();
        if (EventArray.Length > 0)
        {
            foreach (Event _event in EventArray)
                _dict.Add(_event.EventName, _event);
        }

        return _dict;
    }
}

[CreateAssetMenu(menuName = "Data/EventData")]
public class Event : ScriptableObject
{
    public string EventName;
    [TextArea]
    public string Description;
    public bool IsHappenning;
    public EventCondition Condition;
    public void Update() => IsHappenning = Condition.Checking();
}

public abstract class EventCondition
{
    public abstract bool Checking();
}
