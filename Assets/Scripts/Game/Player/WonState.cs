namespace Game.Player {
  internal class WonState : State {
    protected override void Enter() {
      Logger.Log("Player has won");
      this.Player.won.Trigger(this.Player.transform.position);
    }

    public override void Update(PlayerInput input) {
      // ...nothing
    }
  }
}