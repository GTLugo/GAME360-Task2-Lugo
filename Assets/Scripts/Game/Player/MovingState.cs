using UnityEngine;

class MovingState : State
{
  public MovingState(PlayerController player) : base(player)
  {
  }

  public override void Enter()
  {
    Debug.Log("Player is Moving");
  }

  public override void Update(PlayerInput input)
  {
    if (input.direction.magnitude < 0.1f)
    {
      player.state = new IdleState(player);
      return;
    }

    HandleMovement(input.direction);

    if (player.Score >= player.targetScore && player.won != null)
    {
      player.won.Trigger(player.transform.position);
      player.state = new WonState(player);
    }
  }

  // New movement handling based on Brackeys' Third Person Movement tutorial: https://youtu.be/4HpC--2iowE
  void HandleMovement(Vector3 direction)
  {
    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref player.turnSpeed, player.turnTime);
    player.transform.rotation = Quaternion.Euler(0f, angle, 0f);

    player.controller.Move(direction * player.moveSpeed * Time.deltaTime);
  }
}