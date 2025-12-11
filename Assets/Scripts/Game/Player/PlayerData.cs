using UnityEngine;

namespace Game.Player {
  public class PlayerData : MonoBehaviour {
    [Header("Movement")]
    [field: SerializeField]
    public float MoveSpeed { get; set; } = 5f;

    public bool isAffectedByGravity = true;
    public float gravityVelocity;
    public Vector3 moveVelocity;

    public float stopDistance = 0.1f;
    public float deadZone = 0.01f;
    public float turnSpeed = 15.0f;
    public float turnTime = 0.1f;

    [Header("Stats")]
    [field: SerializeField]
    public int Health { get; set; } = 100;

    [field: SerializeField]
    public int MaxHealth { get; set; } = 100;

    [field: SerializeField]
    public bool HasWon { get; set; }

    [field: SerializeField]
    public bool IsAlive { get; set; } = true;

    [field: SerializeField]
    public float RespawnTimeSecs { get; set; } = 3.0f;

    [field: SerializeField]
    public Vector3 RespawnPosition { get; set; } = new(0.0f, 0.0f, 0.0f);

    public int Score { get; set; }
    public State State { get; set; }
  }
}