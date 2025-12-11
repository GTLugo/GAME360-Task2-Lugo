using System;
using UnityEngine;

namespace Managers.Global {
  // public enum GameEvent {
  //   PlayerDied,
  //   PlayerWon,
  //   CoinCollected,
  //   ScoreChanged,
  // }

  public struct GameEvent {
    private event Action Action;

    public void Subscribe(Action obj) {
      this.Action += obj;
    }

    public void Unsubscribe(Action obj) {
      this.Action -= obj;
    }

    public void Trigger() {
      this.Action?.Invoke();
    }
  }

  public class GameEvent<T> {
    private event Action<T> Action = delegate { };

    public void Subscribe(Action<T> obj) {
      this.Action += obj;
    }

    public void Unsubscribe(Action<T> obj) {
      this.Action -= obj;
    }

    public void Trigger(T input) {
      this.Action?.Invoke(input);
    }
  }

  public class EventManager : Singleton<EventManager> {
    public static readonly GameEvent<(int, Vector3)> buttonPushed = new();
    public static readonly GameEvent<Vector3> coinCollected = new();
    public static readonly GameEvent<Vector3> playerDied = new();
    public static readonly GameEvent<(int, int)> playerHurt = new();
    public static readonly GameEvent<Vector3> playerRespawned = new();
    public static readonly GameEvent<Vector3> playerWon = new();
    public static readonly GameEvent<int> scoreChanged = new();
  }
}