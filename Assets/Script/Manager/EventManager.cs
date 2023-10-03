using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : GameManager
{
    
    public List<Event_SO> EventList = new List<Event_SO>();

    public int[] DeathRequired;
    public int TriggeredEvents;
    public int[] EventID = { 0, 1, 2, 3 };

    public bool isTriggeringEvent;
    public override void InitAwake()
    {

    }

    public override void InitStart()
    {

    }
    public override void Updating()
    {

        EventsBehaviour();

    }
    void EventsBehaviour()
    {
        if (!isTriggeringEvent)
        {
            if (EventList.Count > 0)
            {
                foreach (Event_SO _event in EventList)
                {
                    CheckEvent(_event.EventID);
                    isTriggeringEvent = true;
                }
            }
        }
    }
    void CheckEvent(int i)
    {
        switch (i)
        {
            case 0:
                // RelationShip Event;
                break;
            case 1:
                // Trade Event;
                break;
            case 2:
                // HelpEvent
                break;

        }
    }

    public void AddEventList(Event_SO _event)
    {
        EventList.Add(_event);
        
    }
    public void RemoveEventList(Event_SO _event)
    {
        if (EventList.Contains(_event))
        {
            EventList.Remove(_event);
        }
    }
    public void ResetEvent()
    {
        isTriggeringEvent = false;
        TriggeredEvents = 0;
    }
}
