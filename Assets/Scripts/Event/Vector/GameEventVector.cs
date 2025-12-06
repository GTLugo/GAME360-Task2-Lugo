using System.Collections.Generic;
using UnityEngine;

namespace Event.Vector {
  [CreateAssetMenu(menuName = "GameEvent/Vector", fileName = "New Game Event")]
  public class GameEventVector : ScriptableObject {
    private readonly HashSet<GameEventVectorSubscriber> _subscribers = new();

    public void Trigger(Vector3 value) {
      foreach (var subscriber in _subscribers) {
        subscriber.Trigger(value);
      }
    }

    public void Subscribe(GameEventVectorSubscriber subscriber) {
      _subscribers.Add(subscriber);
    }

    public void Unsubscribe(GameEventVectorSubscriber subscriber) {
      _subscribers.Remove(subscriber);
    }
  }
}