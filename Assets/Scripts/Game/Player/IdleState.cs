using UnityEngine;

class IdleState : State
{
  public IdleState(PlayerController player) : base(player)
  {
  }

  public override void Enter()
  {
    Debug.Log("Player is Idle");
  }

  public override void Update(PlayerInput input)
  {
    if (input.direction.magnitude >= 0.1f)
    {
      player.state = new MovingState(player);
    }

    if (player.Score >= player.targetScore && player.won != null)
    {
      player.state = new WonState(player);
    }
  }
}