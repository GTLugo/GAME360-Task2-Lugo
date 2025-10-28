using UnityEngine;

class WonState : State
{
  public override void Enter()
  {
    Debug.Log("Player has won");
    player.won.Trigger(player.transform.position);
  }

  public override void Update(PlayerInput input)
  {
    // ...nothing
  }
}