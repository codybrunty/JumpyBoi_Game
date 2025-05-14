using System;
using System.Collections.Generic;
using UnityEngine.Events;

public static class EventBus {
    private static Dictionary<string, UnityEvent> eventDictionary = new();

    public static void Subscribe(string eventName, UnityAction listenerFunction) {
        //Check if event exists in dictionary
        if (!eventDictionary.TryGetValue(eventName, out var thisEvent)) {
            //If not add to dictionary
            thisEvent = new UnityEvent();
            eventDictionary[eventName] = thisEvent;
        }
        //Add listener function
        thisEvent.AddListener(listenerFunction);
    }

    public static void Unsubscribe(string eventName, UnityAction listenerFunction) {
        //If event exists in dictionary remove listener function
        if (eventDictionary.TryGetValue(eventName, out var thisEvent)) {
            thisEvent.RemoveListener(listenerFunction);
        }
    }

    public static void Publish(string eventName) {
        //If event exists in dictionary invoke all listener functions in order added
        if (eventDictionary.TryGetValue(eventName, out var thisEvent)) {
            thisEvent.Invoke();
        }
    }
}
