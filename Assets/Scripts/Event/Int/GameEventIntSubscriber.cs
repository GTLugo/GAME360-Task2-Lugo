// Based on the implementation by Jason Weimann: https://youtu.be/lgA8KirhLEU

using UnityEngine;
using UnityEngine.Events;

namespace Event.Int {
  public class GameEventIntSubscriber : MonoBehaviour {
    [SerializeField] private GameEventInt gameEvent;

    [SerializeField] private UnityEvent<int> unityEvent;

    private void Awake() {
      if (this.gameEvent) {
        this.gameEvent.Subscribe(this);
      }
    }

    private void OnDestroy() {
      if (this.gameEvent) {
        this.gameEvent.Unsubscribe(this);
      }
    }

    public void Trigger(int value) {
      this.unityEvent?.Invoke(value);
    }
  }
}