namespace Game.Player {
  internal class WonState : State {
    public override void Enter() {
      Logger.Log("Player has won");
      player.won.Trigger(player.transform.position);
    }

    public override void Update(PlayerInput input) {
      // ...nothing
    }
  }
}