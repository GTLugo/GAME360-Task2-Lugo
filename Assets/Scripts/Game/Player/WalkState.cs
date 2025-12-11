using Managers.Global;
using UnityEngine;

namespace Game.Player {
  internal class WalkState : State {
    protected override void Enter() {
      this.Player.animator.SetBool(AnimationLibrary.isWalking, true);
      Logger.Log($"Player `{this.Player.name}` is Walking");
    }

    protected override void Exit() {
      this.Player.animator.SetBool(AnimationLibrary.isWalking, false);
    }

    public override void Update() {
      this.Player.Agent.speed = this.Player.Data.MoveSpeed;
      // Logger.Log($"WALK | moveInput `{moveInput}`");

      if (this.Player.Data.HasWon) {
        this.Transition<WonState>();
        return;
      }

      if (this.Player.Data.Health <= 0) {
        this.Transition<DeadState>();
        return;
      }

      switch (InputManager.CurrentControlScheme) {
        case ControlScheme.Keyboard:
          this.UpdateKeyboard();
          break;
        case ControlScheme.Gamepad:
          this.UpdateGamepad();
          break;
        default:
          return;
      }

      // this.Player.ApplyGravity();
    }

    private void UpdateKeyboard() {
      var infinitePlane = new Plane(Vector3.up, this.Player.transform.position);
      var mouseWorldPos = InputManager.GetMousePosOnPlane(infinitePlane);
      var pointing = mouseWorldPos - this.Player.transform.position;
      var input = Vector2.ClampMagnitude(new Vector2(pointing.x, pointing.z), 1.0f);

      if (InputManager.Actions.Master.MoveToCursor.IsPressed()) {
        var direction = new Vector3(input.x, 0.0f, input.y);
        this.FreeMove(direction);
      } else {
        if (this.Player.IsAgentDone()) {
          this.Transition<IdleState>();
        }

        this.Player.FaceDirection(this.Player.Agent.desiredVelocity.normalized);
      }
    }

    private void UpdateGamepad() {
      var input = InputManager.GetMoveVector();

      if (input.sqrMagnitude < this.Player.Data.deadZone) {
        this.Transition<IdleState>();
      }

      var direction = Quaternion.AngleAxis(45, Vector3.up) * new Vector3(input.x, 0.0f, input.y);
      this.FreeMove(direction);
    }


    private void FreeMove(Vector3 direction) {
      if (direction.magnitude < this.Player.Data.deadZone) {
        return;
      }

      Logger.Log($"Move | Input: {direction}");

      this.Player.Agent.updatePosition = false;

      this.Player.FaceDirection(direction);
      this.Player.Data.moveVelocity = direction * (this.Player.Data.MoveSpeed * Time.deltaTime);
      this.Player.Controller.Move(this.Player.Data.moveVelocity);

      this.Player.Agent.Warp(this.Player.transform.position);
      this.Player.Agent.updatePosition = true;
    }
  }
}