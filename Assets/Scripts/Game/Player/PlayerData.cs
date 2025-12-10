using UnityEngine;

namespace Game.Player {
  public class PlayerData : MonoBehaviour {
    [Header("Movement")]
    [field: SerializeField]
    public float MoveSpeed { get; set; } = 5f;

    public float stopDistance = 0.1f;
    public float deadZone = 0.01f;
    public float turnSpeed = 15.0f;
    public float turnTime = 0.1f;

    [Header("Score")]
    [field: SerializeField]
    public int TargetScore { get; set; } = 200;

    public int Score { get; set; }
    public State State { get; set; }
  }
}