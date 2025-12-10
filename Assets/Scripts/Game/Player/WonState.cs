using Managers.Global;

namespace Game.Player {
  internal class WonState : State {
    protected override void Enter() {
      this.Player.animator.SetBool(AnimationLibrary.isWalking, false);
      this.Player.won.Trigger(this.Player.transform.position);
      InputManager.Actions.Master.Disable();
      this.Player.Agent.ResetPath();
      this.Player.Agent.speed = 0;

      Logger.Log("Player has won");
    }

    public override void Update() {
      // ...nothing
    }
  }
}