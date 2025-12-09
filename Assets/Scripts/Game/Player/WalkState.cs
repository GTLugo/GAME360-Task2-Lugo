using UnityEngine;

namespace Game.Player {
  internal class WalkState : State {
    private static readonly int isWalking = Animator.StringToHash("isWalking");

    protected override void Enter() {
      this.Player.animator.SetBool(isWalking, true);
      Logger.Log($"Player `{this.Player.name}` is Walking");
    }

    public override void Update() {
      if (this.Player.Agent.remainingDistance < this.Player.stopDistance) {
        this.Transition<IdleState>();
        return;
      }

      if (this.Player.Data.Score >= this.Player.Data.TargetScore) {
        this.Transition<WonState>();
        return;
      }

      this.Player.FaceTarget();
      if (this.Player.InputActions.Master.Move.IsPressed()) {
        this.Player.Agent.speed = Mathf.Clamp(
          this.Player.Agent.remainingDistance * this.Player.speedRampFactor,
          0.0f,
          this.Player.Data.MoveSpeed
        );
      } else {
        this.Player.Agent.speed = this.Player.Data.MoveSpeed;
      }
    }
  }
}