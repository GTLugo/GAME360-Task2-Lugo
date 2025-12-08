using UnityEngine;

namespace Game.Player {
  internal class IdleState : State {
    private static readonly int isWalking = Animator.StringToHash("isWalking");

    protected override void Enter() {
      this.Player.animator.SetBool(isWalking, false);
      Logger.Log($"Player `{this.Player.name}` is Idle");
    }

    public override void Update() {
      if (this.Player.Agent.remainingDistance >= 0.1f) {
        this.Transition<WalkState>();
        return;
      }

      if (this.Player.Data.Score >= this.Player.Data.TargetScore) {
        this.Transition<WonState>();
      }
    }
  }
}