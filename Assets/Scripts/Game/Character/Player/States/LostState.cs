using Managers.Global;

namespace Game.Character.Player.States {
  internal class LostState : State {
    protected override void Enter() {
      this.Player.animator.SetBool(AnimationLibrary.isWalking, false);
      InputManager.Actions.Master.Disable();
      this.Player.Agent.ResetPath();
      this.Player.Agent.speed = 0;

      Logger.Log("Player has lost");
    }
  }
}