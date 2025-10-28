using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent/Vector", fileName = "New Game Event")]
public class GameEventVector : ScriptableObject
{
  readonly HashSet<GameEventVectorSubscriber> subscribers = new();

  public void Trigger(Vector3 value)
  {
    foreach (var subscriber in subscribers)
    {
      subscriber.Trigger(value);
    }
  }

  public void Subscribe(GameEventVectorSubscriber subscriber) => subscribers.Add(subscriber);
  public void Unsubscribe(GameEventVectorSubscriber subscriber) => subscribers.Remove(subscriber);
}