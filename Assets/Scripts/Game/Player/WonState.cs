using UnityEngine;

namespace Game.Player {
  internal class WonState : State {
    private static readonly int isWalking = Animator.StringToHash("isWalking");

    protected override void Enter() {
      this.Player.animator.SetBool(isWalking, false);
      this.Player.won.Trigger(this.Player.transform.position);
      this.Player.InputActions.Disable();
      Logger.Log("Player has won");
    }

    public override void Update() {
      // ...nothing
    }
  }
}