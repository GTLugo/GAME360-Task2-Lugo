using Managers.Global;

namespace Game.Player {
  internal class IdleState : State {
    protected override void Enter() {
      this.Player.animator.SetBool(AnimationLibrary.isWalking, false);
      Logger.Log($"Player `{this.Player.name}` is Idle");
    }

    public override void Update() {
      var moveInput = InputManager.GetMoveVector();
      // Logger.Log($"IDLE | moveInput `{moveInput}`");

      if (this.Player.Data.Score >= this.Player.Data.TargetScore) {
        this.Transition<WonState>();
        return;
      }

      switch (InputManager.CurrentControlScheme) {
        case ControlScheme.Keyboard:
          var agentCanMove = this.Player.Agent.remainingDistance >=
                             this.Player.Data.stopDistance;
          if (InputManager.Actions.Master.MoveToCursor.IsPressed() || agentCanMove) {
            this.Transition<WalkState>();
          }

          break;
        case ControlScheme.Gamepad:
          if (moveInput.sqrMagnitude >= this.Player.Data.deadZone) {
            this.Transition<WalkState>();
          }

          break;
        default:
          return;
      }
    }
  }
}