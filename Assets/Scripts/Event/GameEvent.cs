// Based on the implementation by Jason Weimann: https://youtu.be/lgA8KirhLEU

using System.Collections.Generic;
using UnityEngine;

namespace Event {
  public abstract class GameEvent<T> : ScriptableObject {
    private readonly HashSet<GameEventSubscriber<T>> _subscribers = new();

    public void Trigger(T input) {
      foreach (var subscriber in this._subscribers) {
        subscriber.Trigger(input);
      }
    }

    public void Subscribe(GameEventSubscriber<T> subscriber) {
      this._subscribers.Add(subscriber);
    }

    public void Unsubscribe(GameEventSubscriber<T> subscriber) {
      this._subscribers.Remove(subscriber);
    }
  }

  [CreateAssetMenu(menuName = "GameEvent/Void", fileName = "New Game Event")]
  public class GameEvent : ScriptableObject {
    private readonly HashSet<GameEventSubscriber> _subscribers = new();

    public void Trigger() {
      foreach (var subscriber in this._subscribers) {
        subscriber.Trigger();
      }
    }

    public void Subscribe(GameEventSubscriber subscriber) {
      this._subscribers.Add(subscriber);
    }

    public void Unsubscribe(GameEventSubscriber subscriber) {
      this._subscribers.Remove(subscriber);
    }
  }
}