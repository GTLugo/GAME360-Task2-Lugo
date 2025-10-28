using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Integer", fileName = "New Game Event")]
public class GameEventInt : ScriptableObject
{
  readonly HashSet<GameEventIntSubscriber> subscribers = new();

  public void Trigger(int value)
  {
    foreach (var subscriber in subscribers)
    {
      subscriber.Trigger(value);
    }
  }

  public void Subscribe(GameEventIntSubscriber subscriber) => subscribers.Add(subscriber);
  public void Unsubscribe(GameEventIntSubscriber subscriber) => subscribers.Remove(subscriber);
}