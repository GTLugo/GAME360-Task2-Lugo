using Game.Character.Player.States;

namespace Game.Character.Player {
  public abstract class State {
    protected PlayerController Player { get; private set; }

    protected virtual void Enter() { }

    protected virtual void Exit() { }

    public virtual void Update() { }

    protected void Transition<T>() where T : State, new() {
      Logger.Log($"Transitioning from `{this.GetType()}` to {typeof(T)}");
      this.Exit();
      var newState = new T {
        Player = this.Player
      };
      this.Player.Data.State = newState;
      newState.Enter();
    }

    internal static State Init(PlayerController player) {
      return new IdleState {
        Player = player
      };
    }
  }
}