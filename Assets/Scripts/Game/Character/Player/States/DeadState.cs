using Managers.Global;

namespace Game.Character.Player.States {
  internal class DeadState : State {
    private float _time;

    protected override void Enter() {
      this.Player.Agent.ResetPath();
      this.Player.Agent.updatePosition = false;
      EventManager.playerDied.Trigger(this.Player.transform.position);

      Logger.Log($"Player has died at `{this.Player.transform.position}`");
    }

    protected override void Exit() {
      this.Player.Agent.ResetPath();
      this.Player.Agent.Warp(this.Player.Data.RespawnPosition);
      // EventManager.playerRespawned.Trigger(this.Player.transform.position);
      // this.Player.Agent.updatePosition = true;
      //
      // Logger.Log($"Player has respawned at `{this.Player.transform.position}`");
    }

    public override void Update() {
      this.Transition<LostState>();
      // this._time += Time.deltaTime;
      // if (this._time >= this.Player.Data.RespawnTimeSecs) {
      //   this.Transition<IdleState>();
      // }
    }
  }
}