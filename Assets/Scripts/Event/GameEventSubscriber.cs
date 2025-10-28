// Based on the implementation by Jason Weimann: https://youtu.be/lgA8KirhLEU

using UnityEngine;
using UnityEngine.Events;

public class GameEventSubscriber : MonoBehaviour
{
  [SerializeField] GameEvent gameEvent;
  [SerializeField] UnityEvent unityEvent;


  void Awake()
  {
    if (gameEvent != null)
    {
      gameEvent.Subscribe(this);
    }
  }

  void OnDestroy()
  {
    if (gameEvent != null)
    {
      gameEvent.Unsubscribe(this);
    }
  }

  public void Trigger() => unityEvent?.Invoke();
}