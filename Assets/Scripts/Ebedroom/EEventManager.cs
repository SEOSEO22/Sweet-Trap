using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    private string currentActiveEvent = null;
    private Dictionary<string, bool> events = new Dictionary<string, bool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            events = new Dictionary<string, bool>(); 
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public bool IsAnyEventActive()
    {
        foreach (var pair in events)
        {
            if (pair.Value) return true;
        }
        return false;
    }
    public bool StartEvent(string eventName)
    {
        if (!events.ContainsKey(eventName) || !events[eventName])
        {
            events[eventName] = true;
            currentActiveEvent = eventName;
            return true;
        }
        return false;
    }

    public void EndEvent(string eventName)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] = false;
            currentActiveEvent = null;
        }
    }

    public bool IsEventActive(string eventName)
    {
        return currentActiveEvent == eventName;
    }
}
