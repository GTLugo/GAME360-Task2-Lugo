// Based on the implementation by Jason Weimann: https://youtu.be/lgA8KirhLEU

using UnityEngine;
using UnityEngine.Events;

namespace Event.Empty {
  public class GameEventSubscriber : MonoBehaviour {
    [SerializeField] private GameEvent gameEvent;

    [SerializeField] private UnityEvent unityEvent;

    private void Awake() {
      if (gameEvent) {
        gameEvent.Subscribe(this);
      }
    }

    private void OnDestroy() {
      if (gameEvent) {
        gameEvent.Unsubscribe(this);
      }
    }

    public void Trigger() {
      unityEvent?.Invoke();
    }
  }
}