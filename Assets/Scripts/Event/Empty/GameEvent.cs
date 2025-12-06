// Based on the implementation by Jason Weimann: https://youtu.be/lgA8KirhLEU

using System.Collections.Generic;
using UnityEngine;

namespace Event.Empty {
  [CreateAssetMenu(menuName = "GameEvent/Empty", fileName = "New Game Event")]
  public class GameEvent : ScriptableObject {
    private readonly HashSet<GameEventSubscriber> _subscribers = new();

    public void Trigger() {
      foreach (var subscriber in _subscribers) {
        subscriber.Trigger();
      }
    }

    public void Subscribe(GameEventSubscriber subscriber) {
      _subscribers.Add(subscriber);
    }

    public void Unsubscribe(GameEventSubscriber subscriber) {
      _subscribers.Remove(subscriber);
    }
  }
}