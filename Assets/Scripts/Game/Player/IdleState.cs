namespace Game.Player {
  internal class IdleState : State {
    protected override void Enter() {
      this.Player.Agent.ResetPath();
      Logger.Log($"Player `{this.Player.name}` is Idle");
    }

    public override void Update() {
      var moveInput = InputManager.GetMoveVector();
      // Logger.Log($"IDLE | moveInput `{moveInput}`");

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
          // var agentCanMove = this.Player.Agent.remainingDistance >=
          //                    this.Player.Data.stopDistance;
          if (InputManager.Actions.Master.MoveToCursor.IsPressed() || !this.Player.IsAgentDone()) {
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

      // this.Player.ApplyGravity();
    }
  }
}