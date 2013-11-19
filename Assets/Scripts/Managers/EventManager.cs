using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager : IService
{
	public delegate void TriggeredEvent(IEventArgs args);
	
	private Dictionary<string, List<TriggeredEvent>> _managedEvents = new Dictionary<string, List<TriggeredEvent>>();
	
	public void Register(string eventName)
	{
		if (!_managedEvents.ContainsKey(eventName))
		{
			_managedEvents.Add(eventName, new List<TriggeredEvent>());
		}
		else
		{
			Debug.LogWarning("Attempting to register an event that's already registered: " + eventName);	
		}
	}
	
	public void UnRegister(string eventName)
	{
		if (_managedEvents.ContainsKey(eventName))
		{
			_managedEvents.Remove(eventName);	
		}
		else
		{
			Debug.LogWarning("Attempting to unregister an event that is not registered: " + eventName);	
		}
	}
	
	public void Subscribe(string eventName, TriggeredEvent trigger)
	{
		if (_managedEvents.ContainsKey(eventName))
		{
			_managedEvents[eventName].Add(trigger);	
		}
		else
		{
			Register(eventName);
			Subscribe(eventName, trigger);
			//Debug.LogWarning ("Attempting to subscribe to an event that is not registered: " + eventName);	
		}
	}
	
	public void UnSubscribe(string eventName, TriggeredEvent trigger)
	{
		if (_managedEvents.ContainsKey(eventName))
		{
			List<TriggeredEvent> events = _managedEvents[eventName];
			for (int i = 0; i < events.Count; i++) 
			{
				if (events[i].Target == trigger.Target)
				{
					events.RemoveAt(i);
					break;
				}
			}
		}
		else
		{
			Debug.LogWarning ("Attempting to unsubscribe to an event that is not registered: " + eventName);	
		}
	}
	
    public void FireEvent(string eventName, IEventArgs args)
    {
        if (_managedEvents.ContainsKey(eventName))
        {
            foreach (TriggeredEvent trigger in _managedEvents[eventName])
            {
                trigger.Invoke(args);
            }
        }
        else
        {
            Debug.LogWarning ("Attempting to fire an event that is not registered: " + eventName);	
        }
    }
	
    public ServiceType GetServiceType()
    {
        return ServiceType.EventManager;
    }
}

public interface IEventArgs
{
	
}
