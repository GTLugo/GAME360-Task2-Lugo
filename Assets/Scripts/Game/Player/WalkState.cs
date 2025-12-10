using Managers.Global;
using UnityEngine;

namespace Game.Player {
  internal class WalkState : State {
    protected override void Enter() {
      this.Player.animator.SetBool(AnimationLibrary.isWalking, true);
      Logger.Log($"Player `{this.Player.name}` is Walking");
    }

    public override void Update() {
      var moveInput = InputManager.GetMoveVector();
      // Logger.Log($"WALK | moveInput `{moveInput}`");

      if (this.Player.Data.Score >= this.Player.Data.TargetScore) {
        this.Transition<WonState>();
        return;
      }

      switch (InputManager.CurrentControlScheme) {
        case ControlScheme.Keyboard:
          // Convert from (0.0, SCREEN) to (-1.0, 1.0)
          moveInput /= new Vector2(Screen.width, Screen.height);
          moveInput *= 2.0f;
          moveInput = Vector2.ClampMagnitude(moveInput - new Vector2(1.0f, 1.0f), 1.0f);
          // Reshape to be circular and clamp
          moveInput = Vector2.ClampMagnitude(moveInput * 5.0f, 1.0f);

          if (InputManager.Actions.Master.MoveToCursor.IsPressed()) {
            if (moveInput.sqrMagnitude < this.Player.Data.deadZone) {
              this.Transition<IdleState>();
            }

            this.FreeMove(moveInput);
          } else {
            var agentDone = this.Player.Agent.remainingDistance <
                            this.Player.Data.stopDistance;
            if (agentDone) {
              this.Transition<IdleState>();
            }

            var direction = this.Player.Agent.desiredVelocity.normalized;
            this.FaceDirection(direction);
          }

          break;
        case ControlScheme.Gamepad:
          this.Player.Agent.ResetPath();

          if (moveInput.sqrMagnitude < this.Player.Data.deadZone) {
            this.Transition<IdleState>();
          }

          this.FreeMove(moveInput);
          break;
        default:
          return;
      }
    }


    private void FreeMove(Vector2 input) {
      Logger.Log($"Move | Input: {input}");

      var direction = Quaternion.AngleAxis(45, Vector3.up) * new Vector3(input.x, 0.0f, input.y);
      if (direction.magnitude < this.Player.Data.deadZone) {
        return;
      }

      this.FaceDirection(direction);
      this.Player.Controller.Move(direction * (this.Player.Data.MoveSpeed * Time.deltaTime));
    }

    private void FaceDirection(Vector3 direction) {
      var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
      this.Player.transform.rotation = Quaternion.Slerp(
        this.Player.transform.rotation,
        lookRotation,
        Time.deltaTime * this.Player.Data.turnSpeed
      );
    }
  }
}