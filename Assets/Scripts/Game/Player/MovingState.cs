using UnityEngine;

namespace Game.Player {
  internal class MovingState : State {
    public override void Enter() {
      Logger.Log("Player is Moving");
    }

    public override void Update(PlayerInput input) {
      if (input.direction.magnitude < 0.1f) {
        player.Transition<IdleState>();
        return;
      }

      if (player.Score >= player.targetScore) {
        player.Transition<WonState>();
        return;
      }

      HandleMovement(input.direction);
    }

    // New movement handling based on Brackeys' Third Person Movement tutorial: https://youtu.be/4HpC--2iowE
    private void HandleMovement(Vector3 direction) {
      var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
      var angle = Mathf.SmoothDampAngle(
        player.transform.eulerAngles.y,
        targetAngle,
        ref player.turnSpeed,
        player.turnTime
      );
      player.transform.rotation = Quaternion.Euler(0f, angle, 0f);

      player.controller.Move(direction * (player.moveSpeed * Time.deltaTime));
    }
  }
}