// Based on the implementation by Jason Weimann: https://youtu.be/lgA8KirhLEU

using UnityEngine;
using UnityEngine.Events;

namespace Event.Int {
  public class GameEventIntSubscriber : MonoBehaviour {
    [SerializeField] private GameEventInt gameEvent;

    [SerializeField] private UnityEvent<int> unityEvent;

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

    public void Trigger(int value) {
      unityEvent?.Invoke(value);
    }
  }
}