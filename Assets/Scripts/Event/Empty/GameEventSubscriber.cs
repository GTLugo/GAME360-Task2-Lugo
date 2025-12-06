// Based on the implementation by Jason Weimann: https://youtu.be/lgA8KirhLEU

using UnityEngine;
using UnityEngine.Events;

namespace Event.Empty {
  public class GameEventSubscriber : MonoBehaviour {
    [SerializeField] private GameEvent gameEvent;

    [SerializeField] private UnityEvent unityEvent;

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