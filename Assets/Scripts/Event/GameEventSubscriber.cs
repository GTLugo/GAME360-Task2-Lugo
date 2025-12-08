// Based on the implementation by Jason Weimann: https://youtu.be/lgA8KirhLEU

using UnityEngine;
using UnityEngine.Events;

namespace Event {
  public abstract class GameEventSubscriber<T> : MonoBehaviour {
    [SerializeField]
    private GameEvent<T> gameEvent;

    [SerializeField]
    private UnityEvent<T> unityEvent;

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

    public void Trigger(T input) {
      this.unityEvent?.Invoke(input);
    }
  }

  public abstract class GameEventSubscriber : MonoBehaviour {
    [SerializeField]
    private GameEvent gameEvent;

    [SerializeField]
    private UnityEvent unityEvent;

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

    public void Trigger() {
      this.unityEvent?.Invoke();
    }
  }
}