namespace Game.Player {
  internal class AttackState : State {
    protected override void Enter() {
      Logger.Log($"Player `{this.Player.name}` is Idle");
    }

    public override void Update() {
      if (this.Player.Data.HasWon) {
        this.Transition<WonState>();
        return;
      }

      if (this.Player.Data.Health <= 0) {
        this.Transition<DeadState>();
      }
    }
  }
}