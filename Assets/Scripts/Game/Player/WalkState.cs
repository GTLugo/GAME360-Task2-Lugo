using UnityEngine;

namespace Game.Player {
  internal class WalkState : State {
    private static readonly int isWalking = Animator.StringToHash("isWalking");

    protected override void Enter() {
      this.Player.animator.SetBool(isWalking, true);
      Logger.Log($"Player `{this.Player.name}` is Walking");
    }

    public override void Update() {
      if (this.Player.Agent.remainingDistance < 0.1f) {
        this.Transition<IdleState>();
        return;
      }

      if (this.Player.Data.Score >= this.Player.Data.TargetScore) {
        this.Transition<WonState>();
        return;
      }

      this.Player.FaceTarget();
    }
  }
}