// Based on the implementation by Jason Weimann: https://youtu.be/lgA8KirhLEU

using UnityEngine;
using UnityEngine.Events;

public class GameEventVectorSubscriber : MonoBehaviour
{
  [SerializeField] GameEventVector gameEvent;
  [SerializeField] UnityEvent<Vector3> unityEvent;

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

  public void Trigger(Vector3 value) => unityEvent?.Invoke(value);
}