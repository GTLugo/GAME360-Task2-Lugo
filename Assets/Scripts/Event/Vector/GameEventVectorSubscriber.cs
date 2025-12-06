// Based on the implementation by Jason Weimann: https://youtu.be/lgA8KirhLEU

using UnityEngine;
using UnityEngine.Events;

namespace Event.Vector {
  public class GameEventVectorSubscriber : MonoBehaviour {
    [SerializeField] private GameEventVector gameEvent;

    [SerializeField] private UnityEvent<Vector3> unityEvent;

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

    public void Trigger(Vector3 value) {
      unityEvent?.Invoke(value);
    }
  }
}