using UnityEngine;

namespace Game.Player {
  internal class WalkState : State {
    public override void Enter() {
      Logger.Log($"Player `{this.Player.name}` is Walking");
    }

    public override void Update(PlayerInput input) {
      if (input.direction.magnitude < 0.1f) {
        this.Transition<IdleState>();
        return;
      }

      if (this.Player.Score >= this.Player.targetScore) {
        this.Transition<WonState>();
        return;
      }

      this.HandleMovement(input.direction);
    }

    // New movement handling based on Brackeys' Third Person Movement tutorial: https://youtu.be/4HpC--2iowE
    private void HandleMovement(Vector3 direction) {
      var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
      var angle = Mathf.SmoothDampAngle(
        this.Player.transform.eulerAngles.y,
        targetAngle,
        ref this.Player.turnSpeed,
        this.Player.turnTime
      );
      this.Player.transform.rotation = Quaternion.Euler(0f, angle, 0f);

      this.Player.controller.Move(direction * (this.Player.moveSpeed * Time.deltaTime));
    }
  }
}