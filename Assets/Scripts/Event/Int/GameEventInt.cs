using System.Collections.Generic;
using UnityEngine;

namespace Event.Int {
  [CreateAssetMenu(menuName = "GameEvent/Integer", fileName = "New Game Event")]
  public class GameEventInt : ScriptableObject {
    private readonly HashSet<GameEventIntSubscriber> _subscribers = new();

    public void Trigger(int value) {
      foreach (var subscriber in this._subscribers) {
        subscriber.Trigger(value);
      }
    }

    public void Subscribe(GameEventIntSubscriber subscriber) {
      this._subscribers.Add(subscriber);
    }

    public void Unsubscribe(GameEventIntSubscriber subscriber) {
      this._subscribers.Remove(subscriber);
    }
  }
}