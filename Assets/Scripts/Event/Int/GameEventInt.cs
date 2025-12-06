using System.Collections.Generic;
using UnityEngine;

namespace Event.Int {
  [CreateAssetMenu(menuName = "GameEvent/Integer", fileName = "New Game Event")]
  public class GameEventInt : ScriptableObject {
    private readonly HashSet<GameEventIntSubscriber> _subscribers = new();

    public void Trigger(int value) {
      foreach (var subscriber in _subscribers) {
        subscriber.Trigger(value);
      }
    }

    public void Subscribe(GameEventIntSubscriber subscriber) {
      _subscribers.Add(subscriber);
    }

    public void Unsubscribe(GameEventIntSubscriber subscriber) {
      _subscribers.Remove(subscriber);
    }
  }
}