namespace Game.Player {
  internal class IdleState : State {
    public override void Enter() {
      Logger.Log($"Player `{this.Player.name}` is Idle");
    }

    public override void Update(PlayerInput input) {
      if (input.direction.magnitude >= 0.1f) {
        this.Transition<WalkState>();
        return;
      }

      if (this.Player.Score >= this.Player.targetScore) {
        this.Transition<WonState>();
      }
    }
  }
}