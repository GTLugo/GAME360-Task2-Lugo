// Based on the implementation by Jason Weimann: https://youtu.be/lgA8KirhLEU

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Empty", fileName = "New Game Event")]
public class GameEvent : ScriptableObject
{
  readonly HashSet<GameEventSubscriber> subscribers = new();

  public void Trigger()
  {
    foreach (var subscriber in subscribers)
    {
      subscriber.Trigger();
    }
  }

  public void Subscribe(GameEventSubscriber subscriber) => subscribers.Add(subscriber);
  public void Unsubscribe(GameEventSubscriber subscriber) => subscribers.Remove(subscriber);
}