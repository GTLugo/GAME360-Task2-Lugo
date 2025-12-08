using UnityEngine;

namespace Game.Player {
  public class PlayerData : MonoBehaviour {
    [field: SerializeField]
    public int TargetScore { get; set; } = 200;

    [field: SerializeField]
    public float MoveSpeed { get; set; } = 5f;

    [field: SerializeField]
    public float turnTime = 0.1f;

    public int Score { get; set; }

    public State State { get; set; }
  }
}