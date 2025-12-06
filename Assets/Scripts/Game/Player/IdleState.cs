namespace Game.Player {
  internal class IdleState : State {
    public override void Enter() {
      Logger.Log("Player is Idle");
    }

    public override void Update(PlayerInput input) {
      if (input.direction.magnitude >= 0.1f) {
        player.Transition<MovingState>();
        return;
      }

      if (player.Score >= player.targetScore) {
        player.Transition<WonState>();
      }
    }
  }
}